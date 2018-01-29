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
		public virtual decimal ShortEMA { get; private set; }
		public virtual decimal LongEMA { get; private set; }
		public virtual Trend Trend { get; private set; }
		public virtual TradeSignal Signal { get; private set; }
		public virtual TradeSignal CrossSignal { get; private set; }
		public virtual TradeSignal AverageDistanceSignal { get; private set; }

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

			Trend = CalculateTrend(ShortEMA, LongEMA);
			CrossSignal = CalculateCrossSignal(config, ShortEMA, LongEMA, Trend);
			AverageDistanceSignal = CalculateAverageDistanceSignal(config, ShortEMA, LongEMA, Trend);
			Signal = CalculateSignal(CrossSignal, AverageDistanceSignal);

			return this;
		}

		public virtual Trend CalculateTrend(decimal shortEMA, decimal longEMA)
		{
			return (shortEMA > longEMA) ? Trend.Up : Trend.Down;
		}

		public virtual TradeSignal CalculateCrossSignal(IEMAConfig config, decimal shortEMA, decimal longEMA, Trend trend)
		{
			var emaVariation = trend == Trend.Up ? longEMA.PercentageOfChange(shortEMA) : shortEMA.PercentageOfChange(longEMA);
			var tolerance = trend == Trend.Up ? config.CrossoverTolerance : config.CrossunderTolerance;

			if (trend != Trend.Neutral && emaVariation <= tolerance)
				return trend == Trend.Up ? TradeSignal.Long : TradeSignal.Short;

			return TradeSignal.Hold;
		}

		public virtual TradeSignal CalculateAverageDistanceSignal(IEMAConfig config, decimal price, decimal shortEMA, Trend trend)
		{
			var variation = shortEMA > price ? price.PercentageOfChange(shortEMA) : shortEMA.PercentageOfChange(price);

			if (trend != Trend.Neutral && variation <= config.AverageDistanceTolerance)
				return trend == Trend.Up ? TradeSignal.Long : TradeSignal.Short;

			return TradeSignal.Hold;
		}

		public virtual TradeSignal CalculateSignal(TradeSignal crossSignal, TradeSignal averageDistanceSignal)
		{
			if (crossSignal == TradeSignal.Long)
				return averageDistanceSignal == TradeSignal.Long ? TradeSignal.StrongLong : TradeSignal.WeakLong;

			if (crossSignal == TradeSignal.Short)
				return averageDistanceSignal == TradeSignal.Short ? TradeSignal.StrongShort : TradeSignal.WeakShort;

			return TradeSignal.Hold;
		}
	}
}
