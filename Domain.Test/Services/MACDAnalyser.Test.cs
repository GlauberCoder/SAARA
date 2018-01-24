using Domain.Abstractions.Entitys;
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
		[
			Theory(DisplayName = "The LongEMA from MACD Analyses after Calculate method should be"),
			InlineData(22.22, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 6, 10),
			InlineData(22.21, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 6, 10)
		]
		public void The_LongEMA_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, double[] closeValueCandle, int value1, int value2)
		{
			var config = new MACDConfig { EMA1 = value1, EMA2 = value2 };
			var candles = closeValueCandle.Select(a => (ICandle)new Candle() { Close = decimal.Parse(a.ToString()) }).ToList();
			var analysis = new CandleAnalyser { Previous = candles };

			var actual = new MACDAnalyser (config, analysis);
			Assert.Equal(expected, actual.LongEMA);
		}


		[
			Theory(DisplayName = "The ShortEMA from MACD Analyses after Calculate method should be"),
			InlineData(22.26, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 6, 10),
			InlineData(22.23, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 6, 10)
		]
		public void The_ShortEMA_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, double[] closeValueCandle, int value1, int value2)
		{
			var config = new MACDConfig { EMA1 = value1, EMA2 = value2 };
			var candles = closeValueCandle.Select(a => (ICandle)new Candle() { Close = decimal.Parse(a.ToString()) }).ToList();
			var analysis = new CandleAnalyser { Previous = candles };

			var actual = new MACDAnalyser(config, analysis);
			Assert.Equal(expected, actual.ShortEMA);
		}


		[
			Theory(DisplayName = "The MACD value from MACD Analyses after Calculate method should be"),
			InlineData(0.04, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 6, 10),
			InlineData(0.02, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 6, 10)
		]
		public void The_MACD_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, double[] closeValueCandle, int value1, int value2)
		{
			var config = new MACDConfig { EMA1 = value1, EMA2 = value2 };
			var candles = closeValueCandle.Select(a => (ICandle)new Candle() { Close = decimal.Parse(a.ToString()) }).ToList();
			var analysis = new CandleAnalyser { Previous = candles };

			var actual = new MACDAnalyser(config, analysis);
			Assert.Equal(expected, actual.MACD);
		}


		[
			Theory(DisplayName = "The Signal from MACD Analyses after Calculate method should be"),
			InlineData(0.04, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 6, 10, 2, 3),
			InlineData(0.02, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 6, 10, 2, 3)
		]
		public void The_Signal_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, double[] closeValueCandle, int value1, int value2, int value3, int referenceCandle)
		{
			var config = new MACDConfig { EMA1 = value1, EMA2 = value2, SignalEMA = value3 };
			var candles = closeValueCandle.Select(a => (ICandle)new Candle() { Close = decimal.Parse(a.ToString()) }).ToList();
			var analysis = new CandleAnalyser { Previous = candles };

			var actual = new MACDAnalyser(config, analysis, candles[referenceCandle]);
			Assert.Equal(expected, actual.Signal);
		}

		[
			Theory(DisplayName = "The Histogram from MACD Analyses after Calculate method should be"),
			InlineData(0.0, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 6, 10, 2, 3),
			InlineData(0.0, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 6, 10, 2, 3)
		]
		public void The_Histogram_from_MACDAnalyses_after_Calculate_method_should_be(decimal expected, double[] closeValueCandle, int value1, int value2, int value3, int referenceCandle)
		{
			var config = new MACDConfig { EMA1 = value1, EMA2 = value2, SignalEMA = value3 };
			var candles = closeValueCandle.Select(a => (ICandle)new Candle() { Close = decimal.Parse(a.ToString()) }).ToList();
			var analysis = new CandleAnalyser { Previous = candles };

			var actual = new MACDAnalyser(config, analysis, candles[referenceCandle]);
			Assert.Equal(expected, actual.Histogram);
		}

	}
}
