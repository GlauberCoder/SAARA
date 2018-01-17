using System;
using SAARA.JobSchedule;
using System;
using System.Timers;
using Infra.ExchangerDataReader;
using Domain.Entitys;
using Domain.Abstractions.Enums;
using Infra.ExchangerDataReader.BitcoinTradeDataReader;
using System.IO;

namespace DataReaderSchedulerTest
{
	class Program
	{
		static void Main(string[] args)
		{
			JobScheduler.Start().GetAwaiter().GetResult();
			Console.Read();

			//var time = new BitcoinTradeDataReader().GetTimeValues(CandleTimespan.OneMinute);
			//foreach(var item in time)
			//{
			//	Console.WriteLine("{0}, {1}", item.Key, item.Value);
			//}

			//Console.WriteLine(new BitcoinTradeDataReader().GetUrlFrom(new Symbol() { Name = "BTC" }, CandleTimespan.FifteenMinutes));

			//var fileName = "data.json";
			//var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
			//var json = string.Empty;
			//using (var streamReader = new StreamReader(filePath))
			//	json = streamReader.ReadToEnd();

			//var candle = new BitcoinTradeDataReader().GetCandleFrom(json);

			//Console.Write("date {0}, open {1}, close {2}, high {3}, low {4}, vol {5} ", candle.Date, candle.Open, candle.Close, candle.High, candle.Low, candle.Vol);
			//Console.Read();
		}
	}
}

