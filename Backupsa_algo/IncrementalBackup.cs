using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon.Backupsa_algo
{
    public class IncrementalBackup : BackupTemplate
    {
        public IncrementalBackup(string sSource, string sTarget , string FileOption) : base(sSource, sTarget, FileOption)
        {
            if (File.Exists(this.ConfigPath))
                this.config = Deserialize(this.ConfigPath);
            else
            {
                FullBackup full = new FullBackup(Source.FullName, Target.Parent.FullName,SaveOption);

                return;
            }

            CreateInc();

            Write();
        }

        public void CreateInc()
        {
            this.BackupWSnapshot();
        }

    }
}
