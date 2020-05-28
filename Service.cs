using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Quartz;
using Quartz.Impl; 
namespace Deamon
{
    public class Service
    {

        public Service()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler().GetAwaiter().GetResult();
        }
        public IScheduler scheduler;

        public void Start()
        {
            scheduler.Start().GetAwaiter().GetResult();
            JobSet(); 

        }
        public void Stop()
        {
            scheduler.Shutdown().GetAwaiter().GetResult();
        }

        public void JobSet()
        {
            //Testing FUCTIONALITY 
            //IJobDetail job = JobBuilder.Create(typeof(BacJob)).Build();
            //ITrigger trigger = TriggerBuilder.Create().StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever()).Build();
            //job.JobDataMap.Add("ID", 37);
            //scheduler.ScheduleJob(job, trigger).GetAwaiter().GetResult();

            foreach (var item in Api_Helper.Temp_Get().Result)
            {
                IJobDetail job = JobBuilder.Create(typeof(BacJob)).Build();
                ITrigger trigger = TriggerBuilder.Create().StartNow().WithCronSchedule(item.Schedule).Build();
                job.JobDataMap.Add("ID", item.id);
                scheduler.ScheduleJob(job, trigger).GetAwaiter().GetResult();
            }

        }

    }
}
