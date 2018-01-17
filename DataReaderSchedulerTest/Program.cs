using System;
using SAARA.JobSchedule;
using System;
using System.Timers;
using Infra.ExchangerDataReader;
using Domain.Entitys;
using Domain.Abstractions.Enums;

namespace DataReaderSchedulerTest
{
	class Program
	{
		static void Main(string[] args)
		{
			JobScheduler.Start().GetAwaiter().GetResult();
			Console.Read();
		}
	}
}

