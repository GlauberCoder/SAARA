using Domain.AnalisysConfig;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
	public class MACDAnalisys
	{
		public virtual decimal LongEMA { get; set; }
		public virtual decimal ShortEMA { get; set; }
		public virtual decimal MACD { get; set; }
		public virtual decimal Signal { get; set; }
		public virtual decimal Histogram { get; set; }
		public MACDAnalisys()
		{

		}
		public MACDAnalisys(MACDConfig config, CandleAnalysis analysis) : this()
		{
			Calculate(config, analysis);
		}
		public MACDAnalisys(MACDConfig config, CandleAnalysis analysis, Candle candle) : this()
		{
			Calculate(config, analysis, candle);
		}
		public virtual MACDAnalisys Calculate(MACDConfig config, CandleAnalysis analysis)
		{
			return Calculate(config, analysis, analysis.Main);
		}
		public virtual MACDAnalisys Calculate(MACDConfig config, CandleAnalysis analysis, Candle candle)
		{
			LongEMA = analysis.EMA(config.LongEMA);
			ShortEMA = analysis.EMA(config.ShortEMA);
			MACD = ShortEMA - LongEMA;
			Signal = analysis
							.Previous
							.TakePrevious(candle, config.SignalEMA)
							.Select(c => new MACDAnalisys(config, analysis, c).MACD)
							.ToList()
							.EMA(config.SignalEMA);

			Histogram = MACD - Signal;

			return this;
		}
	}
}
