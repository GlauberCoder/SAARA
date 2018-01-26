using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Util.Extensions;
using System.Linq;

namespace Domain.Services
{
	public class EMAAnalyser : IEMAAnalyser
	{
		public virtual decimal ShortEMA { get; set; }
		public virtual decimal LongEMA { get; set; }
		public virtual TradeSignal Signal => ShortEMA > LongEMA ? TradeSignal.Long : TradeSignal.Short;
		public virtual Trend PriceTrend => ShortEMA > LongEMA ? Trend.High : Trend.Down;
		public virtual TradeSignal EMACrossover { get; set; }
		public virtual TradeSignal PriceCrossover { get; set; }

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

		public virtual TradeSignal CalculateEMACrossover(ICandleAnalyser analyser, IEMAConfig config)
		{
			if (Crossover(ShortEMA, LongEMA, Reference(analyser)))
			{
				var previous = analyser.Previous;
				var candle = previous.TakePrevious(previous.Last(), 3).Select(c => c.Close);
				//TODO: forma de reconhecer bullish e bearish crossover 
			}

			return TradeSignal.Hold;
		}

		public virtual TradeSignal CalculatePriceCrossover(ICandleAnalyser analyser, IEMAConfig config)
		{
			var candle = analyser.Previous.Last();
			var average = Math.Abs(candle.Open + candle.Close) / 2;

			if (Crossover(ShortEMA, average, Reference(analyser)))
			{
				//TODO: forma de reconhecer bullish e bearish crossover 
			}
			return TradeSignal.Hold;
		}

		public virtual bool Crossover(decimal value1, decimal value2, decimal reference)
		{
			var tolerance = 0.01m;
			return (Math.Abs((value1 - value2) / reference) < tolerance) ? true : false;
		}

		public virtual decimal Reference(ICandleAnalyser analyser)
		{
			var candle = analyser.Previous.Last();
			return (Math.Abs(candle.Open + candle.Close) / 2);
		}
	}
}
