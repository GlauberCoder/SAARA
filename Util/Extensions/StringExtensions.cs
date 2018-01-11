using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Util.Extensions
{
	public static class StringExtensions
	{
		public static T Deserilize<T>(this string value)
		{
			return JsonConvert.DeserializeObject<T>(value);
		}


		public static T Cast<T>(this string value)
		{
			return (T)Convert.ChangeType(value, typeof(T));
		}
		public static long ToLong(this string value)
		{
			return long.Parse(value);
		}
		public static double ToDouble(this string value)
		{
			return double.Parse(value);
		}
		public static decimal ToDecimal(this string value)
		{
			return decimal.Parse(value, CultureInfo.InvariantCulture);
		}
		public static decimal ToDecimal(this string value, CultureInfo culture)
		{
			return decimal.Parse(value, culture);
		}
	}
}
