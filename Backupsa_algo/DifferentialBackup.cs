using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Deamon.Backupsa_algo
{
   public class DifferentialBackup : BackupTemplate
    {
        public DifferentialBackup(string sSource, string sTarget) : base(sSource, sTarget)
        {
            if (File.Exists(this.ConfigPath)) 
               this.config = Deserialize(this.ConfigPath);
            else
            {
                FullBackup full = new FullBackup(Source.FullName, Target.Parent.FullName);

                return;
            }

            CreateDiff();
        }          

        public void CreateDiff()
        {
            Backup();
        }

        




    }
}
