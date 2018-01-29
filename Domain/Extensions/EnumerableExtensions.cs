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
			return candles.Select(c => c.Close).ToList().EMA(length);
		}
		public static decimal EMA(this IList<double> values, int length)
		{
			return values.Select(c => decimal.Parse(c.ToString())).ToList().EMA(length);
		}
		public static decimal EMA(this IList<decimal> values, int length)
		{
			var minNumberOfValues = length;

			if (values.Count < minNumberOfValues)
				throw new ArgumentException("Number of values is bellow to the minimal for this length.");

			var matureValues = values.Skip(length);
			var ema = values.Take(length).Average();
			decimal k = 2m / (length + 1);

			foreach (var value in matureValues)
				ema = value.EMA(ema, length);

			return decimal.Round(ema, 2);
		}
		public static decimal EMA(this decimal value, decimal previousEMA, int length)
		{
			decimal k = 2m / (length + 1);
			return k * (value - previousEMA) + previousEMA;
		}
		public static bool HasCross(this IList<decimal> values, IList<decimal> otherValues)
		{
			if (values.Count == otherValues.Count)
				return values.Difference(otherValues).HasCross();
			return false;
		}
		public static bool HasCross(this IList<decimal> values)
		{
			foreach(var value in values.Take(values.Count-1).Reverse())
			{
				if (value * values.Last() < 0)
					return true;
				if (value * values.Last() > 0)
					return false;
			}
			return false;
		}
		public static IList<decimal> Difference(this IList<decimal> values, IList<decimal> otherValues)
		{
			if (values.Count != otherValues.Count)
				return new List<decimal>();

			var result = new List<decimal>(values.Count);
			for (int i = 0; i < values.Count; i++)
					result.Add(values[i] - otherValues[i]);
			return result;
		}

		public static bool HasCross(this IList<double> values, IList<double> otherValues)
		{
			var _values = values.Select(c => decimal.Parse(c.ToString())).ToList<decimal>();
			var _otherValues = otherValues.Select(c => decimal.Parse(c.ToString())).ToList<decimal>();
			return _values.HasCross(_otherValues);
		}
		public static bool HasCross(this IList<double> values)
		{
			var _values = values.Select(c => decimal.Parse(c.ToString())).ToList<decimal>();

			return _values.HasCross();
		}
		public static IList<double> Difference(this IList<double> values, IList<double> otherValues)
		{
			var _values = values.Select(c => decimal.Parse(c.ToString())).ToList<decimal>();
			var _otherValues = otherValues.Select(c => decimal.Parse(c.ToString())).ToList<decimal>();
			return _values.Difference(_otherValues).Select(c => double.Parse(c.ToString())).ToList<double>();
		}
	}
}

