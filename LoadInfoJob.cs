using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon
{
    public class LoadInfoJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            if (Properties.Settings.Default.Id == 0)
            {
                Api_Helper.Info_Register();
            }
            else
            {
                Api_Helper.Info_Get();
            }

            Console.WriteLine("ID: " + Properties.Settings.Default.Id);

            return Task.CompletedTask;
        }
    }
}
