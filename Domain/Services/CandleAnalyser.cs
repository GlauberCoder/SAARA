using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
	public class CandleAnalyser : ICandleAnalyser, ICanBeClassifiedByAltitude
	{
		public virtual IList<ICandle> Previous { get; set; }
		public virtual ICandle Main { get; set; }
		public virtual IAnalysisConfig Config { get; set; }

		public Altitude Altitude { get; set; }

		public void ClassifyByAltitude(Altitude altitude)
		{
			Altitude = altitude;
		}

		public virtual decimal EMA(int length)
		{
			return Previous.EMA(length);
		}

		public decimal ValueForAltitude()
		{
			return Main != null ? Main.Close : Previous.Last().Close;
		}
	}
}
