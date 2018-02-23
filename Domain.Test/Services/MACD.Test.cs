using Domain.Abstractions.Services;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Domain.Abstractions.Enums;

namespace Domain.Test.Services
{
	public class MACDTest
	{
		decimal[] values => new decimal[] { 0.25m, 0.23m, 0.21m, 0.17m, 0.08m, 0.26m, 0.14m, 0.07m, 0.17m, 0.11m, 0.05m };
		List<IMACD> Macds
		{
			get
			{
				var macds = new List<IMACD>();
				foreach (var value in values)
					macds.Add(new MACD { Value = value });
				return macds;
			}
		}

		[
			Theory(DisplayName = "The MACD value from short and long EMA should be"),
			InlineData(-2.78, 23.45, 26.23),
			InlineData(0.14, 23.47, 23.33),
			InlineData(20.66, 43.67, 23.01),
			InlineData(-9.57, 14.62, 24.19),
			InlineData(6.21, 25.12, 18.91)
		]
		public void The_MACD_value_from_short_and_long_EMA_should_be(decimal expected, decimal shortEMA, decimal longEMA)
		{
			var actual = new MACD { ShortEMA = shortEMA, LongEMA = longEMA }.CalculateValue().Value;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The Signal from macds and length should be"),
			InlineData(0.20, 6, 3),
			InlineData(0.17, 7, 3),
			InlineData(0.12, 8, 3),
			InlineData(0.14, 9, 3),
			InlineData(0.13, 10, 3)

		]
		public void The_Signal_from_macds_and_length_should_be(decimal expected, int index, int length)
		{
			var actual = new MACD().CalculateSignal(Macds.Take(index).ToList(), length).Signal;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The Histogram from macds and length should be"),
			InlineData(-2.98, 6, 3, 23.45, 26.23),
			InlineData(-0.03, 7, 3, 23.47, 23.33),
			InlineData(20.54, 8, 3, 43.67, 23.01),
			InlineData(-9.71, 9, 3, 14.62, 24.19),
			InlineData(6.08, 10, 3, 25.12, 18.91)

		]
		public void The_Histrogram_from_macd_and_signal_EMA_should_be(decimal expected, int index, int length, decimal shortEMA, decimal longEMA)
		{
			var actual = new MACD { ShortEMA = shortEMA, LongEMA = longEMA }
					.CalculateValue()
					.CalculateSignal(Macds.Take(index).ToList(), length)
					.CalculateHistogram()
					.Histogram;

			Assert.Equal(expected, actual);
		}
		[
			Theory(DisplayName = "The Trend from macd and signal should be"),
			InlineData(Trend.Up, 6.0, 3.0),
			InlineData(Trend.Neutral, 7.0, 7.0),
			InlineData(Trend.Down, 8.0, 9.0),
			InlineData(Trend.Down, 2.0, 3.0),
			InlineData(Trend.Up, 4.0, 3.0)

		]
		public void The_Trend_from_macd_and_signal_should_be(Trend expected, decimal value, decimal signal)
		{
			var actual = new MACD() { Value = value, Signal = signal}.Trend;
			Assert.Equal(expected, actual);
		}
		[
			Theory(DisplayName = "The TradeSignal from trend should be"),
			InlineData(TradeSignal.Long, 6.0, 3.0),
			InlineData(TradeSignal.Short, 7.0, 7.0),
			InlineData(TradeSignal.Short, 8.0, 9.0),
			InlineData(TradeSignal.Short, 1.0, 3.0),
			InlineData(TradeSignal.Long, 4.0, 3.0),
		]
		public void The_TradeSignal_from_trend_should_be(TradeSignal expected, decimal value, decimal signal)
		{
			var actual = new MACD() { Value = value, Signal = signal }.TradeSignal;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The Cross Trade Signal from trend should be"),
			InlineData(TradeSignal.StrongLong, 6.0, 3.0),
			InlineData(TradeSignal.WeakShort, 7.0, 7.0),
			InlineData(TradeSignal.WeakShort, 6.0, 9.0),
			InlineData(TradeSignal.WeakLong, -1.0, -3.0),
			InlineData(TradeSignal.StrongShort, -4.0, -3.0),
		]
		public void The_CrossTradeSignal_from_trend_should_be(TradeSignal expected, decimal value, decimal signal)
		{
			var actual = new MACD() { Value = value, Signal = signal }.CrossTradeSignal;
			Assert.Equal(expected, actual);
		}
	}
}
