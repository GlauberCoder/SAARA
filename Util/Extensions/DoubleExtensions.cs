using System;

namespace Util.Extensions
{
	public static class DoubleExtensions
	{
		public static DateTime TimestampToDateTime(this double value) => new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(value);

		public static double ProportionOn(this double value, double total) => (value / total);

		public static double PercentageOn(this double value, double total) => value.ProportionOn(total) * 100;
	}
}
