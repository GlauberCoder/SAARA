using Domain.Abstractions.Entitys;
using Domain.Abstractions.Services;
using Domain.Entitys;
using Domain.Entitys.AnalisysConfig;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Domain.Test.Services
{
	public class MACDAnalyserTest
	{
		decimal[] CloseCandleValues => new decimal[] { 22.27m, 22.19m, 22.08m, 22.17m, 22.18m, 22.13m, 22.23m, 22.43m, 22.24m, 22.29m, 22.15m, 22.39m, 22.38m, 22.61m, 23.36m, 24.05m, 23.75m, 23.83m, 23.95m, 23.63m, 23.82m, 23.87m, 23.65m, 23.19m, 23.10m, 23.33m, 22.68m, 23.10m, 22.40m, 22.17m, 22.23m, 22.23m, 22.23m, 22.23m, 22.23m, 22.23m, 22.23m, 22.23m };

		private decimal[] GetCloseCandleValues(int length)
		{
			return CloseCandleValues.Take(length).ToArray();
		}

		private MACDConfig GetConfig(int shortEMA, int longEMA, int signalEMA)
		{
			return new MACDConfig { EMA1 = shortEMA, EMA2 = longEMA, SignalEMA = signalEMA };
		}

		private CandleAnalyser GetCandleAnalyser(decimal[] closeValueCandle)
		{
			var i = 1;
			var candles = closeValueCandle.Select(a => new Candle() { ID = i++, Close = a }).ToList<ICandle>();
			return new CandleAnalyser { Previous = candles };
		}

		private IMACDAnalyser generateCandleAnalyser(decimal[] closeValueCandle, int shortEMA, int longEMA, int signalEMA = 2, int? candleID = null)
		{
			var config = GetConfig(shortEMA, longEMA, signalEMA);
			var analyser = GetCandleAnalyser(closeValueCandle);

			return candleID.HasValue ?
				new MACDAnalyser(config, analyser, analyser.Previous.First(c => c.ID == candleID)) : new MACDAnalyser(config, analyser, analyser.Previous.Last());
		}


		[
			Theory(DisplayName = "The LongEMA from MACD Analyses after Calculate method should be"),
			InlineData(23.34, 30, 6, 10),
			InlineData(23.34, 31, 6, 10),
			InlineData(23.46, 30, 6, 11),
			InlineData(23.41, 30, 6, 12),
			InlineData(23.33, 31, 6, 13)
		]
		public void The_LongEMA_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA).LongEMA;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The ShortEMA from MACD Analyses after Calculate method should be"),
			InlineData(22.28, 30, 6, 10),
			InlineData(22.28, 31, 6, 10),
			InlineData(22.37, 30, 7, 10),
			InlineData(22.91, 30, 8, 10),
			InlineData(23.19, 30, 9, 10)
		]
		public void The_ShortEMA_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA).ShortEMA;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The MACD value from MACD Analyses after Calculate method should be"),
			InlineData(-1.06, 30, 6, 10),
			InlineData(-1.06, 31, 6, 10),
			InlineData(-1.06, 32, 6, 10),
			InlineData(-1.18, 30, 6, 11),
			InlineData(-1.13, 30, 6, 12),
			InlineData(-1.05, 31, 6, 13),
			InlineData(-0.97, 30, 7, 10),
			InlineData(-0.43, 30, 8, 10),
			InlineData(-0.15, 30, 9, 10)
		]
		public void The_MACD_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA).MACD;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The Signal from MACD Analyses after Calculate method should be"),
			InlineData(0.56, 32, 6, 10, 5, 32),
			InlineData(0.34, 30, 6, 9, 5, 30)
		]
		public void The_Signal_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA, int signalEMA, int referenceCandle)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA, signalEMA, referenceCandle).Signal;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The Histogram from MACD Analyses after Calculate method should be"),
			InlineData(0.0, 20, 6, 10, 2, 3),
			InlineData(0.0, 20, 6, 10, 2, 4)
		]
		public void The_Histogram_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA, int signalEMA, int referenceCandle)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA, signalEMA, referenceCandle).Histogram;
			Assert.Equal(expected, actual);
		}

	}
}
