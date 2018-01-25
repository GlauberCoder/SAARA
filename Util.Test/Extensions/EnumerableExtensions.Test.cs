using System;
using System.Linq;
using Util.Extensions;
using Xunit;

namespace Infra.Test
{
	public class EnumerableExtensions
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
			Theory(DisplayName = "The sequence of all previous itens should be"),
			InlineData(new long[] { 1 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 1),
			InlineData(new long[] { 1, 2, 3, 4 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 4),
			InlineData(new long[] { 1, 2, 3, 4, 5, 6 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 6),
			InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 8),
			InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 9)


		]
		public void The_sequence_of_all_previous_itens_should_be(long[] expected, long[] values, long value)
		{

			Assert.True(expected.SequenceEqual(values.ToList().TakeAllPrevious(value)));
		}
	}
}
