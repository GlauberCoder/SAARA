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
		public void The_EMA_Should_be_correct(decimal expected, int numberOfValues, int length)
		{
			var values = EMASequence1.Take(numberOfValues).Select(v => decimal.Parse(v.ToString())).ToList();

			Assert.Equal(expected, values.EMA(length));
		}


		[
			Theory(DisplayName = "The EMA Should throw argument exception when the number of values are bellow the minimum"),
			InlineData(4, 10),
			InlineData(9, 10)
		]
		public void The_EMA_Should_Throw_Argument_Exception_When_The_Number_Of_Values_Are_Bellow_The_Minimum(int numberOfValues, int length)
		{
			Assert.Throws<ArgumentException>(() => EMASequence1.Take(numberOfValues).ToList().EMA(length));
		}


		[
			Theory(DisplayName = "The EMA based on number should be correct"),
			InlineData(22.27, 22.38, 22.24, 10),
			InlineData(22.33, 22.61, 22.27, 10),
			InlineData(23.13, 23.83, 22.97, 10)
		]
		public void EMA_based_on_number_should_be_correct(decimal expected, decimal value, decimal previousEMA, int length)
		{
			var actual = decimal.Round(value.EMA(previousEMA, length), 2);
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "there are crosses between the lists"),
			InlineData(false, new double[] { 1, 2, 3, 4 }, new double[] { 2, 3, 4, 8 }),
			InlineData(true, new double[] { 8, 2, 3, 9 }, new double[] { 2, 3, 4, 8 }),
			InlineData(true, new double[] { 3, 3, 4, 7 }, new double[] { 2, 3, 4, 8 }),
			InlineData(false, new double[] { 8, 4, 4, 9 }, new double[] { 2, 3, 4, 8 })
		]
		public void There_are_crosses_between_lists(bool expected, double[] values, double[] otherValues)
		{
			var actual = values.HasCross(otherValues);
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "there should be returned the correct "),
			InlineData(false, new double[] { 0, 0, 0, 0 }),
			InlineData(false, new double[] { 1, 0, -1, -1 }),
			InlineData(true, new double[] { 0, 0, -1, 1 }),
			InlineData(true, new double[] { -1, 0, 0, 1 }),
			InlineData(false, new double[] { 0, 0, 0, 1 })
		]
		public void There_are_crosses_(bool expected, double[] values)
		{
			var actual = values.HasCross();
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The difference of lists should be correct"),
			InlineData(new double[] { -1, -1, -1, -4 }, new double[] { 1, 2, 3, 4 }, new double[] { 2, 3, 4, 8 })
		]
		public void The_difference_of_lists_should_be_correct(double [] expected, double[] values, double [] otherValues)
		{
			var actual = values.Difference(otherValues);
			Assert.Equal(expected, actual);
		}
	}
}
