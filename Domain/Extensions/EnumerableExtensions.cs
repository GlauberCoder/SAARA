﻿using Domain.Abstractions.Entitys;
using System;
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
			return values.Select(c => decimal.Parse(c.ToString())).ToList().EMA(length);
		}

		public static decimal EMA(this IList<decimal> values, int length)
		{
			var minNumberOfValues = length * 2;

			if (values.Count < minNumberOfValues)
				throw new ArgumentException("Number of values is bellow to the minimal for this length.");

			values = values.Take(minNumberOfValues).ToList();

			var matureValues = values.Skip(length);
			var ema = values.Take(length).Average();
			decimal k = 2m / (length + 1);

			foreach (var value in matureValues)
				ema = k * (value - ema) + ema;

			return decimal.Round(ema, 2);
		}
	}
}

