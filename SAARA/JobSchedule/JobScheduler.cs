using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace SAARA.JobSchedule
{
	public class JobScheduler
	{
		public static async Task Start()
		{
			var OneHour = 3600;

			var props = new NameValueCollection
			{
				{ "quartz.serializer.type", "binary" }
			};

			var factory = new StdSchedulerFactory(props);

			var scheduler = await factory.GetScheduler();
			await scheduler.Start();

			var job = JobBuilder.Create<DataReaderJob>()
				.WithIdentity("ExchangerDataReader", "group1")
				.Build();

			var trigger = TriggerBuilder.Create()
				.WithIdentity("TriggerMinute", "group1")
				.StartNow()
				.WithSimpleSchedule(x => x
					.WithIntervalInSeconds(OneHour)
					.RepeatForever())
				.Build();

			await scheduler.ScheduleJob(job, trigger);

		}
	}
}
