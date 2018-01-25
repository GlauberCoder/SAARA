using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace Util.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool Empty<T>(this IList<T> list)
		{
			return list == null || list.Count == 0;
		}
		public static bool NotEmpty<T>(this IList<T> list)
		{
			return !list.Empty();
		}

		public static IList<T> TakePrevious<T>(this IList<T> values, T value, int length)
		{
			var index = values.IndexOf(value);

			if (index < 0) return new List<T>();

			return values.Take(index).TakeLast(length).ToList();
		}

		public static T TakePrevious<T>(this IList<T> values, T value)
		{
			var index = values.IndexOf(value);
			return index > 0 ? values[index - 1] : default(T);
		}

		public static IList<T> TakeAllPrevious<T>(this IList<T> values, T value)
		{
			var index = values.IndexOf(value);

			if (index < 0) return new List<T>();

			return values.Take(index + 1).ToList();
		}
	}
}
