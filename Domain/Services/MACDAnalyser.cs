using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Extensions;

namespace Domain.Services
{
	public class MACDAnalyser : IMACDAnalyser
	{
		public virtual decimal LongEMA { get; set; }
		public virtual decimal ShortEMA { get; set; }
		public virtual decimal MACD { get; set; }
		public virtual decimal Signal { get; set; }
		public virtual decimal Histogram { get; set; }
		public virtual Trend Trend { get; set; }
		public virtual TradeSignal CrossSignal { get; set; }
		public virtual TradeSignal CenteCrossSignal { get; set; }
		public virtual TradeSignal DivergenceSignal { get; set; }
		

		public MACDAnalyser()
		{
		}
		public MACDAnalyser(IMACDConfig config, ICandleAnalyser analysis) : this()
		{
			Calculate(config, analysis);
		}
		public MACDAnalyser(IMACDConfig config, ICandleAnalyser analysis, ICandle candle) : this()
		{
			Calculate(config, analysis, candle);
		}
		public virtual IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis)
		{
			return Calculate(config, analysis, analysis.Main);
		}
		public virtual IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis, ICandle candle)
		{
			calculateMACD(config, analysis);

			calculateSignal(config, analysis, candle);

			calculateHistogram();

			return this;
		}
		private IMACDAnalyser calculateMACD(IMACDConfig config, ICandleAnalyser analysis)
		{
			LongEMA = analysis.EMA(config.LongEMA);
			ShortEMA = analysis.EMA(config.ShortEMA);
			MACD = ShortEMA - LongEMA;

			return this;
		}
		private IMACDAnalyser calculateSignal(IMACDConfig config, ICandleAnalyser analysis, ICandle candle)
		{
			var candles = analysis.Previous;
			var length = candles.Count - config.LongEMA;
			var previous = candles.TakeUntil(candle, length);

			Signal = previous
							.Select(c => new MACDAnalyser().calculateMACD(config, new CandleAnalyser { Previous = candles.TakeUntil(c) }).MACD)
							.ToList()
							.EMA(config.SignalEMA);

			return this;
		}
		private IMACDAnalyser calculateHistogram()
		{
			Histogram = MACD - Signal;
			return this;
		}
		public virtual Trend CalculateTrend(decimal macdLine, decimal signalLine)
		{
			return (macdLine > signalLine) ? Trend.Up : Trend.Down;
		}
		public virtual TradeSignal CalculateCrossSignal(IMACDConfig config, decimal macdLine, decimal signalLine, Trend trend)
		{
			var variation = trend == Trend.Up ? signalLine.PercentageOfChange(macdLine) : macdLine.PercentageOfChange(signalLine);
			var tolerance = trend == Trend.Up ? config.CrossoverTolerance : config.CrossunderTolerance;

			if (trend == Trend.Up && variation <= tolerance)
				return macdLine > 0 ? TradeSignal.StrongLong : TradeSignal.WeakLong;

			if (trend == Trend.Down && variation <= tolerance)
				return signalLine < 0 ? TradeSignal.StrongShort : TradeSignal.WeakShort;

			return TradeSignal.Hold;
		}
		public virtual TradeSignal CalculateCrossSignal(IMACDConfig config, IList<decimal> macdLine, IList<decimal> signalLine)
		{
			if (! macdLine.LastValueIsCrossing(signalLine))
				return TradeSignal.Hold;

			var macd = macdLine.Last();
			var signal = signalLine.Last();

			if (macd > signal)
				return macd > 0 ? TradeSignal.StrongLong : TradeSignal.WeakLong;

			if (macd < signal)
				return signal < 0 ? TradeSignal.StrongShort : TradeSignal.WeakShort;

			return TradeSignal.Hold;
		}
		public virtual TradeSignal CalculateCenterCrossSignal(IMACDConfig config, decimal macdLine, Trend trend)
		{
			var tolerance = trend == Trend.Up ? config.CrossoverTolerance : config.CrossunderTolerance;

			if (trend != Trend.Neutral && macdLine <= tolerance)
				return trend == Trend.Up ? TradeSignal.Long : TradeSignal.Short;

			return TradeSignal.Hold;
		}
		public virtual TradeSignal CalculateCenterCrossSignal(IMACDConfig config, IList<decimal> macdLine)
		{
			if (macdLine.LastValueIsCrossing())
				return macdLine.Last() > 0 ? TradeSignal.Long : TradeSignal.Short;
			return TradeSignal.Hold;
		}
		public virtual TradeSignal CalculateDivergenceSignal(IMACDConfig config, ICandleAnalyser analysis, ICandle candle)
		{
			return TradeSignal.Hold;
		}
		public virtual TradeSignal CalculateDivergenceSignal(IList<decimal> price, IList<decimal> macd, int minTopLength, int minBottomLength)
		{
			var length = 3;
			var analyser = new AltitudeAnalyser().ByLength(length, length);

			var priceAltitude = analyser.Identify(price);
			var macdAltitude = analyser.Identify(macd);

			var congruentValues = Congruence(priceAltitude, macdAltitude);

			var bearishSignal = BearishDivergenceSignal(price, macd, congruentValues);
			var bullishSignal = BullishDivergenceSignal(price, macd, congruentValues);

			if (bearishSignal == TradeSignal.Short && bullishSignal == TradeSignal.Hold)
				return TradeSignal.Short;
			if (bullishSignal == TradeSignal.Long && bearishSignal == TradeSignal.Hold)
				return TradeSignal.Long;

			return TradeSignal.Hold;
		}
		public virtual TradeSignal BearishDivergenceSignal(IList<decimal> price, IList<decimal> macd, IList<Altitude> congruentValues)
		{
			var indexes = new AltitudeAnalyser().IndexesFrom(congruentValues, Altitude.Top);

			foreach (var previous in indexes)
				foreach (var actual in indexes.Where(c => c > previous))
					if (BearishDivergence(price[previous], price[actual], macd[previous], macd[actual]))
						return TradeSignal.Short;
			return TradeSignal.Hold;
		}
		private bool BearishDivergence(decimal previousPrice, decimal actualPrice, decimal previousMACD, decimal actualMACD)
		{
			return previousPrice < actualPrice && previousMACD > actualMACD;
		}
		public virtual TradeSignal BullishDivergenceSignal(IList<decimal> price, IList<decimal> macd, IList<Altitude> congruentValues)
		{
			var indexes = new AltitudeAnalyser().IndexesFrom(congruentValues, Altitude.Bottom);

			foreach (var previous in indexes)
				foreach (var actual in indexes.Where(c => c > previous))
					if (BullishDivergence(price[previous], price[actual], macd[previous], macd[actual]))
						return TradeSignal.Long;
			return TradeSignal.Hold;
		}
		private bool BullishDivergence(decimal previousPrice, decimal actualPrice, decimal previousMACD, decimal actualMACD)
		{
			return previousPrice > actualPrice && previousMACD < actualMACD;
		}

		public IList<Altitude> Congruence(IList<Altitude> values, IList<Altitude> otherValues)
		{
			var altitudeCongruence = new List<Altitude>();
			PrependToEqualizeLength(ref values, ref otherValues, Altitude.Neutral);

			for (int i = 0; i < values.Count; i++)
				altitudeCongruence.Add((values[i] == otherValues[i]) ? values[i] : Altitude.Neutral);

			return altitudeCongruence;
		}
		public void PrependToEqualizeLength(ref IList<Altitude> values, ref IList<Altitude> otherValues, Altitude altitude)
		{
			var difference = Math.Abs(values.Count - otherValues.Count);

			if (values.Count > otherValues.Count)
				otherValues = PrependAltitudeValues(otherValues, altitude, difference);
			if (values.Count < otherValues.Count)
				values = PrependAltitudeValues(values, altitude, difference);
		}
		public IList<Altitude> PrependAltitudeValues(IList<Altitude> values, Altitude altitude, int count)
		{
			var altitudes = values.ToList<Altitude>();
			for (int i = 0; i < count; i++)
				altitudes.Insert(0, altitude);
			return altitudes;
		}
		
	}
}
