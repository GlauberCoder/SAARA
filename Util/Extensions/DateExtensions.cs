using System;

namespace Util.Extensions
{
	public static class DateExtensions
	{
		public static DateTime Add(this DateTime date, int hours = 0, int minutes = 0, int seconds = 0) => date.Add(new TimeSpan(hours, minutes, seconds));

		public static DateTime WithoutMilliseconds(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);

		public static DateTime Set(this DateTime date, int? year = null, int? month = null, int? day = null, int? hour = null, int? minute = null, int? second = null, int? millisecond = null)
		{
			return new DateTime(year??date.Year, month??date.Month, day??date.Day, hour??date.Hour, minute??date.Minute, second??date.Second, millisecond??date.Millisecond);
		}

		public static DateTime SetYear(this DateTime date, int year) => date.Set(year: year);

		public static DateTime SetMonth(this DateTime date, int month) => date.Set(month: month);

		public static DateTime SetDay(this DateTime date, int day) => date.Set(day: day);

		public static DateTime SetHour(this DateTime date, int hour) => date.Set(hour: hour);

		public static DateTime SetMinute(this DateTime date, int minute) => date.Set(minute: minute);

		public static DateTime SetSecond(this DateTime date, int second) => date.Set(second: second);

		public static DateTime SetMillisecond(this DateTime date, int millisecond) => date.Set(millisecond: millisecond);

		public static DateTime FloorToYear(this DateTime date, int year) => date.Set(year: year, month: 0, day:0, hour:0, minute:0 , second:0, millisecond: 0);

		public static DateTime FloorToMonth(this DateTime date, int month) => date.Set(month: month, day: 0, hour: 0, minute: 0, second: 0, millisecond: 0);

		public static DateTime FloorToDay(this DateTime date, int day) => date.Set(day: day, hour: 0, minute: 0, second: 0, millisecond: 0);

		public static DateTime FloorToHour(this DateTime date, int hour) => date.Set(hour: hour, minute: 0, second: 0, millisecond: 0);

		public static DateTime FloorToMinute(this DateTime date, int minute) => date.Set(minute: minute, second: 0, millisecond: 0);

		public static DateTime FloorToSecond(this DateTime date, int second) => date.Set(second: second, millisecond: 0);

		public static DateTime FloorToYear(this DateTime date) => date.Set(month: 0, day: 0, hour: 0, minute: 0, second: 0, millisecond: 0);

		public static DateTime FloorToMonth(this DateTime date) => date.Set(day: 0, hour: 0, minute: 0, second: 0, millisecond: 0);

		public static DateTime FloorToDay(this DateTime date) => date.Set(hour: 0, minute: 0, second: 0, millisecond: 0);

		public static DateTime FloorToHour(this DateTime date) => date.Set(minute: 0, second: 0, millisecond: 0);

		public static DateTime FloorToMinute(this DateTime date) => date.Set(second: 0, millisecond: 0);

		public static DateTime FloorToSecond(this DateTime date) => date.Set(millisecond: 0);
	}
}
