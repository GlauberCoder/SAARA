using Domain.Abstractions.Entitys;
using System.Collections.Generic;
using System.Linq;

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
			return values.ToDecimalList().EMA(length);
		}
		public static decimal EMA(this IList<decimal> values, int length)
		{
			var minNumberOfValues = length;

			if (values.Count < minNumberOfValues)
				throw new ArgumentException("Number of values is bellow to the minimal for this length.");

			var matureValues = values.Skip(length);
			var ema = values.Take(length).Average();

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
			return values.Sum(otherValues.Select(c=> -1*c).ToList<decimal>());
		}
		public static IList<decimal> Sum(this IList<decimal> values, IList<decimal> otherValues)
		{
			if (values.Count != otherValues.Count)
				return new List<decimal>();

			var result = new List<decimal>();
			for (int i = 0; i < values.Count; i++)
				result.Add(values[i] + otherValues[i]);
			return result;
		}

		public static bool HasCross(this IList<double> values, IList<double> otherValues)
		{
			var _values = values.ToDecimalList();
			var _otherValues = otherValues.ToDecimalList();
			return _values.HasCross(_otherValues);
		}
		public static bool HasCross(this IList<double> values)
		{
			var _values = values.ToDecimalList();

			return _values.HasCross();
		}
		public static IList<double> Difference(this IList<double> values, IList<double> otherValues)
		{
			var _values = values.ToDecimalList();
			var _otherValues = otherValues.ToDecimalList();
			return _values.Difference(_otherValues).ToDoubleList();
		}
		public static IList<double> Sum(this IList<double> values, IList<double> otherValues)
		{
			var _values = values.ToDecimalList();
			var _otherValues = otherValues.ToDecimalList();
			return _values.Sum(_otherValues).ToDoubleList();
		}
		public static IList<decimal> LinearLeastSquare(this IList<decimal> values)
		{
			var x = Enumerable.Range(1, values.Count).ToDecimalList();
			return values.LinearLeastSquare(x);
		}
		public static IList<decimal> LinearLeastSquare(this IList<decimal> y, IList<decimal> x)
		{
			if (x.Count != y.Count)
				return new List<decimal>();

			var x2 = x.Select(c => c * c);
			var y2 = y.Select(c => c * c);
			var xy = x.Zip(y, (a, b) => a * b);
			var n = x.Count;

			var a0 = ((x2.Sum() * y.Sum()) - (xy.Sum() * x.Sum())) / (n * x2.Sum() - x.Sum()* x.Sum());
			var a1 = (n * xy.Sum() - x.Sum() * y.Sum()) / (n * x2.Sum() - x.Sum()* x.Sum());

			return new List<decimal> { a0, a1 };
		}

		public static decimal Function(this IList<decimal> coefficients, decimal x)
		{
			var result = 0m;
			foreach (var value in coefficients.Reverse())
				result = result * x + value;

			return result;
		}

		public static IList<double> ToDoubleList(this IEnumerable<decimal> values)
		{
			return values.Select(c => double.Parse(c.ToString())).ToList<double>();
		}
		public static IList<decimal> ToDecimalList(this IEnumerable<double> values)
		{
			return values.Select(c => decimal.Parse(c.ToString())).ToList<decimal>();
		}
		public static IList<decimal> ToDecimalList(this IEnumerable<int> values)
		{
			return values.Select(c => decimal.Parse(c.ToString())).ToList<decimal>();
		}
	}
}

