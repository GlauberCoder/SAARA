using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Extensions;
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
		public virtual TradeSignal CalculateDCrossSignal(IMACDConfig config, decimal macdLine, decimal signalLine, Trend trend)
		{
			var variation = trend == Trend.Up ? signalLine.PercentageOfChange(macdLine) : macdLine.PercentageOfChange(signalLine);
			var tolerance = trend == Trend.Up ? config.CrossoverTolerance : config.CrossunderTolerance;

			if (trend == Trend.Up && variation <= tolerance)
				return macdLine > 0 ? TradeSignal.StrongLong : TradeSignal.WeakLong;

			if (trend == Trend.Down && variation <= tolerance)
				return signalLine < 0 ? TradeSignal.StrongShort : TradeSignal.WeakShort;

			return TradeSignal.Hold;
		}
		public virtual TradeSignal CalculateCenterCrossSignal(IMACDConfig config, decimal macdLine, Trend trend)
		{
			var tolerance = trend == Trend.Up ? config.CrossoverTolerance : config.CrossunderTolerance;

			if (trend != Trend.Neutral && macdLine <= tolerance)
				return trend == Trend.Up ? TradeSignal.Long : TradeSignal.Short;

			return TradeSignal.Hold;
		}



	}
}
