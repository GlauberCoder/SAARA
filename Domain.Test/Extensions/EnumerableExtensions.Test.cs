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
			InlineData(22.22, 10, 10),
			InlineData(23.34, 20, 10),
			InlineData(23.43, 21, 10),
			InlineData(23.46, 22, 11),
			InlineData(23.41, 24, 12),
			InlineData(23.33, 26, 13),
			InlineData(23.21, 28, 14),
			InlineData(22.97, 30, 15)

		]
		public void The_EMA_Should_be_correct(decimal expected, int numberOfValues,  int length)
		{
			var values = EMASequence1.Take(numberOfValues).Select(v => decimal.Parse(v.ToString())).ToList();

			Assert.Equal(expected, values.EMA(length));
		}



		[
			Theory(DisplayName = "The EMA Should throw argument exception when the number of values are bellow the minimum"),
			InlineData(4, 10),
			InlineData(9, 10),
		]
		public void The_EMA_Should_Throw_Argument_Exception_When_The_Number_Of_Values_Are_Bellow_The_Minimum(int numberOfValues, int length)
		{
			Assert.Throws<ArgumentException>(() => EMASequence1.Take(numberOfValues).ToList().EMA(length));
		}
	}
}
