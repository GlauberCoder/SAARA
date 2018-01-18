using Domain.Abstractions.Enums;
using Domain.Entitys;
using Infra.ExchangerDataReader.BitcoinTradeDataReader;
using SAARA.JobSchedule;
using System;
using System.IO;
using Util.Extensions;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {

			JobScheduler.Start().GetAwaiter().GetResult();
			Console.Read();

			//var time = new BitcoinTradeDataReader().GetTimeValues(CandleTimespan.OneMinute, DateTime.Now);
			//foreach (var item in time)
			//{
			//	Console.WriteLine("{0}, {1}", item.Key, item.Value);
			//}
			//Console.WriteLine(new BitcoinTradeDataReader().GetUrlFrom(new Symbol() { Name = "BTC" }, CandleTimespan.OneMinute, new DateTime(2017, 12, 12, 10, 10, 10)));
			//Console.WriteLine(new BitcoinTradeDataReader().GetUrlFrom(new Symbol() { Name = "BTC" }, CandleTimespan.FiveMinutes, new DateTime(2017, 12, 12, 10, 10, 10)));
			//Console.WriteLine(new BitcoinTradeDataReader().GetUrlFrom(new Symbol() { Name = "BTC" }, CandleTimespan.FifteenMinutes, new DateTime(2017, 12, 12, 10, 10, 10)));
			//Console.WriteLine(new BitcoinTradeDataReader().GetUrlFrom(new Symbol() { Name = "BTC" }, CandleTimespan.ThirtyMinutes, new DateTime(2017, 12, 12, 10, 10, 10)));
			//Console.WriteLine(new BitcoinTradeDataReader().GetUrlFrom(new Symbol() { Name = "BTC" }, CandleTimespan.OneHour, new DateTime(2017, 12, 12, 10, 10, 10)));

			//var fileName = "data.json";
			//var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
			//var json = string.Empty;
			//using (var streamReader = new StreamReader(filePath))
			//	json = streamReader.ReadToEnd();

			//var candle = new BitcoinTradeDataReader().GetCandleFrom(json);

			//Console.Write("date {0}, open {1}, close {2}, high {3}, low {4}, vol {5} ", candle.Date, candle.Open, candle.Close, candle.High, candle.Low, candle.Vol);
			//Console.Read();

			//var time = new BitcoinTradeDataReader().GetTimeValues(CandleTimespan.OneMinute, DateTime.Now);
			//foreach (var item in time)
			//{
			//	Console.WriteLine("{0}, {1}", item.Key, item.Value);
			//}

			//var time = new BitcoinTradeDataReader().GetTimeValues(CandleTimespan.OneMinute, DateTime.Now);
			//foreach (var item in time)
			//{
			//	Console.WriteLine("{0}, {1}", item.Key, item.Value);
			//}

			//Console.WriteLine(new BitfinexDataReader().GetUrlFrom(new Symbol() { Name = "BTCUSD" }, CandleTimespan.FifteenMinutes, DateTime.Now.WithoutMilliseconds()));
			//Console.Write(new BitcoinTradeDataReader().GetUrlFrom(new Symbol() { Name = "BTC" }, CandleTimespan.OneHour, DateTime.Now.WithoutMilliseconds()));
			//Console.Read();
		}
    }
}
