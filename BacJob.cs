using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz; 
namespace Deamon
{
    public class BacJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            int ID = (int)context.MergedJobDataMap.Get("ID");
            var temp =  Api_Helper.Temp_Get().Result.First(x => x.id == ID);
            Console.WriteLine(ID);
            return Task.CompletedTask;  
        }
    }
}
