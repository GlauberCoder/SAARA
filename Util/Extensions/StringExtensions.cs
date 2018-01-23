using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Util.Extensions
{
	public static class StringExtensions
	{
		public static T Deserilize<T>(this string value) => JsonConvert.DeserializeObject<T>(value);

		public static T Cast<T>(this string value) => (T)Convert.ChangeType(value, typeof(T));

		public static long ToLong(this string value) => long.Parse(value);

		public static double ToDouble(this string value) => double.Parse(value);

		public static decimal ToDecimal(this string value) => decimal.Parse(value, CultureInfo.InvariantCulture);

		public static decimal ToDecimal(this string value, CultureInfo culture) => decimal.Parse(value, culture);
	}
}
