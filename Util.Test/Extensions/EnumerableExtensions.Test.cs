using System;
using System.Collections.Generic;
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
			Theory(DisplayName = "The sequence of all itens until value should be"),
			InlineData(new long[] { 1 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 1),
			InlineData(new long[] { 1, 2, 3, 4 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 4),
			InlineData(new long[] { 1, 2, 3, 4, 5, 6 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 6),
			InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 8),
			InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 9)
		]
		public void The_sequence_of_all_itens_until_value_should_be(long[] expected, long[] values, long value)
		{
			Assert.True(expected.SequenceEqual(values.ToList().TakeUntil(value)));
		}


		[
			Theory(DisplayName = "The sequence of n itens until value should be"),
			InlineData(new long[] { 1 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 1, 1),
			InlineData(new long[] { 3, 4 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 4, 2),
			InlineData(new long[] { 4, 5, 6 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 6, 3),
			InlineData(new long[] { 5, 6, 7, 8 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 8, 4),
			InlineData(new long[] { 5, 6, 7, 8, 9 }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 9, 5)
		]
		public void The_sequence_of_itens_until_value_should_be(long[] expected, long[] values, long value, int length)
		{
			Assert.True(expected.SequenceEqual(values.ToList().TakeUntil(value, length)));
		}

		[
			Theory(DisplayName = "The sequence of itens from index of count size should be"),
			InlineData(new long[] { }, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 0, 0),
			InlineData(new long[] { }, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 1, 0),
			InlineData(new long[] { }, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 11, 4),
			InlineData(new long[] { 0 }, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 0, 1),
			InlineData(new long[] { 1 }, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 1, 1),
			InlineData(new long[] { 4, 5 }, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 4, 2),
			InlineData(new long[] { 6, 7, 8 }, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 6, 3),
			InlineData(new long[] { 7, 8, 9, 10 }, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 7, 4),
			InlineData(new long[] { 9, 10 }, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 9, 4),
			

		]
		public void The_sequence_of_itens_from_index_of_count_size_should_be(long[] expected, long[] values, int index, int length)
		{
			var actual = values.ToList().SkipAndTake(index, length);
			Assert.True(expected.SequenceEqual(actual));
		}

		[
			Theory(DisplayName = "The object returned from with max should be"),
			InlineData(0, new long[] {}),
			InlineData(0, new long[] { 9, 9, 9 }),
			InlineData(2, new long[] { 1, 2, 3 }),
			InlineData(1, new long[] { 2, 3, 1 }),
			InlineData(0, new long[] { 3, 1, 2 }),
			InlineData(10, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }),
			InlineData(9, new long[] { 1, 1, 2, 3, 4, 5, 6, 7, 8, 10, 10 }),
			InlineData(0, new long[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }),
			InlineData(0, new long[] { 10, 10, 8, 7, 6, 5, 4, 3, 2, 1, 1 }),
			InlineData(3, new long[] { 3, 7, 1, 10, 4, 5, 9, 0, 2, 6, 8 })
		]
		public void The_object_returned_from_with_max_should_be(int expected, long[] values)
		{
			var actual = WithId(values).WithMax(v => v.value).id;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The object returned from with min should be"),
			InlineData(0, new long[] { }),
			InlineData(0, new long[] { 9, 9, 9 }),
			InlineData(0, new long[] { 1, 2, 3 }),
			InlineData(2, new long[] { 2, 3, 1 }),
			InlineData(1, new long[] { 3, 1, 2 }),
			InlineData(0, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }),
			InlineData(0, new long[] { 1, 1, 2, 3, 4, 5, 6, 7, 8, 10, 10 }),
			InlineData(10, new long[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }),
			InlineData(9, new long[] { 10, 10, 8, 7, 6, 5, 4, 3, 2, 1, 1 }),
			InlineData(7, new long[] { 3, 7, 1, 10, 4, 5, 9, 0, 2, 6, 8 })
		]
		public void The_object_returned_from_with_min_should_be(int expected, long[] values)
		{
			var actual = WithId(values).WithMin(v => v.value).id;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The index of max returned should be"),
			InlineData(-1, new long[] { }),
			InlineData(0, new long[] { 9, 9, 9 }),
			InlineData(2, new long[] { 1, 2, 3 }),
			InlineData(1, new long[] { 2, 3, 1 }),
			InlineData(0, new long[] { 3, 1, 2 }),
			InlineData(10, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }),
			InlineData(9, new long[] { 1, 1, 2, 3, 4, 5, 6, 7, 8, 10, 10 }),
			InlineData(0, new long[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }),
			InlineData(0, new long[] { 10, 10, 8, 7, 6, 5, 4, 3, 2, 1, 1 }),
			InlineData(3, new long[] { 3, 7, 1, 10, 4, 5, 9, 0, 2, 6, 8 })
		]
		public void The_index_of_max_returned_should_be(int expected, long[] values)
		{
			var actual = values.IndexOfMax(v => v);
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The index of min returned should be"),
			InlineData(-1, new long[] { }),
			InlineData(0, new long[] { 9, 9, 9 }),
			InlineData(0, new long[] { 1, 2, 3 }),
			InlineData(2, new long[] { 2, 3, 1 }),
			InlineData(1, new long[] { 3, 1, 2 }),
			InlineData(0, new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }),
			InlineData(0, new long[] { 1, 1, 2, 3, 4, 5, 6, 7, 8, 10, 10 }),
			InlineData(10, new long[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }),
			InlineData(9, new long[] { 10, 10, 8, 7, 6, 5, 4, 3, 2, 1, 1 }),
			InlineData(7, new long[] { 3, 7, 1, 10, 4, 5, 9, 0, 2, 6, 8 })
		]
		public void The_index_of_min_returned_should_be(int expected, long[] values)
		{
			var actual = values.IndexOfMin(v => v);
			Assert.Equal(expected, actual);
		}

		public IList<(int id, T value)> WithId<T>(IList<T> values)
		{
			var result = new List<(int id, T value)>();
			for (int i = 0; i < values.Count; i++)
			{
				result.Add((i, values[i]));
			}
			return result;
		}
	}
}
