using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Services;
using Domain.Extensions;
using System.Collections.Generic;

namespace Domain.Services
{
	public class CandleAnalyser : ICandleAnalyser
	{
		public virtual IList<ICandle> Previous { get; set; }
		public virtual ICandle Main { get; set; }
		public virtual IAnalysisConfig Config { get; set; }

		public virtual decimal EMA(int length)
		{
			return Previous.EMA(length);
		}
	}
}
