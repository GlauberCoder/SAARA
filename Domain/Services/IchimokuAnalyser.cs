using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Util.Extensions;
using System.Linq;
using Domain.Abstractions.Entitys;

namespace Domain.Services
{
	public class IchimokuAnalyser : IIchimokuAnalyser
	{
		public virtual IList<IIchimoku> Ichimokus { get; set; }
		public virtual IIchimoku Ichimoku { get; private set; }
		public virtual ICandle Candle { get; set; }
		public virtual TradeSignal ConversionXBaseLineSignal { get; set; }
		public virtual TradeSignal PriceXBaseLineSignal { get; set; }
		public virtual Trend SpanCrossSignal { get; private set; }
		public virtual Trend PriceXCloudSignal { get; private set; }
		public virtual Trend LaggedXPriceSignal { get; private set; }
		public virtual TradeSignal SpanCrossTradeSignal { get; private set; }
		public virtual TradeSignal LaggingCrossPriceTradeSignal { get; private set; }
		public IchimokuAnalyser()
		{
		}
		public IchimokuAnalyser(IIchimokuConfig config, ICandleAnalyser analysis) : this() => Calculate(config, analysis);
		public virtual TradeSignal CalculateConversionXBaseLineSignal() => Ichimoku.ConversionAndBaseAreCrossing ? cloudPositionSignal(Ichimoku.ConversionLine) : TradeSignal.Hold;
		public virtual TradeSignal CalculatePriceXBaseLineSignal() => Ichimoku.PriceAndBaseAreCrossing ? cloudPositionSignal(Candle.Close) : TradeSignal.Hold;
		private TradeSignal cloudPositionSignal(decimal value) => value > Ichimoku.BaseLine ? signalToTheCloud(TradeSignal.StrongShort, TradeSignal.WeakShort, TradeSignal.Hold) : signalToTheCloud(TradeSignal.WeakLong, TradeSignal.StrongLong, TradeSignal.Hold);
		private Trend calculateSpanAXSpanBSignal() => Ichimoku.SpanAIsGreaterThanB ? Trend.Up : Trend.Down;
		private TradeSignal signalToTheCloud(TradeSignal above, TradeSignal bellow, TradeSignal inside) => Ichimoku.IsBellowTheCloud ? bellow : Ichimoku.IsAboveTheCloud ? above : inside;

		public virtual IIchimokuAnalyser Calculate(IIchimokuConfig config, ICandleAnalyser analysis)
		{
			var candles = analysis.Previous;
			calculatesIchimokus(config, candles);
			ConversionXBaseLineSignal = CalculateConversionXBaseLineSignal();
			PriceXBaseLineSignal = CalculatePriceXBaseLineSignal();
			SpanCrossSignal = calculateSpanAXSpanBSignal();
			PriceXCloudSignal = Ichimoku.PriceXCloudPosition;
			LaggedXPriceSignal = Ichimoku.LaggedXPricePosition;
			SpanCrossTradeSignal = getCrossSignal(Ichimoku.SpansAreCrossing, Ichimoku.SpanAIsGreaterThanB );
			LaggingCrossPriceTradeSignal = getCrossSignal(Ichimoku.LaggingAreCrossingPrice, Ichimoku.IsLaggedAboveThePrice);
			return this;
		}
		private TradeSignal getCrossSignal(bool isCrossing, bool isRinsing)
		{
			if (isCrossing)
				return isRinsing ? signalToTheCloud(TradeSignal.StrongShort, TradeSignal.WeakShort, TradeSignal.Short) : signalToTheCloud(TradeSignal.WeakLong, TradeSignal.StrongLong, TradeSignal.Long);

			return TradeSignal.Hold;
		}
		private IIchimokuAnalyser calculatesIchimokus(IIchimokuConfig config, IList<ICandle> candles)
		{
			Ichimokus = new List<IIchimoku>();
			var baseLineCantBeCalculated = config.BaseLine;

			foreach (var candle in candles.Skip(baseLineCantBeCalculated))
				Ichimokus.Add(new Ichimoku(Ichimokus, config));

			Ichimokus = Ichimokus.Where(e => e.Mature).ToList();
			return this;
		}
	}
}
