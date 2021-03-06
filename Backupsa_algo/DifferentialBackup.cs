﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Deamon.Backupsa_algo
{
   public class DifferentialBackup : BackupTemplate
    {
        public DifferentialBackup(string sSource, string sTarget,string FileOption) : base(sSource, sTarget,FileOption)
        {
            if (File.Exists(this.ConfigPath)) 
               this.config = Deserialize(this.ConfigPath);
            else
            {
                FullBackup full = new FullBackup(Source.FullName, Target.Parent.FullName,SaveOption);

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
