using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Deamon.Backupsa_algo
{
   public class FullBackup
    {

        public Config config { get; set; } = new Config();


        DirectoryInfo Source { get; set; }
        DirectoryInfo Target { get; set; }


        public void Write()
        {
            config.Write(Target.FullName +  "\\configuration.txt");
        }

        public FullBackup(string sSource, string sTarget)
        {
            Source = new DirectoryInfo(sSource);
            Target = new DirectoryInfo(sTarget);

            CopyFull(Source, Target);


        }

        public void CopyFull(DirectoryInfo Source, DirectoryInfo Destination)
        {
            Directory.CreateDirectory(Destination.FullName);

            foreach (FileInfo item in Source.GetFiles())
            {
                item.CopyTo(Path.Combine(Destination.FullName, item.Name));



                config.snapshots.Add(new ModelSnapshot() {

                    path = item.FullName,
                    type = "file",
                    created = item.CreationTime


                });
               
            }


            foreach (DirectoryInfo diSourceSubDir in Source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    Destination.CreateSubdirectory(diSourceSubDir.Name);
                CopyFull(diSourceSubDir, nextTargetSubDir);

                config.snapshots.Add(new ModelSnapshot()
                {

                    path = diSourceSubDir.FullName,
                    type = "Dir",
                    created = diSourceSubDir.CreationTime


                });

            }

        }






    }
}
