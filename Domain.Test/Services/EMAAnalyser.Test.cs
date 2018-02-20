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
		decimal[] CloseCandleValues => new decimal[] { 22.27m, 22.19m, 22.08m, 22.17m, 22.18m, 22.13m, 22.23m, 22.43m, 22.24m, 22.29m, 22.15m, 22.39m, 22.38m, 22.61m, 23.36m, 24.05m, 23.75m, 23.83m, 23.95m, 23.63m, 23.82m, 23.87m, 23.65m, 23.19m, 23.10m, 23.33m, 22.68m, 23.10m, 22.40m, 22.17m };

		private decimal [] GetCloseCandleValues(int length)
		{
			return CloseCandleValues.Take(length).ToArray();
		}

		private EMAConfig GetConfig(int shortEMA = 0, int longEMA = 0)
		{
			return new EMAConfig { EMA1 = shortEMA, EMA2 = longEMA };
		}

		private EMAConfig GetConfig( decimal crossoverTolerance = 0, decimal crossunderTolerance = 0)
		{
			return new EMAConfig { CrossunderTolerance = crossunderTolerance, CrossoverTolerance = crossunderTolerance };
		}


		private EMAConfig GetConfig(int shortEMA = 0, int longEMA = 0, decimal crossoverTolerance = 0, decimal crossunderTolerance = 0)
		{
			return new EMAConfig { EMA1 = shortEMA, EMA2 = longEMA, CrossunderTolerance = crossunderTolerance, CrossoverTolerance = crossunderTolerance };
		}

		private CandleAnalyser GetCandleAnalyser(decimal[] closeValueCandle)
		{
			var i = 1;
			var candles = closeValueCandle.Select(a => new Candle() { ID = i++, Close = a }).ToList<ICandle>();
			return new CandleAnalyser { Previous = candles };
		}

		private EMAAnalyser generateCandleAnalyser(decimal[] closeValueCandle, int shortEMA, int longEMA)
		{
			var config = GetConfig(shortEMA, longEMA);
			var analyser = GetCandleAnalyser(closeValueCandle);

			return new EMAAnalyser(config, analyser);
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
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA ).ShortEMA;
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
			var actual = generateCandleAnalyser( GetCloseCandleValues(candleCount), shortEMA, longEMA).LongEMA;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The_EMAAnalyser_should_raise_exception when candle count is not greater than longEMA"),
			InlineData(5, 6, 10),
			InlineData(9, 6, 10)
		]
		public void The_EMAAnalyser_should_raise_exception_when_candlecount_is_not_greater_than_longEMA(int candleCount, int shortEMA, int longEMA)
		{
			var config = GetConfig(shortEMA, longEMA);
			var closeValueCandle = GetCloseCandleValues(candleCount);
			var analyser = GetCandleAnalyser(closeValueCandle);

			Action actual = () => new EMAAnalyser(config, analyser);
			Assert.Throws<ArgumentException>(actual);
		}


		[
			Theory(DisplayName = "The EMA trend should be"),
			InlineData(Trend.Down, 10.0, 30.0),
			InlineData(Trend.Down, 4, 4),
			InlineData(Trend.Up, 30.0, 10.11),
			InlineData(Trend.Up, 30, 10)
		]
		public void The_EMA_trend_should_be(Trend expected, decimal shortEMA, decimal longEMA)
		{
			var actual = new EMAAnalyser().CalculateTrend(shortEMA, longEMA);
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The EMA cross signal should be"),
			InlineData(TradeSignal.Hold, 0, 0, 0, Trend.Neutral),
			InlineData(TradeSignal.Long, 30, 29.8, 1, Trend.Up),
			InlineData(TradeSignal.Hold, 30, 28, 1, Trend.Up),
			InlineData(TradeSignal.Long, 11110, 11101, 1, Trend.Up),
			InlineData(TradeSignal.Hold, 11111, 10000, 1, Trend.Up),
			InlineData(TradeSignal.Short, 29.8, 30, 1, Trend.Down),
			InlineData(TradeSignal.Hold, 28, 30, 1, Trend.Down),
			InlineData(TradeSignal.Short, 11101, 11110, 1, Trend.Down),
			InlineData(TradeSignal.Hold, 10000, 11111, 1, Trend.Down)
		]
		public void The_EMA_cross_signal_should_be(TradeSignal expected, decimal shortEMA, decimal longEMA, decimal tolerance, Trend trend)
		{
			var config = GetConfig(tolerance, tolerance);
			var actual = new EMAAnalyser().CalculateCrossSignal(config, shortEMA, longEMA, trend);
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The EMA average distance signal should be"),
			InlineData(TradeSignal.Hold, 0, 0, 0, Trend.Neutral),
			InlineData(TradeSignal.Hold, 30, 26, 15, Trend.Up),
			InlineData(TradeSignal.Hold, 30, 25, 15, Trend.Up),
			InlineData(TradeSignal.Long, 11111, 10000, 15, Trend.Up),
			InlineData(TradeSignal.Hold, 11111,  9400, 15, Trend.Up),
			InlineData(TradeSignal.Hold, 26, 30, 15, Trend.Down),
			InlineData(TradeSignal.Hold, 25, 30, 15, Trend.Down),
			InlineData(TradeSignal.Short, 10000, 11111, 15, Trend.Down),
			InlineData(TradeSignal.Hold, 9400, 11111, 15, Trend.Down)
		]
		public void The_EMA_average_distance_signal_should_be(TradeSignal expected, decimal price, decimal shortEMA, decimal tolerance, Trend trend)
		{
			var config = new EMAConfig { AverageDistanceTolerance = tolerance};
			var actual = new EMAAnalyser().CalculateAverageDistanceSignal(config, price, shortEMA, trend);
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The EMA Trade Signal should be"),
			InlineData(TradeSignal.Hold, TradeSignal.Hold, TradeSignal.Short),
			InlineData(TradeSignal.Hold, TradeSignal.Hold, TradeSignal.Long),
			InlineData(TradeSignal.Hold, TradeSignal.Hold, TradeSignal.Hold),
			InlineData(TradeSignal.StrongShort, TradeSignal.Short, TradeSignal.Short),
			InlineData(TradeSignal.WeakShort, TradeSignal.Short, TradeSignal.Long),
			InlineData(TradeSignal.WeakShort, TradeSignal.Short, TradeSignal.Hold),
			InlineData(TradeSignal.StrongLong, TradeSignal.Long, TradeSignal.Long),
			InlineData(TradeSignal.WeakLong, TradeSignal.Long, TradeSignal.Short),
			InlineData(TradeSignal.WeakLong, TradeSignal.Long, TradeSignal.Hold)
		]
		public void The_EMA_Trade_Signal_should_be(TradeSignal expected, TradeSignal crossSignal, TradeSignal averageDistanceSignal)
		{
			var actual = new EMAAnalyser().CalculateSignal(crossSignal, averageDistanceSignal);
			Assert.Equal(expected, actual);
		}

	}
}
