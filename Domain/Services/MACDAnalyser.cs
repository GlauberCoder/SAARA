using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Entitys.AnalisysConfig;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Extensions;

namespace Domain.Services
{
	public class MACDAnalyser : IMACDAnalyser
	{
		public virtual IMACD MACD => MACDs.Last();
		public virtual IList<IMACD> MACDs { get; set; }
		public virtual Trend Trend { get; set; }
		public virtual TradeSignal CrossSignal { get; set; }
		public virtual TradeSignal CenterCrossSignal { get; set; }
		public virtual Trend DivergenceSignal { get; set; }
		private decimal valueForAltitudeAnalyser { get; set; }
		public virtual Altitude Altitude { get; set; }

		public MACDAnalyser() { }
		public MACDAnalyser(IMACDConfig config, ICandleAnalyser analysis) : this() => Calculate(config, analysis);
		public MACDAnalyser(IMACDConfig config, ICandleAnalyser analysis, ICandle candle) : this() => Calculate(config, analysis, candle);
		public virtual IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis) => Calculate(config, analysis, analysis.Main);
		public virtual IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis, ICandle candle)
		{
			calculateMACDs(config, analysis);
			Trend = MACD.Trend;
			CrossSignal = MACD.CrossTradeSignal;
			CenterCrossSignal = CalculateCenterCrossSignal(config);
			DivergenceSignal = CalculateDivergenceSignal(config, analysis);

			return this;
		}
		private IMACDAnalyser calculateMACDs(IMACDConfig config, ICandleAnalyser analysis)
		{
			MACDs = calculateMACDsBaseValues(config, analysis);
			var signalCantBeCalculated = config.SignalEMA - 1;
			foreach (var macd in MACDs.Skip(signalCantBeCalculated))
				macd
					.CalculateSignal(MACDs.TakeUntil(macd), config.SignalEMA)
					.CalculateHistogram();

			return this;
		}
		private IList<IMACD> calculateMACDsBaseValues(IMACDConfig config, ICandleAnalyser analysis)
		{
			var macds = new List<IMACD>();
			var candles = analysis.Previous.ToList();
			var macdCantBeCalculated = config.EMAConfig.LongEMA - 1;

			foreach (var candle in candles.Skip(macdCantBeCalculated))
			{
				var macd = new MACD
				{
					Candle = candle,
					LongEMA = candles.TakeUntil(candle).EMA(config.EMAConfig.LongEMA),
					ShortEMA = candles.TakeUntil(candle).EMA(config.EMAConfig.ShortEMA)
				}
				.CalculateValue();
				macds.Add(macd);
			}
			return macds;
		}
		public virtual TradeSignal CalculateCrossSignal()
		{
			if (MACDs.Select(m => m.Value - m.Signal).ToList().LastValueIsCrossing())
				return MACD.TradeSignal;

			return TradeSignal.Hold;
		}
		public virtual TradeSignal CalculateCenterCrossSignal(IMACDConfig config)
		{
			if (MACDs.SelectList(m => m.Value).LastValueIsCrossing())
				return MACD.CrossTradeSignal;

			return TradeSignal.Hold;
		}
		public virtual Trend CalculateDivergenceSignal(IMACDConfig config, ICandleAnalyser analysis)
		{
			var macdsTrend = new TrendAnalyser<IMACD>().Configure(config.MACDTrendAnalyserConfig).Identify(MACDs);
			var pricesTrend = new TrendAnalyser<ICandle>().Configure(config.PriceTrendAnalyserConfig).Identify(MACDs.SelectList(m => m.Candle));

			return BearishDivergence(pricesTrend, macdsTrend) ? Trend.Down : BullishDivergence(pricesTrend, macdsTrend) ? Trend.Up : Trend.Neutral;
		}
		private bool BearishDivergence(Trend price, Trend macd) => price == Trend.Up && macd == Trend.Down;
		private bool BullishDivergence(Trend price, Trend macd) => price == Trend.Down && macd == Trend.Up;
		public virtual decimal ValueForAltitude() => valueForAltitudeAnalyser;
		public virtual void ClassifyByAltitude(Altitude altitude) => Altitude = altitude;
	}
}
