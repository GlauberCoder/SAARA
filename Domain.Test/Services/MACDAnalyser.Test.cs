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
		private IMACDAnalyser genarateCandles(double[] closeValueCandle, int shortEMA, int longEMA, int signalEMA = 2, int? candleID = null)
		{
			var i = 1;
			var config = new MACDConfig { EMA1 = shortEMA, EMA2 = longEMA, SignalEMA = signalEMA };
			var candles = closeValueCandle.Select(a => new Candle() { ID = i++, Close = decimal.Parse(a.ToString()) }).ToList<ICandle>();
			var analyser =  new CandleAnalyser { Previous = candles };

			return candleID.HasValue ? new MACDAnalyser(config, analyser, analyser.Previous.First(c => c.ID == candleID)) :  new MACDAnalyser(config, analyser);
		}

		[
			Theory(DisplayName = "The LongEMA from MACD Analyses after Calculate method should be"),
			InlineData(22.22, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 6, 10),
			InlineData(22.21, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 6, 10)
		]
		public void The_LongEMA_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, double[] closeValueCandle, int shortEMA, int longEMA)
		{
			var actual = genarateCandles(closeValueCandle, shortEMA, longEMA).LongEMA;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The ShortEMA from MACD Analyses after Calculate method should be"),
			InlineData(22.26, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 6, 10),
			InlineData(22.23, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 6, 10)
		]
		public void The_ShortEMA_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, double[] closeValueCandle, int shortEMA, int longEMA)
		{
			var actual = genarateCandles(closeValueCandle, shortEMA, longEMA).ShortEMA;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The MACD value from MACD Analyses after Calculate method should be"),
			InlineData(0.04, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 6, 10),
			InlineData(0.02, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 6, 10)
		]
		public void The_MACD_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, double[] closeValueCandle, int shortEMA, int longEMA)
		{
			var actual = genarateCandles(closeValueCandle, shortEMA, longEMA).MACD;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The Signal from MACD Analyses after Calculate method should be"),
			InlineData(0.04, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 6, 10, 2, 3),
			InlineData(0.02, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 6, 10, 2, 3)
		]
		public void The_Signal_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, double[] closeValueCandle, int shortEMA, int longEMA, int signalEMA, int referenceCandle)
		{
			var actual = genarateCandles(closeValueCandle, shortEMA, longEMA, signalEMA, referenceCandle).Signal;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The Histogram from MACD Analyses after Calculate method should be"),
			InlineData(0.0, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 6, 10, 2, 3),
			InlineData(0.0, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 6, 10, 2, 3)
		]
		public void The_Histogram_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, double[] closeValueCandle, int shortEMA, int longEMA, int signalEMA, int referenceCandle)
		{
			var actual = genarateCandles(closeValueCandle, shortEMA, longEMA, signalEMA, referenceCandle).Histogram;
			Assert.Equal(expected, actual);
		}

	}
}
