using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using System;
using System.Collections;
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
		public static decimal EMA(this IList<decimal> values, int length, int precision = 2)
		{
			var minNumberOfValues = length;

			if (values.Count < minNumberOfValues)
				throw new ArgumentException("Number of values is bellow to the minimal for this length.");

			var matureValues = values.Skip(length);
			var ema = values.Take(length).Average();

			foreach (var value in matureValues)
				ema = value.EMA(ema, length);

			return decimal.Round(ema, precision);
		}
		public static decimal EMA(this decimal value, decimal previousEMA, int length)
		{
			decimal k = 2m / (length + 1);
			return k * (value - previousEMA) + previousEMA;
		}
		public static bool LastValueIsCrossing(this IList<decimal> values, IList<decimal> otherValues)
		{
			if (values.Count == otherValues.Count)
				return values.Difference(otherValues).LastValueIsCrossing();
			return false;
		}
		public static bool LastValueIsCrossing(this IList<decimal> values)
		{
			foreach(var value in values.Take(values.Count-1).Reverse())
				if (value * values.Last() != 0)
					return value * values.Last() < 0;

			return false;
		}
		public static IList<decimal> Difference(this IList<decimal> values, IList<decimal> otherValues)
		{
			return values.Sum(otherValues.Select(c => -1 * c).ToList<decimal>());
		}
		public static IList<decimal> Sum(this IList<decimal> values, IList<decimal> otherValues)
		{
			if (values.Count != otherValues.Count)
				throw new ArgumentException("The sequences contains different lengths");

			var result = new List<decimal>();

			for (int i = 0; i < values.Count; i++)
				result.Add(values[i] + otherValues[i]);

			return result;
		}

		public static IList<Altitude> PositionsCongruence(this IList<Altitude> values, IList<Altitude> otherValues)
		{
			var positionCongruence = new List<Altitude>();

			PrependToEqualizeLength(ref values, ref otherValues, Altitude.Neutral);
			
			for (int i = 0; i < values.Count; i++)
				positionCongruence.Add((values[i] == otherValues[i]) ? values[i] : Altitude.Neutral);
			
			return positionCongruence;
		}
		public static void PrependToEqualizeLength(ref IList<Altitude> values, ref IList<Altitude> otherValues, Altitude position)
		{
			var difference = Math.Abs(values.Count - otherValues.Count);

			if (values.Count > otherValues.Count)
				otherValues = otherValues.PrependPositions(position, difference);
			if (values.Count < otherValues.Count)
				values = values.PrependPositions(position, difference);
		}
		public static IList<int> IndexesFrom(this IList<Altitude> values, Altitude position)
		{
			var indexes = new List<int>();
			for (int i = 0; i < values.Count; i++)
				if (values[i] == position)
					indexes.Add(i);

			return indexes;
		}
		public static IList<Altitude> PrependPositions(this IList<Altitude> values, Altitude position, int count)
		{
			var positions = values.ToList<Altitude>();

			for (int i = 0; i < count; i++)
				positions.Insert(0, position);

			return positions;
		}
		public static IList<T> CastAs<T>(this IList<double> values)
		{
			return values.Select(v => (T)Convert.ChangeType(v, typeof(T))).ToList();
		}
		public static IList<T> CastAs<T>(this IList<decimal> values)
		{
			return values.Select(v => (T)Convert.ChangeType(v, typeof(T))).ToList();
		}
		public static IList<T> CastAs<T>(this IList<int> values)
		{
			return values.Select(v => (T)Convert.ChangeType(v, typeof(T))).ToList();
		}
	}
}

