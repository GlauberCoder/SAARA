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
			InlineData(22.22, 10, 10),
			InlineData(22.21, 11, 10),
			InlineData(22.24, 12, 10),
			InlineData(22.27, 13, 10),
			InlineData(22.33, 14, 10),
			InlineData(22.52, 15, 10),
			InlineData(22.80, 16, 10),
			InlineData(22.97, 17, 10),
			InlineData(23.13, 18, 10),
			InlineData(23.28, 19, 10),
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
			InlineData(22.90, 30, 10)
		]
		public void The_EMA_from_candle_list_should_be(decimal expected, int numberOfValues, int length)
		{
			var candles = EMASequence1.Take(numberOfValues).Select(a => (ICandle)new Candle() { Close = decimal.Parse(a.ToString()) }).ToList();
			var actual = new CandleAnalyser { Previous = candles }.EMA(length);
			Assert.Equal(expected, actual);
		}

	}
}
