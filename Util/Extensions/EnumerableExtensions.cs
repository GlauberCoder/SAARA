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

		public static IList<T> TakeUntil<T>(this IList<T> values, T value)
		{
			var index = values.IndexOf(value);

			if (index < 0) return new List<T>();

			return values.Take(index + 1).ToList();
		}

		public static IList<T> TakeUntil<T>(this IList<T> values, T value, int length)
		{
			var index = values.IndexOf(value);

			if (index < 0) return new List<T>();

			return values.Take(index + 1).TakeLast(length).ToList();
		}
		public static T ElementAtBackwardIndex<T>(this IList<T> values, int backwardIndex)
		{
			var index = values.Count - backwardIndex - 1;
			return values.ElementAt(index);
		}

		public static IList<T> SkipAndTake<T>(this IList<T> values, int skip, int take)
		{
			return values.Skip(skip).Take(take).ToList();
		}
		public static T WithMax<T, TKey>(this IList<T> source, Func<T,TKey> selector)
		{
			return source != null && source.Any() ? source.OrderByDescending(selector).FirstOrDefault() : default(T);
		}
		public static T WithMin<T, TKey>(this IList<T> source, Func<T, TKey> selector)
		{
			return source != null && source.Any() ? source.OrderBy(selector).FirstOrDefault() : default(T);
		}
		public static int IndexOfMax<T, TKey>(this IList<T> source, Func<T, TKey> selector)
		{
			return source.IndexOf(source.WithMax(selector));
		}
		public static int IndexOfMin<T, TKey>(this IList<T> source, Func<T, TKey> selector)
		{
			return source.IndexOf(source.WithMin(selector));
		}
		public static T WithMax<T, TKey>(this IList<T> source, Func<T, TKey> selector, int index)
		{
			return source != null && source.Any() ? source.OrderByDescending(selector).ElementAtOrDefault(index) : default(T);
		}
		public static T WithMin<T, TKey>(this IList<T> source, Func<T, TKey> selector, int index)
		{
			return source != null && source.Any() ? source.OrderBy(selector).ElementAtOrDefault(index) : default(T);
		}
		public static int IndexOfMax<T, TKey>(this IList<T> source, Func<T, TKey> selector, int index)
		{
			return source.IndexOf(source.WithMax(selector, index));
		}
		public static int IndexOfMin<T, TKey>(this IList<T> source, Func<T, TKey> selector, int index)
		{
			return source.IndexOf(source.WithMin(selector, index));
		}
		public static IList<T> Add<T>(this IList<T> list, params T[] items )
		{
			return list.Concat(items).ToList();
		}
		public static IList<T> Foreach<T>(this IList<T> list, Action<T> action)
		{
			foreach (var item in list) action(item);
			return list;
		}

		public static IList<TResult> SelectList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			return source.Select(selector).ToList();
		}
		public static IList<TResult> SelectList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
		{
			return source.Select(selector).ToList();
		}
	}
}
