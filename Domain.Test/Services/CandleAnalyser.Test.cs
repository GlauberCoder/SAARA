using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Domain.Entitys;
using Domain.Abstractions.Entitys;

namespace Domain.Test.Services
{
	public class CandleAnalyserTest
	{
		private IEnumerable<decimal> EMASequence1 => new decimal[] { 22.27m, 22.19m, 22.08m, 22.17m, 22.18m, 22.13m, 22.23m, 22.43m, 22.24m, 22.29m, 22.15m, 22.39m, 22.38m, 22.61m, 23.36m, 24.05m, 23.75m, 23.83m, 23.95m, 23.63m, 23.82m, 23.87m, 23.65m, 23.19m, 23.10m, 23.33m, 22.68m, 23.10m, 22.40m, 22.17m };


		[
			Theory(DisplayName = "The EMA from Candle List should be"),
			InlineData(23.34, 20, 10),
			InlineData(23.43, 21, 10),
			InlineData(23.51, 22, 10),
			InlineData(23.53, 23, 10),
			InlineData(23.47, 24, 10),
			InlineData(23.39, 25, 10),
			InlineData(23.37, 26, 10),
			InlineData(23.23, 27, 10),
			InlineData(23.20, 28, 10),
			InlineData(23.05, 29, 10),
			InlineData(23.46, 22, 11),
			InlineData(23.41, 24, 12),
			InlineData(23.33, 26, 13),
			InlineData(23.21, 28, 14),
			InlineData(22.97, 30, 15)
		]
		public void The_EMA_from_candle_list_should_be(decimal expected, int numberOfValues, int length)
		{
			var candles = EMASequence1.Take(numberOfValues).Select(a => (ICandle)new Candle() { Close = decimal.Parse(a.ToString()) }).ToList();
			var actual = new CandleAnalyser { Previous = candles }.EMA(length);
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The EMA from Candle List should raise exception"),
			InlineData(9, 10),
			InlineData(4, 10),
			InlineData(19, 10)

		]
		public void The_EMA_from_candle_list_should_raise_exception(int numberOfValues, int length)
		{
			var candles = EMASequence1.Take(numberOfValues).Select(a => (ICandle)new Candle() { Close = decimal.Parse(a.ToString()) }).ToList();
			var actual = new CandleAnalyser { Previous = candles };
			Assert.Throws<ArgumentException>(() => actual.EMA(length));
		}
		

	}
}
