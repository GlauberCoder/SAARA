using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Entitys;
using Domain.Entitys.AnalisysConfig;
using Domain.Services;
using System.Linq;
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
			var altitudeAnalyserConfig = new AltitudeAnalyserConfig { MinBottom = 3, MinTop = 3, Mode = AltitudeAnalyserMode.Length };
			var trendAnalyserConfig = new TrendAnalyserConfig { AltitudeAnalyserConfig = altitudeAnalyserConfig, Mode = TrendAnalyserMode.MostRecents };
			return new MACDConfig { EMAConfig = new EMAConfig { EMA1 = shortEMA, EMA2 = longEMA }, SignalEMA = signalEMA, MACDTrendAnalyserConfig = trendAnalyserConfig , PriceTrendAnalyserConfig = trendAnalyserConfig };
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
			InlineData(23.47, 24, 6, 10),
			InlineData(23.39, 25, 6, 10),
			InlineData(23.37, 26, 6, 10),
			InlineData(23.23, 27, 6, 10),
			InlineData(23.20, 28, 6, 10),
			InlineData(23.05, 29, 6, 10),
			InlineData(22.90, 30, 6, 10),
			InlineData(23.20, 28, 6, 12),
			InlineData(23.07, 29, 6, 12),
			InlineData(22.93, 30, 6, 12) 
		]
		public void The_LongEMA_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA).MACD.LongEMA;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The ShortEMA from MACD Analyses after Calculate method should be"),
			InlineData(23.54, 24, 6, 10),
			InlineData(23.43, 25, 6, 10),
			InlineData(23.42, 26, 6, 10),
			InlineData(23.22, 27, 6, 10),
			InlineData(23.18, 28, 6, 10),
			InlineData(22.96, 29, 6, 10),
			InlineData(22.74, 30, 6, 10),
			InlineData(23.18, 28, 6, 12),
			InlineData(22.96, 29, 6, 12),
			InlineData(22.74, 30, 6, 12),
		]
		public void The_ShortEMA_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA).MACD.ShortEMA;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The MACD value from MACD Analyses after Calculate method should be"),
			InlineData(0.07, 24, 6, 10),
			InlineData(0.04, 25, 6, 10),
			InlineData(0.05, 26, 6, 10),
			InlineData(-0.01, 27, 6, 10),
			InlineData(-0.02, 28, 6, 10),
			InlineData(-0.09, 29, 6, 10),
			InlineData(-0.16, 30, 6, 10),
			InlineData(-0.02, 28, 6, 12),
			InlineData(-0.11, 29, 6, 12),
			InlineData(-0.19, 30, 6, 12)
		]
		public void The_MACD_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA).MACD.Value;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The Signal from MACD Analyses after Calculate method should be"),

			InlineData(0.08, 25, 6, 10, 3, 25),
			InlineData(0.07, 26, 6, 10, 3, 26),
			InlineData(0.03, 27, 6, 10, 3, 27),
			InlineData(-0.0, 28, 6, 10, 3, 28),
			InlineData(-0.04, 29, 6, 10, 3, 29),
			InlineData(-0.10, 30, 6, 10, 3, 30),
		]
		public void The_Signal_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA, int signalEMA, int referenceCandle)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA, signalEMA, referenceCandle).MACD.Signal;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The Histogram from MACD Analyses after Calculate method should be"),
			InlineData(-0.04, 27, 6, 10, 3, 27),
			InlineData(-0.02, 28, 6, 10, 3, 28),
			InlineData(-0.05, 29, 6, 10, 3, 29),
			InlineData(-0.06, 30, 6, 10, 3, 30),
		]
		public void The_Histogram_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA, int signalEMA, int referenceCandle)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA, signalEMA, referenceCandle).MACD.Histogram;
			Assert.Equal(expected, actual);
		}



	}
}
