using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Extensions;

namespace Domain.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTime Add(this DateTime date, CandleTimespan candleTimespan)
		{
			return date.AddMinutes((int)candleTimespan);
		}

		public static DateTime FirstMinute(this DateTime date, CandleTimespan candleTimespan)
		{
			return date.FloorToMinute(candleTimespan.FirstMinute(date.Minute));
		}

		public static DateTime FinishFor(this DateTime date, CandleTimespan candleTimespan)
		{
			return date.StartFor(candleTimespan).Add(candleTimespan);
		}

		public static long SecondsSince1970(this DateTime date)
		{
			return (long)(date - new DateTime(1970, 1, 1)).TotalSeconds;
		}

		public static DateTime StartFor(this DateTime date, CandleTimespan timespan)
		{
			var minute = timespan.FirstMinute(date.Minute);
			switch (timespan)
			{
				case CandleTimespan.OneMinute:
				case CandleTimespan.FiveMinutes:
				case CandleTimespan.FifteenMinutes:
				case CandleTimespan.ThirtyMinutes:
					return date.FirstMinute(timespan);

				case CandleTimespan.OneHour:
					return date.FloorToHour();

				default:
					throw new InvalidCastException("Timespan not recognized");
			}
		}

	}
}

