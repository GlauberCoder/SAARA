using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using System.Collections.Generic;
using System.Linq;
using Util.Extensions;

namespace Domain.Extensions
{
	public static class CandleTimespanExtensions
	{
		public static long ToMilliseconds(this CandleTimespan candleTimespan)
		{
			return (int)candleTimespan * 60000;
		}

		public static int FirstMinute(this CandleTimespan candle, int number)
		{
			var fraction = (int)candle;

			return (fraction == 0) || (number % fraction == 0) ? number : number - (number % fraction);
		}

		public static string stringFormat(this CandleTimespan timespan)
		{
			switch (timespan)
			{
				case CandleTimespan.OneMinute:
					return "1m";
				case CandleTimespan.FiveMinutes:
					return "5m";
				case CandleTimespan.FifteenMinutes:
					return "15m";
				case CandleTimespan.ThirtyMinutes:
					return "30m";
				case CandleTimespan.OneHour:
					return "1h";
				default:
					return null;
			}
		}


	}
}

