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

			Trend = CalculateTrend(MACD, Signal);
			CrossSignal = CalculateCrossSignal(config, analysis);
			CenteCrossSignal = CalculateCenterCrossSignal(config, analysis);
			DivergenceSignal = CalculateDivergenceSignal(config, analysis);

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
		public virtual Trend CalculateTrend(decimal macd, decimal signal)
		{
			return (macd > signal) ? Trend.Up : Trend.Down;
		}
		public virtual TradeSignal CalculateCrossSignal(IMACDConfig config, ICandleAnalyser analysis)
		{
			var candles = analysis.Previous;
			var length = candles.Count - config.LongEMA - config.SignalEMA;
			var previous = candles.TakeUntil(candles.Last(), length);

			var macds = new List<decimal>();
			var signals = new List<decimal>();

			for (int i = 0; i < length; i++)
			{
				var _candles = candles.SkipLast(i).ToList();
				var analyser = new CandleAnalyser { Previous = _candles  };
				MACDAnalyser macd = (MACDAnalyser)new MACDAnalyser().calculateMACD(config, analyser);
				macd.calculateSignal(config, analyser, _candles.Last());
				macds.Add(macd.MACD);
				signals.Add(macd.Signal);
			}
			return CalculateCrossSignal(config, macds, signals);

		}
		public virtual TradeSignal CalculateCrossSignal(IMACDConfig config, decimal macd, decimal signal, Trend trend)
		{
			var variation = trend == Trend.Up ? signal.PercentageOfChange(macd) : macd.PercentageOfChange(signal);
			var tolerance = trend == Trend.Up ? config.CrossoverTolerance : config.CrossunderTolerance;

			if (trend == Trend.Up && variation <= tolerance)
				return macd > 0 ? TradeSignal.StrongLong : TradeSignal.WeakLong;

			if (trend == Trend.Down && variation <= tolerance)
				return signal < 0 ? TradeSignal.StrongShort : TradeSignal.WeakShort;

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
		public virtual TradeSignal CalculateCenterCrossSignal(IMACDConfig config, ICandleAnalyser analysis)
		{
			var macds = new List<decimal>();

			var candles = analysis.Previous;
			var length = candles.Count - config.LongEMA;
			var previous = candles.TakeUntil(candles.Last(), length);

			macds = previous
							.Select(c => new MACDAnalyser().calculateMACD(config, new CandleAnalyser { Previous = candles.TakeUntil(c) }).MACD)
							.ToList();
			
			return CalculateCenterCrossSignal(config, macds);
		}
		public virtual TradeSignal CalculateCenterCrossSignal(IMACDConfig config, IList<decimal> macdLine)
		{
			if (macdLine.LastValueIsCrossing())
				return macdLine.Last() > 0 ? TradeSignal.Long : TradeSignal.Short;
			return TradeSignal.Hold;
		}
		public virtual TradeSignal CalculateDivergenceSignal(IMACDConfig config, ICandleAnalyser analysis)
		{
			var macds = new List<decimal>();
			var prices = new List<decimal>();

			var candles = analysis.Previous;
			var length = candles.Count - config.LongEMA;
			var previous = candles.TakeUntil(candles.Last(), length);

			macds = previous
							.Select(c => new MACDAnalyser().calculateMACD(config, new CandleAnalyser { Previous = candles.TakeUntil(c) }).MACD)
							.ToList();
			foreach (var item in previous)
				prices.Add(item.Close);

			return CalculateDivergenceSignal(prices, macds);
		}
		public virtual TradeSignal CalculateDivergenceSignal(IList<decimal> price, IList<decimal> macd)
		{
			var bearishDivergence = BearishDivergenceSignal(price, macd);
			var bullishDivergence = BullishDivergenceSignal(price, macd);

			if (bearishDivergence == TradeSignal.Hold && bullishDivergence != TradeSignal.Hold)
				return bullishDivergence;
			if (bearishDivergence != TradeSignal.Hold && bullishDivergence == TradeSignal.Hold)
				return bearishDivergence;
			return TradeSignal.Hold;
		}
		public virtual TradeSignal BearishDivergenceSignal(IList<decimal> price, IList<decimal> macd)
		{
			var altitude = Altitude.Top;
			var analyser = new TrendAnalyser().ByMostRecents(altitude);
			var priceTrend = analyser.Identify(price);
			var macdTrend = analyser.Identify(macd);

			if (BearishDivergence(priceTrend, macdTrend))
				return TradeSignal.Short;
			return TradeSignal.Hold;
		}
		private bool BearishDivergence(Trend price, Trend macd)
		{
			return price == Trend.Up && macd == Trend.Down;
		}
		public virtual TradeSignal BullishDivergenceSignal(IList<decimal> price, IList<decimal> macd)
		{
			var altitude = Altitude.Bottom;
			var analyser = new TrendAnalyser().ByMostRecents(altitude);
			var priceTrend = analyser.Identify(price);
			var macdTrend = analyser.Identify(macd);

			if (BullishDivergence(priceTrend, macdTrend))
				return TradeSignal.Short;
			return TradeSignal.Hold;
		}
		private bool BullishDivergence(Trend price, Trend macd)
		{
			return price == Trend.Down && macd == Trend.Up;
		}

	}
}
