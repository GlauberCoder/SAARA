using System;
using System.Linq;
using Xunit;
using Domain.Extensions;
using Util.Extensions;
using System.Collections.Generic;

namespace Domain.Test
{
	public class EnumerableExtensionsTest
	{
		private IList<decimal> EMASequence1 => new decimal[] { 22.27m, 22.19m, 22.08m, 22.17m, 22.18m, 22.13m, 22.23m, 22.43m, 22.24m, 22.29m, 22.15m, 22.39m, 22.38m, 22.61m, 23.36m, 24.05m, 23.75m, 23.83m, 23.95m, 23.63m, 23.82m, 23.87m, 23.65m, 23.19m, 23.10m, 23.33m, 22.68m, 23.10m, 22.40m, 22.17m };

		[
			Theory(DisplayName = "The EMA Should be correct"),
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
		public void The_EMA_Should_be_correct(decimal expected, int numberOfValues,  int length)
		{
			var values = EMASequence1.Take(numberOfValues).Select(v => decimal.Parse(v.ToString())).ToList();

			Assert.Equal(expected, values.EMA(length));
		}



		[
			Theory(DisplayName = "The EMA Should throw argument exception when the number of values are bellow the minimum"),
			InlineData(10, 10),
			InlineData(11, 10),
			InlineData(12, 10),
			InlineData(13, 10),
			InlineData(14, 10),
			InlineData(15, 10),
			InlineData(16, 10),
			InlineData(17, 10),
			InlineData(18, 10),
			InlineData(19, 10)
		]
		public void The_EMA_Should_Throw_Argument_Exception_When_The_Number_Of_Values_Are_Bellow_The_Minimum(int numberOfValues, int length)
		{
			Assert.Throws<ArgumentException>(() => EMASequence1.Take(numberOfValues).ToList().EMA(length));
		}
	}
}
