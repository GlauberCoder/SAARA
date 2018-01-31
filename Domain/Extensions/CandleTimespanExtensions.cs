using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using System.Collections.Generic;
using System.Linq;

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


	}
}

