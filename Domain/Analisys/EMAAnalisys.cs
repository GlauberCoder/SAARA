using Domain.AnalisysConfig;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Analisys
{
    public class EMAAnalisys
    {
		public EMAAnalisys()
		{

		}
		public EMAAnalisys(EMAConfig config, CandleAnalysis analysis) : this()
		{
			Calculate(config, analysis);
		}
		public virtual decimal ShortEMA { get; set; }
		public virtual decimal LongEMA { get; set; }


		public virtual EMAAnalisys Calculate(EMAConfig config, CandleAnalysis analysis)
		{
			ShortEMA = analysis.EMA(config.ShortEMA);
			LongEMA = analysis.EMA(config.LongEMA);

			return this;
		}
	}
}
