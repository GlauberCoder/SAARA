using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Extensions;

namespace Domain.Services
{
	public class TrendAnalyser<T> : ITrendAnalyser<T>, ITrendAnalyserConfigured<T>
		where T : ICanBeClassifiedByAltitude
	{
		Altitude Altitude { get; set; }
		TrendAnalyserMode Mode { get; set; }
		IAltitudeAnalyserConfigured<T> AltitudeAnalyser { get; set; }

		public virtual ITrendAnalyserConfigured<T> Configure(ITrendAnalyserConfig config)
		{
			Mode = config.Mode;
			Altitude = Altitude.Top;
			AltitudeAnalyser = new AltitudeAnalyser<T>().Configure(config.AltitudeAnalyserConfig);

			return this;
		}
		public virtual Trend Identify(IList<T> values)
		{
			var altitudes = AltitudeAnalyser.Identify(values);
			var tops = altitudes.Where(v => v.Altitude == Altitude).ToList();
			if (tops.Count() > 1)
			{
				var result = IdentifyRecentAndPrevious()(tops);
				return GetTrend(result.recent, result.previous);
			}
			return Trend.Neutral;
		}
		private Func<IList<T>, (decimal recent, decimal previous)> IdentifyRecentAndPrevious()
		{
			switch (Mode)
			{
				case TrendAnalyserMode.FirstAndLast:
					return IdentifyByFirstAndLast;
				case TrendAnalyserMode.MostRecents:
					return IdentifyByMostRecents;
				case TrendAnalyserMode.HighestAndLowest:
					return IdentifyByHighestAndLowest;
				case TrendAnalyserMode.Highest:
					return IdentifyByHighest;
				default:
					throw new Exception("The technic should be defined ");
			};
		}
		private (decimal recent, decimal previous) IdentifyByFirstAndLast(IList<T> tops)
		{
			var recent = tops.Last().ValueForAltitude();
			var previous = tops.First().ValueForAltitude();
			return (recent, previous);
		}
		private (decimal recent, decimal previous) IdentifyByMostRecents(IList<T> tops)
		{
			var recent = tops.Last().ValueForAltitude();
			var previous = tops[tops.Count() - 2].ValueForAltitude();
			return (recent, previous);
		}
		private (decimal recent, decimal previous) IdentifyByHighestAndLowest(IList<T> tops)
		{
			var highest = tops.IndexOfMax(v => v.ValueForAltitude());
			var lowest = tops.IndexOfMin(v => v.ValueForAltitude());
			return highest > lowest ? (tops[highest].ValueForAltitude(), tops[lowest].ValueForAltitude()) : (tops[lowest].ValueForAltitude(), tops[highest].ValueForAltitude());
		}
		private (decimal recent, decimal previous) IdentifyByHighest(IList<T> tops)
		{
			var indexOfHighest = tops.IndexOfMax(v => v.ValueForAltitude());
			var indexOfSecondHighest = tops.IndexOfMax(v => v.ValueForAltitude(), 1);
			return indexOfHighest > indexOfSecondHighest ? (tops[indexOfHighest].ValueForAltitude(), tops[indexOfSecondHighest].ValueForAltitude()) : (tops[indexOfSecondHighest].ValueForAltitude(), tops[indexOfHighest].ValueForAltitude());
		}
		private Trend GetTrend(decimal? recent, decimal? previous)
		{
			if(recent == previous || recent == null || previous == null)
				return Trend.Neutral;
			return recent > previous ? Trend.Up : Trend.Down;
		}
	}
}
