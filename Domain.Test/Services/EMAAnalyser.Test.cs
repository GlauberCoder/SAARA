using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
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
	public class EMAAnalyserTest
	{
		[
			Theory(DisplayName = "The EMA Trade Signal should be"),
			InlineData(TradeSignal.Short, 10.0, 30.0),
			InlineData(TradeSignal.Short, 4, 4),
			InlineData(TradeSignal.Long, 30.0, 10.11)
		]
		public void The_EMA_Trade_Signal_should_be(TradeSignal expected, decimal value1, decimal value2)
		{
			var actual = new EMAAnalyser {ShortEMA = value1, LongEMA = value2 }.Signal;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The ShortEMA after Calculate method should be"),
			InlineData(22.26, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 6, 10),
			InlineData(22.23, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 6, 10)

		]
		public void The_ShortEMA_after_Calculate_method_should_be(decimal expected, double[] closeValueCandle, int value1, int value2)
		{
			var config = new EMAConfig { EMA1 = value1, EMA2 = value2 };
			var candles = closeValueCandle.Select(a => (ICandle) new Candle() { Close = decimal.Parse(a.ToString()) }).ToList();
			var analysis = new CandleAnalyser { Previous = candles };

			var actual = new EMAAnalyser(config, analysis);
			Assert.Equal(expected, actual.ShortEMA);
		}


		[
			Theory(DisplayName = "The LongEMA after Calculate method should be"),
			InlineData(22.22, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 6, 10),
			InlineData(22.21, new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 6, 10)
		]
		public void The_LongEMA_after_Calculate_method_should_be(decimal expected, double[] closeValueCandle, int value1, int value2)
		{
			var config = new EMAConfig { EMA1 = value1, EMA2 = value2 };
			var candles = closeValueCandle.Select(a => (ICandle) new Candle() { Close = decimal.Parse(a.ToString()) }).ToList();
			var analysis = new CandleAnalyser { Previous = candles };

			var actual = new EMAAnalyser(config, analysis);
			Assert.Equal(expected, actual.LongEMA);
		}

	}
}
