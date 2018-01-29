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
			return (shortEMA > longEMA) ? Trend.High : Trend.Down;
		}

		public virtual TradeSignal CalculateCrossSignal(IEMAConfig config, decimal shortEMA, decimal longEMA, Trend trend)
		{
			var emaVariation = shortEMA.PercentageOfChange(longEMA).Abs();
			var tolerance = trend == Trend.High ? config.CrossoverTolerance : config.CrossunderTolerance;

			if (trend != Trend.Neutral && emaVariation <= tolerance)
				return trend == Trend.High ? TradeSignal.Long : TradeSignal.Short;

			return TradeSignal.Hold;
		}

		public virtual TradeSignal CalculateAverageDistanceSignal(IEMAConfig config, decimal price, decimal shortEMA, Trend trend)
		{
			var variation = shortEMA > price ? price.PercentageOfChange(shortEMA).Abs() : price.PercentageOfChange(shortEMA).Abs();

			if (trend != Trend.Neutral && variation <= config.AverageDistanceTolerance)
				return trend == Trend.High ? TradeSignal.Long : TradeSignal.Short;

			return TradeSignal.Hold;
		}

		public virtual TradeSignal CalculateSignal(TradeSignal crossSignal, TradeSignal averageDistanceSignal)
		{
			if (crossSignal == TradeSignal.Long)
				return averageDistanceSignal == TradeSignal.Long ? TradeSignal.StrongLong : TradeSignal.Long;

			if (crossSignal == TradeSignal.Short)
				return averageDistanceSignal == TradeSignal.Short ? TradeSignal.StrongShort : TradeSignal.Short;

			if (crossSignal == TradeSignal.Hold)
			{
				if (averageDistanceSignal == TradeSignal.Short)
					return TradeSignal.WeakShort;
				if (averageDistanceSignal == TradeSignal.Long)
					return TradeSignal.WeakLong;
			}
			return TradeSignal.Hold;
		}
	}
}
