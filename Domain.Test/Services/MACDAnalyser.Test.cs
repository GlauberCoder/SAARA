using Domain.Abstractions.Entitys;
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
			return new MACDConfig { EMAConfig = new EMAConfig { EMA1 = shortEMA, EMA2 = longEMA }, SignalEMA = signalEMA };
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
			InlineData(23.34, 20, 6, 10),
			InlineData(23.43, 21, 6, 10),
			InlineData(23.51, 22, 6, 10),
			InlineData(23.53, 23, 6, 10),
			InlineData(23.47, 24, 6, 10),
			InlineData(23.46, 22, 6, 11),
			InlineData(23.41, 24, 6, 12),
			InlineData(23.33, 26, 6, 13)
		]
		public void The_LongEMA_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA).MACD.LongEMA;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The ShortEMA from MACD Analyses after Calculate method should be"),
			InlineData(23.59, 20, 6, 10),
			InlineData(23.66, 21, 6, 10),
			InlineData(23.72, 22, 6, 10),
			InlineData(23.70, 23, 6, 10),
			InlineData(23.55, 24, 6, 10),
			InlineData(23.60, 21, 7, 10),
			InlineData(23.54, 21, 8, 10),
			InlineData(23.48, 21, 9, 10)
		]
		public void The_ShortEMA_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA).MACD.ShortEMA;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The MACD value from MACD Analyses after Calculate method should be"),
			InlineData(0.25, 20, 6, 10),
			InlineData(0.23, 21, 6, 10),
			InlineData(0.21, 22, 6, 10),
			InlineData(0.17, 23, 6, 10),
			InlineData(0.08, 24, 6, 10),
			InlineData(0.26, 22, 6, 11),
			InlineData(0.14, 24, 6, 12),
			InlineData(0.07, 26, 6, 13),
			InlineData(0.17, 21, 7, 10),
			InlineData(0.11, 21, 8, 10),
			InlineData(0.05, 21, 9, 10)

		]
		public void The_MACD_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA).MACD.Value;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The Signal from MACD Analyses after Calculate method should be"),
			InlineData( 0.04, 14, 6, 10, 4, 14),
			InlineData( 0.08, 15, 6, 10, 4, 15),
			InlineData( 0.15, 16, 6, 10, 4, 16),
			InlineData( 0.21, 17, 6, 10, 4, 17),
			InlineData( 0.24, 18, 6, 10, 4, 18),
			InlineData( 0.26, 19, 6, 10, 4, 19),
			InlineData( 0.26, 20, 6, 10, 4, 20),
			InlineData( 0.25, 21, 6, 10, 4, 21),
			InlineData( 0.23, 22, 6, 10, 4, 22),
			InlineData( 0.21, 23, 6, 10, 4, 23),
			InlineData( 0.16, 24, 6, 10, 4, 24),
			InlineData( 0.10, 25, 6, 10, 4, 25),
			InlineData( 0.07, 26, 6, 10, 4, 26),
			InlineData( 0.01, 27, 6, 10, 4, 27),
			InlineData(-0.02, 28, 6, 10, 4, 28),
			InlineData(-0.06, 29, 6, 10, 4, 29),
			InlineData(-0.12, 30, 6, 10, 4, 30)
		]
		public void The_Signal_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA, int signalEMA, int referenceCandle)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA, signalEMA, referenceCandle).MACD.Signal;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The Histogram from MACD Analyses after Calculate method should be"),
			InlineData( 0.02, 14, 6, 10, 4, 14),
			InlineData( 0.07, 15, 6, 10, 4, 15),
			InlineData( 0.11, 16, 6, 10, 4, 16),
			InlineData( 0.08, 17, 6, 10, 4, 17),
			InlineData( 0.05, 18, 6, 10, 4, 18),
			InlineData( 0.03, 19, 6, 10, 4, 19),
			InlineData(-0.01, 20, 6, 10, 4, 20),
			InlineData(-0.02, 21, 6, 10, 4, 21),
			InlineData(-0.02, 22, 6, 10, 4, 22),
			InlineData(-0.04, 23, 6, 10, 4, 23),
			InlineData(-0.08, 24, 6, 10, 4, 24),
			InlineData(-0.08, 25, 6, 10, 4, 25),
			InlineData(-0.06, 26, 6, 10, 4, 26),
			InlineData(-0.08, 27, 6, 10, 4, 27),
			InlineData(-0.04, 28, 6, 10, 4, 28),
			InlineData(-0.07, 29, 6, 10, 4, 29),
			InlineData(-0.08, 30, 6, 10, 4, 30)
		]
		public void The_Histogram_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA, int signalEMA, int referenceCandle)
		{
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA, signalEMA, referenceCandle).MACD.Histogram;
			Assert.Equal(expected, actual);
		}



	}
}
