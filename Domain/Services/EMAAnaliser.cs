using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class EMAAnaliser : IEMAAnaliser
	{
		public virtual decimal ShortEMA { get; set; }
		public virtual decimal LongEMA { get; set; }
		public virtual TradeSignal Signal
		{
			get
			{
				return ShortEMA > LongEMA ? TradeSignal.Long : TradeSignal.Short;
			}
		}

		public EMAAnaliser()
		{

		}
		public EMAAnaliser(IEMAConfig config, ICandleAnalyser analysis) : this()
		{
			Calculate(config, analysis);
		}


		public virtual IEMAAnaliser Calculate(IEMAConfig config, ICandleAnalyser analysis)
		{
			ShortEMA = analysis.EMA(config.ShortEMA);
			LongEMA = analysis.EMA(config.LongEMA);

			return this;
		}
	}
}
