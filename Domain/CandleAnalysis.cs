using Domain.AnalisysConfig;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class CandleAnalysis
	{
		public virtual IList<Candle> Previous { get; set; }
		public virtual Candle Main { get; set; }
		public virtual AnalysisConfig Config { get; set; }

		public virtual decimal EMA(int length) {
			return Previous.EMA(length);
		}
	}
}
