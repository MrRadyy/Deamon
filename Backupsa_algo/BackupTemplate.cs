using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Threading;
using System.Net;
using FluentFTP; 
namespace Deamon
{
    public class BackupTemplate
    {
        public Config config { get; set; }
        protected string ConfigPath { get => this.Target.Parent.FullName + "\\configuration.txt"; }

        protected DirectoryInfo Source { get; set; }
        protected DirectoryInfo Target { get; set; }
 
        protected  string SaveOption { get; set; }
        
        public BackupTemplate(string sSource, string sTarget,string SaveOptionQ)
        {
            string Targetos = Path.Combine(sTarget, DateTime.Now.ToString("MM_dd_yyyy_H_mm_ss"));

            Directory.CreateDirectory(Targetos);

            Source = new DirectoryInfo(sSource);
            Target = new DirectoryInfo(Targetos);

            SaveOption = SaveOptionQ;
        }

        protected void Write()
        {
            if (File.Exists(this.ConfigPath))
                File.Delete(this.ConfigPath);

            config.Write(this.ConfigPath);
        }

        protected string MakeRelative(string filePath, string referencePath)
        {
            var fileUri = new Uri(filePath);

            var referenceUri = new Uri(referencePath);

            return Uri.UnescapeDataString(referenceUri.MakeRelativeUri(fileUri).ToString()).Replace('/', System.IO.Path.DirectorySeparatorChar).Replace(@"..", "");
        }
        protected List<string> Compare(string path, string sourcepath)
        {
            List<string> PathsSnapshot = this.config.snapshots.Select(item => MakeRelative(item.path, path).Trim('\\')).ToList();

            List<string> PathsSources = GetDirectory(sourcepath, sourcepath);

            return PathsSources.Except(PathsSnapshot).ToList();
        }
        protected List<string> GetDirectory(string path, string refpath)
        {
            List<string> pathsrecursive = new List<string>();

            foreach (var item in Directory.GetDirectories(path))
            {
                pathsrecursive.AddRange(GetDirectory(item, refpath));
            }

            List<string> pathsRelative = Directory.GetDirectories(path).Select(item => MakeRelative(item, refpath)).ToList();

            List<string> pathfiles = Directory.GetFiles(path).Select(item => MakeRelative(item, refpath)).ToList();

            pathsrecursive.AddRange(pathfiles);
            pathsrecursive.AddRange(pathsRelative);
            return pathsrecursive;
        }

        protected List<string> Backup()
        {
            List<string> Comparison = new List<string>();

            string zipPath = @".\result.zip";
                       
            
            foreach (string item in Compare(this.ConfigPath, Source.FullName))
            {
                string[] ItemParts = item.Split('\\');
                ItemParts[0] = "";
                string FormattedItem = string.Join(@"\", ItemParts).Trim('\\');

                string Source = Path.Combine(this.Source.FullName, FormattedItem);
                string Target = Path.Combine(this.Target.FullName, FormattedItem);

                Comparison.Add(Source);

                FileAttributes attr = File.GetAttributes(Source);

                if (!attr.HasFlag(FileAttributes.Directory))
                    File.Copy(Source, Target);
                else
                    Directory.CreateDirectory(Target);
                

            }
            var templates = Api_Helper.Temp_Get().Result;
            foreach (var item in templates)
            {
                if (item.Save_Options == "rar" || item.Save_Options == "RAR" || item.Save_Options == "RAM")
                {
                    ZipFile.CreateFromDirectory(Target.FullName, Path.Combine(Target.FullName + @".zip"));
                }

            }


            foreach (var item in templates)
            {
                if (item.Destination == "FTP")
                    FTP.FTPBackup(); 
               
            }
            
            return Comparison;
        }       

        protected void BackupWSnapshot()
        {
            Backup().ForEach(Path =>
                this.config.snapshots.Add(new ModelSnapshot
                {
                    path = Path,
                    type = File.GetAttributes(Path) == FileAttributes.Directory ? "DIR" : "FILE",
                    created = Directory.GetCreationTime(Path)
                })
            );
        }

        protected Config Deserialize(string path)
        {
            string text;

            using (StreamReader sr = new StreamReader(path))
            {
                text = sr.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<Config>(text);
        }

       
    }
}
