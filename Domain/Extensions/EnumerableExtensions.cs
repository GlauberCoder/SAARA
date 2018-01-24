using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using Domain.Abstractions;
using Domain.Abstractions.Entitys;

namespace Domain.Extensions
{
    public static class EnumerableExtensions
	{
		public static decimal EMA(this IList<ICandle> candles, int length)
		{
			return candles.TakeLast(length * 2).Select(c => c.Close).ToList().EMA(length);
		}
		public static decimal EMA(this IList<decimal> values, int length)
		{
			var matureValues = values.Skip(length);
			var ema = values.Take(length).Average();
			decimal k = 2m / (length + 1);

			foreach (var value in matureValues)
				ema = k * (value - ema) + ema;

			return decimal.Round(ema, 2);
		}

		public static IList<T> TakePrevious<T>(this IList<T> values, T value, int length)
		{
			var index = values.IndexOf(value);

			if (index <= 0) return new List<T>();

			return values.Take(index).TakeLast(length).ToList(); 
		}

		public static T TakePrevious<T>(this IList<T> values, T value)
		{
			var index = values.IndexOf(value);
			return index > 0 ? values[index-1] : default(T);
		}

	}
}

