using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
using Infra.ExchangerDataReader;
using Quartz;

namespace SAARA.JobSchedule
{
	public class DataReaderJob : IDataReaderJob
	{

		public async Task Execute(IJobExecutionContext context)
		{
			var symbolName = "BTCUSD";

			var candleBitfinex = new BitfinexDataReader().Read(new Symbol() { Name = symbolName }, CandleTimespan.OneMinute);

		}

		public void PrintCandle(ICandle candle)
		{
			Console.WriteLine("Timespan = {0}, Open = {1}, Close = {2}, High = {3}, Low = {4}, Vol = {5} ", candle.Date, candle.Open, candle.Close, candle.High, candle.Low, candle.Vol);
		}
	}
}
