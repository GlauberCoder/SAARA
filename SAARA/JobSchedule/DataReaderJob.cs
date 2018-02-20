using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
using Infra.ExchangerDataReader;
using Infra.ExchangerDataReader.PoloniexDataReader;
using Infra.ExchangerDataReader.BinanceDataReader;
using Infra.ExchangerDataReader.BittrexDataReader;
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
			await Task.Run((Action) ReadPoloniex);
			await Task.Run((Action) ReadBinance);
			await Task.Run((Action) ReadBittrex);

			Console.WriteLine("\n");

			//TODO: saveCandle
		}

		private void ReadBitfinex()
		{
			var symbolName = "BTCUSD";
			var candle = new BitfinexDataReader().Read(new Symbol() { Name = symbolName }, CandleTimespan.OneHour, DateTime.Now);
			Console.Write("\nBitfinex : ");
			PrintCandle(candle);
		}

		private void ReadBitcoinTrade()
		{
			var symbolName = "BTC";
			var candle = new BitcoinTradeDataReader().Read(new Symbol() { Name = symbolName }, CandleTimespan.OneHour,DateTime.Now);
			Console.Write("\nBitcoinTrade : ");
			PrintCandle(candle);
		}

		private void ReadPoloniex()
		{
			var symbolName = "USDT_BTC";
			var candle = new PoloniexDataReader().Read(new Symbol() { Name = symbolName }, CandleTimespan.OneHour, DateTime.Now);
			Console.Write($"\nPoloniex : ");
			PrintCandle(candle);
		}

		private void ReadBittrex()
		{
			var symbolName = "USDT-BTC";
			var candle = new BittrexDataReader().Read(new Symbol() { Name = symbolName }, CandleTimespan.OneHour, DateTime.Now);
			Console.Write($"\nBittrex : ");
			PrintCandle(candle);
		}

		private void ReadBinance()
		{
			var symbolName = "BTCUSDT";
			var candle = new BinanceDataReader().Read(new Symbol() { Name = symbolName }, CandleTimespan.OneHour, DateTime.Now);
			Console.Write($"\nBinance : ");
			PrintCandle(candle);
		}

		public void PrintCandle(ICandle candle)
		{
			Console.WriteLine("Timespan = {0}, Open = {1}, Close = {2}, High = {3}, Low = {4}, Vol = {5} ", candle.Date, candle.Open, candle.Close, candle.High, candle.Low, candle.Vol);
		}
	}
}
