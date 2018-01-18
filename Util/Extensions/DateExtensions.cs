using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Extensions
{
	public static class DateExtensions
	{
		public static DateTime Add(this DateTime date, int hours = 0, int minutes = 0, int seconds = 0)
		{
			return date.Add(new TimeSpan(hours, minutes, seconds));
		}

		public static DateTime WithoutMilliseconds(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
		}

	}
}
