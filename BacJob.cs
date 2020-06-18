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
                var FullBackup = new FullBackup(Temp.Source, Temp.Destination, Temp.Save_Options);
            }
            else if (Type == "DIFF")
            {
                var DiffBackup = new DifferentialBackup(Temp.Source, Temp.Destination ,Temp.Save_Options);
            }
            else if(Type == "INC")
            {
                var IncBackup = new IncrementalBackup(Temp.Source, Temp.Destination, Temp.Save_Options);
            }
            else if(Type == "UNCFULL")
            {
                using (UNCAccessWithCredentials.UNCAccessWithCredentials unc = new UNCAccessWithCredentials.UNCAccessWithCredentials())
                {
                    if (unc.NetUseWithCredentials(@"\\kacenka.litv.sssvt.cz\StudentiPrenosne\PerinaRadek", "perinaradek", "litv", "okmujm987"))
                    {
                       var FullBackup = new FullBackup(Temp.Source, Temp.Destination, Temp.Save_Options);
                    }
                    else
                    {
                        throw new Exception("Neplatné přihlašovací údaje");
                    }
                }

            }
            else if (Type == "UNCINC")
            {
                using (UNCAccessWithCredentials.UNCAccessWithCredentials unc = new UNCAccessWithCredentials.UNCAccessWithCredentials())
                {
                    if (unc.NetUseWithCredentials(@"\\kacenka.litv.sssvt.cz\StudentiPrenosne\PerinaRadek", "perinaradek", "litv", "okmujm987"))
                    {
                        var IncBackup = new IncrementalBackup(Temp.Source, Temp.Destination, Temp.Save_Options);
                    }
                    else
                    {
                        throw new Exception("Neplatné přihlašovací údaje");
                    }
                }

            }
            else if (Type == "UNCDIFF")
            {
                using (UNCAccessWithCredentials.UNCAccessWithCredentials unc = new UNCAccessWithCredentials.UNCAccessWithCredentials())
                {
                    if (unc.NetUseWithCredentials(@"\\kacenka.litv.sssvt.cz\StudentiPrenosne\PerinaRadek", "perinaradek", "litv", "okmujm987"))
                    {
                        var DiffBackup = new DifferentialBackup(Temp.Source, Temp.Destination, Temp.Save_Options);
                    }
                    else
                    {
                        throw new Exception("Neplatné přihlašovací údaje");
                    }
                }

            }
            
            else
            {
                Task.WaitAll(Api_Helper.Bac_Post(new Backup { Made = DateTime.Now, Name = Type + " " + DateTime.Now, Job = 30, Size = Temp.Destination, Succesful = false }));
                throw new Exception("Unexpected type of backup :/");

            }

            Console.WriteLine("BACKUP DONE");
            Console.WriteLine("ID: " + Temp.id);
            Console.WriteLine("Type: " + Temp.Type_Of_Backup);
           Task.WaitAll( Api_Helper.Bac_Post(new Backup { Made = DateTime.Now, Name = Type + " " + DateTime.Now, Job = 30, Size = Temp.Destination, Succesful = true }));

            return Task.CompletedTask;  
        }
    }
}
