using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
	public class EMAAnalyser : IEMAAnalyser
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

		public EMAAnalyser()
		{

		}
		public EMAAnalyser(IEMAConfig config, ICandleAnalyser analysis) : this()
		{
			Calculate(config, analysis);
		}


		public virtual IEMAAnalyser Calculate(IEMAConfig config, ICandleAnalyser analysis)
		{
			ShortEMA = analysis.EMA(config.ShortEMA);
			LongEMA = analysis.EMA(config.LongEMA);

			return this;
		}
	}
}
