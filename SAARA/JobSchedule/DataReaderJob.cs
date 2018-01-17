using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
using Infra.ExchangerDataReader;
using Infra.ExchangerDataReader.BitcoinTradeDataReader;
using Quartz;

namespace SAARA.JobSchedule
{
	public class DataReaderJob : IDataReaderJob
	{

		public async Task Execute(IJobExecutionContext context)
		{
			await Task.Run((Action) ReadBitfinex);
			await Task.Run((Action) ReadBitcoinTrade);

			Console.WriteLine("\n");

			//TODO: saveCandle
		}

		private void ReadBitfinex()
		{
			var symbolName = "BTCUSD";
			var candle = new BitfinexDataReader().Read(new Symbol() { Name = symbolName }, CandleTimespan.OneMinute);
			Console.Write("\nBitfinex : ");
			PrintCandle(candle);
		}

		private void ReadBitcoinTrade()
		{
			var symbolName = "BTC";
			var candle = new BitcoinTradeDataReader().Read(new Symbol() { Name = symbolName }, CandleTimespan.OneHour);
			Console.Write("\nBitcoinTrade : ");
			PrintCandle(candle);
		}

		public void PrintCandle(ICandle candle)
		{
			Console.WriteLine("Timespan = {0}, Open = {1}, Close = {2}, High = {3}, Low = {4}, Vol = {5} ", candle.Date, candle.Open, candle.Close, candle.High, candle.Low, candle.Vol);
		}
	}
}
