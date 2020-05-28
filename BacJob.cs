using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deamon.Backupsa_algo;
using Quartz; 
namespace Deamon
{
    public class BacJob : IJob
    {       
        public Task Execute(IJobExecutionContext context)
        {
            int ID = (int)context.MergedJobDataMap.Get("ID");
            Template Temp = Api_Helper.Temp_Get().Result.First(x => x.id == ID);

            string Type = Temp.Type_Of_Backup.ToUpper();
            if (Type == "FULL")
            {
                var FullBackup = new FullBackup(Temp.Source, Temp.Destination);
            }
            else if (Type == "DIFF")
            {
                var DiffBackup = new DifferentialBackup(Temp.Source, Temp.Destination);
            }
            else if(Type == "INC")
            {
                var IncBackup = new IncrementalBackup(Temp.Source, Temp.Destination);
            }
            else
            {
                throw new Exception("Unexpected type of backup :/");
            }

            Console.WriteLine("BACKUP DONE");
            Console.WriteLine("ID: " + Temp.id);
            Console.WriteLine("Type: " + Temp.Type_Of_Backup);

            return Task.CompletedTask;  
        }
    }
}
