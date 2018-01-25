using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
using Domain.Entitys.AnalisysConfig;
using Domain.Services;
using System;
using System.Linq;
using Xunit;

namespace Domain.Test.Services
{
	public class EMAAnalyserTest
	{
		decimal[] CloseCandleValues => new decimal[] { 22.27m, 22.19m, 22.08m, 22.17m, 22.18m, 22.13m, 22.23m, 22.43m, 22.24m, 22.29m, 22.15m, 22.39m, 22.38m, 22.61m, 23.36m, 24.05m, 23.75m, 23.83m, 23.95m, 23.63m, 23.82m, 23.87m, 23.65m, 23.19m, 23.10m, 23.33m, 22.68m, 23.10m, 22.40m, 22.17m };

		private decimal [] GetCloseCandleValues(int length)
		{
			return CloseCandleValues.Take(length).ToArray();
		}

		private EMAConfig GetConfig(int shortEMA, int longEMA)
		{
			return new EMAConfig { EMA1 = shortEMA, EMA2 = longEMA };
		}

		private CandleAnalyser GetCandleAnalyser(decimal[] closeValueCandle)
		{
			var i = 1;
			var candles = closeValueCandle.Select(a => new Candle() { ID = i++, Close = a }).ToList<ICandle>();
			return new CandleAnalyser { Previous = candles };
		}

		private EMAAnalyser GenerateCandleAnalyser(decimal[] closeValueCandle, int shortEMA, int longEMA)
		{
			var config = GetConfig(shortEMA, longEMA);
			var analyser = GetCandleAnalyser(closeValueCandle);

			return new EMAAnalyser(config, analyser);
		}

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
			InlineData(23.59, 20, 6, 10),
			InlineData(23.66, 21, 6, 10),
			InlineData(23.72, 22, 6, 10),
			InlineData(23.70, 23, 6, 10),
			InlineData(23.55, 24, 6, 10),
			InlineData(23.60, 21, 7, 10),
			InlineData(23.54, 21, 8, 10),
			InlineData(23.48, 21, 9, 10)

		]
		public void The_ShortEMA_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA)
		{
			var actual = GenerateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA ).ShortEMA;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The LongEMA after Calculate method should be"),
			InlineData(23.34, 20, 6, 10),
			InlineData(23.43, 21, 6, 10),
			InlineData(23.51, 22, 6, 10),
			InlineData(23.53, 23, 6, 10),
			InlineData(23.47, 24, 6, 10),
			InlineData(23.46, 22, 6, 11),
			InlineData(23.41, 24, 6, 12),
			InlineData(23.33, 26, 6, 13)
		]
		public void The_LongEMA_after_Calculate_method_should_be(decimal expected, int candleCount, int shortEMA, int longEMA)
		{
			var actual = GenerateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA).LongEMA;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The_EMAAnalyser_should_raise_exception when candle count is not double of longEMA"),
			InlineData(5, 6, 10),
			InlineData(9, 6, 10)
		]
		public void The_EMAAnalyser_should_raise_exception_when_candlecount_is_not_double_of_longEMA(int candleCount, int shortEMA, int longEMA)
		{
			var config = GetConfig(shortEMA, longEMA);
			var closeValueCandle = GetCloseCandleValues(candleCount);
			var analyser = GetCandleAnalyser(closeValueCandle);

			Assert.Throws<ArgumentException>((Action)(() => new EMAAnalyser(config, analyser)));
		}
	}
}
