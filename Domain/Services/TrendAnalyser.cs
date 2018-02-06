using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
	public class TrendAnalyser : ITrendAnalyser
	{
		Altitude Altitude { get; set; }
		TrendAnalyserTechnic Technic { get; set; }
		IAltitudeAnalyser DefaultAltitudeAnalyser { get => new AltitudeAnalyser().ByLength(3, 3); }

		public virtual ITrendAnalyser ByFirstVsLast(Altitude altitude)
		{
			Technic = TrendAnalyserTechnic.FirstVsLast;
			Altitude = altitude;

			return this;
		}
		public virtual ITrendAnalyser ByHigherHighLowerLow(Altitude altitude)
		{
			Technic = TrendAnalyserTechnic.HigherHighLowerLow;
			Altitude = altitude;

			return this;
		}
		public virtual ITrendAnalyser ByMostRecents(Altitude altitude)
		{
			Technic = TrendAnalyserTechnic.MostRecents;
			Altitude = altitude;

			return this;
		}
		public virtual Trend Identify(IList<decimal> values)
		{
			return Identify(values, DefaultAltitudeAnalyser);
		}
		public Trend Identify(IList<decimal> values, IAltitudeAnalyser altitudeAnalyser)
		{
			var altitudes = altitudeAnalyser.Identify(values);
			switch (Technic)
			{
				case TrendAnalyserTechnic.FirstVsLast:
					return IdentifyByFirstVsLast(values, altitudes);
				case TrendAnalyserTechnic.MostRecents:
					return IdentifyByMostRecents(values, altitudes);
				case TrendAnalyserTechnic.HigherHighLowerLow:
					return IdentifyByHigherHighLowerLow(values, altitudes);
				default:
					throw new Exception("The technic should be defined ");
			};
		}
		private Trend IdentifyByFirstVsLast(IList<decimal> values, IList<Altitude> altitudes)
		{
			var first = new AltitudeAnalyser().GetFirst(values, altitudes, Altitude);
			var last = new AltitudeAnalyser().GetLast(values, altitudes, Altitude);
			return GetTrend(last, first);
		}
		private Trend IdentifyByHigherHighLowerLow(IList<decimal> values, IList<Altitude> altitudes)
		{
			var higherIndex = new AltitudeAnalyser().GetHigherIndex(values, altitudes, Altitude);
			var lowerIndex = new AltitudeAnalyser().GetLowerIndex(values, altitudes, Altitude);
			return GetTrend(values, higherIndex, lowerIndex);
			
		}
		private Trend IdentifyByMostRecents(IList<decimal> values, IList<Altitude> altitudes)
		{
			for (int i = altitudes.Count - 1; i >= 0; i--)
				if(altitudes[i] == Altitude)
					for (int j = i - 1; j >= 0; j--)
						if(altitudes[j] == Altitude)
							return GetTrend(values[i], values[j]);
			return Trend.Neutral;
		}
		private Trend GetTrend(decimal? recent, decimal? previous)
		{
			if(recent == previous || recent == null || previous == null)
				return Trend.Neutral;
			return recent > previous ? Trend.Up : Trend.Down;
		}
		private Trend GetTrend(IList<decimal> values, int? recentIndex, int? previousIndex)
		{
			if (recentIndex == previousIndex || recentIndex == null || previousIndex == null || recentIndex > values.Count || previousIndex > values.Count)
				return Trend.Neutral;
			var recent = recentIndex ?? -1;
			var previous = previousIndex ?? -1;
			return recentIndex > previousIndex ? GetTrend(values[recent], values[previous]) : GetTrend(values[previous], values[recent]);
		}

	}
}
