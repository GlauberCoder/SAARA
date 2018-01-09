using Domain.Abstractions;
using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Services;
using Domain.Entitys;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
	public class MACDAnaliser : BaseEntity, IMACDAnaliser
	{
		public virtual decimal LongEMA { get; set; }
		public virtual decimal ShortEMA { get; set; }
		public virtual decimal MACD { get; set; }
		public virtual decimal Signal { get; set; }
		public virtual decimal Histogram { get; set; }
		public MACDAnaliser()
		{

		}
		public MACDAnaliser(IMACDConfig config, ICandleAnalyser analysis) : this()
		{
			Calculate(config, analysis);
		}
		public MACDAnaliser(IMACDConfig config, ICandleAnalyser analysis, ICandle candle) : this()
		{
			Calculate(config, analysis, candle);
		}
		public virtual IMACDAnaliser Calculate(IMACDConfig config, ICandleAnalyser analysis)
		{
			return Calculate(config, analysis, analysis.Main);
		}
		public virtual IMACDAnaliser Calculate(IMACDConfig config, ICandleAnalyser analysis, ICandle candle)
		{
			LongEMA = analysis.EMA(config.LongEMA);
			ShortEMA = analysis.EMA(config.ShortEMA);
			MACD = ShortEMA - LongEMA;
			Signal = analysis
							.Previous
							.TakePrevious(candle, config.SignalEMA)
							.Select(c => new MACDAnaliser(config, analysis, c).MACD)
							.ToList()
							.EMA(config.SignalEMA);

			Histogram = MACD - Signal;

			return this;
		}
	}
}
