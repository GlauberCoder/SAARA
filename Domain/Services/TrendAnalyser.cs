using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
	public class TrendAnalyser : ITrendAnalyser
	{
		Altitude Altitude { get; set; }
		TrendAnalyserTechnic Technic { get; set; }

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
			switch (Technic)
			{
				case TrendAnalyserTechnic.FirstVsLast:
					return IdentifyByFirstVsLast(values);
				case TrendAnalyserTechnic.MostRecents:
					return IdentifyByMostRecents(values);
				case TrendAnalyserTechnic.HigherHighLowerLow:
					return IdentifyByHigherHighLowerLow(values);
				default:
					throw new Exception("The technic should be defined");
			}
		}
		private Trend IdentifyByFirstVsLast(IList<decimal> values)
		{
			throw new NotImplementedException();
		}
		private Trend IdentifyByHigherHighLowerLow(IList<decimal> values)
		{
			throw new NotImplementedException();
		}
		private Trend IdentifyByMostRecents(IList<decimal> values)
		{
			throw new NotImplementedException();
		}



	}
}
