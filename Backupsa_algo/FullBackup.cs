using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Deamon.Backupsa_algo
{
   public class FullBackup : BackupTemplate
    {              
        public FullBackup(string sSource, string sTarget, string FileOption) : base(sSource, sTarget,FileOption)
        {
            config = new Config();
            config.snapshots = new List<ModelSnapshot>();

            CreateFull(Source, Target);          


            Write();
        }

        public void CreateFull(DirectoryInfo Source, DirectoryInfo Destination)
        {
            //foreach (FileInfo item in Source.GetFiles())
            //{
            //    config.snapshots.Add(new ModelSnapshot() {
            //        path = item.FullName,
            //        type = "FILE",
            //        created = item.CreationTime
            //    });               
            //    item.CopyTo(Path.Combine(Destination.FullName, item.Name));
            //}

            //foreach (DirectoryInfo diSourceSubDir in Source.GetDirectories())
            //{
            //    DirectoryInfo nextTargetSubDir =
            //        Destination.CreateSubdirectory(diSourceSubDir.Name);
            //    config.snapshots.Add(new ModelSnapshot() {
            //        path = diSourceSubDir.FullName,
            //        type = "DIR",
            //        created = diSourceSubDir.CreationTime
            //    });
            //    CreateFull(diSourceSubDir, nextTargetSubDir);
            //}

            this.BackupWSnapshot();

        }






    }
}
