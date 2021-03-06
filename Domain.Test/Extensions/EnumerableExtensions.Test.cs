using System;
using System.Linq;
using Xunit;
using Domain.Extensions;

namespace Domain.Test
{
	public class EnumerableExtensionsTest
	{
		[
			Theory(DisplayName = "The sequence of previous itens should be"),
			InlineData(new long[] { 1 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 2, 1),
			InlineData(new long[] { 6, 7 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 8, 2),
			InlineData(new long[] { 7, 8, 9 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10, 3),
			InlineData(new long[] { 1, 2, 3, 4 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 5, 4),
			InlineData(new long[] { 1, 2, 3, 4 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 5, 5),
			InlineData(new long[] { 4, 5, 6, 7, 8, 9 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10, 6),
			InlineData(new long[] { 1, 2, 3, 4, 5, 6 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 7, 7),
			InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 9, 8),
			InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10, 9),
			InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10, 10)
		]
		public void The_sequence_of_previous_itens_should_be(long[] expected, long[] values, long value, int quantity)
		{
			Assert.True(expected.SequenceEqual(values.ToList().TakePrevious(value, quantity)));
		}




		[
			Theory(DisplayName = "The EMA Should be correct"),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29 }, 22.22, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15 }, 22.21, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39 }, 22.24, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38 }, 22.27, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61 }, 22.33, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36 }, 22.52, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05 }, 22.80, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75 }, 22.97, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83 }, 23.13, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83, 23.95 }, 23.28, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83, 23.95, 23.63 }, 23.34, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83, 23.95, 23.63, 23.82 }, 23.43, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83, 23.95, 23.63, 23.82, 23.87 }, 23.51, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83, 23.95, 23.63, 23.82, 23.87, 23.65 }, 23.53, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83, 23.95, 23.63, 23.82, 23.87, 23.65, 23.19 }, 23.47, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83, 23.95, 23.63, 23.82, 23.87, 23.65, 23.19, 23.10 }, 23.40, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83, 23.95, 23.63, 23.82, 23.87, 23.65, 23.19, 23.10, 23.33 }, 23.39, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83, 23.95, 23.63, 23.82, 23.87, 23.65, 23.19, 23.10, 23.33, 22.68 }, 23.26, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83, 23.95, 23.63, 23.82, 23.87, 23.65, 23.19, 23.10, 23.33, 22.68, 23.10 }, 23.23, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83, 23.95, 23.63, 23.82, 23.87, 23.65, 23.19, 23.10, 23.33, 22.68, 23.10, 22.40 }, 23.08, 10),
			InlineData(new double[] { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05, 23.75, 23.83, 23.95, 23.63, 23.82, 23.87, 23.65, 23.19, 23.10, 23.33, 22.68, 23.10, 22.40, 22.17 }, 22.92, 10),
		]
		public void The_EMA_Should_be_correct(double[] doubleValues, decimal expected, int length)
		{
			var values = doubleValues.Select(v => decimal.Parse(v.ToString())).ToList();

			Assert.Equal(expected, values.EMA(length));
		}
	}
}
