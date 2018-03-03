using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Extensions;

namespace Domain.Services
{
    public class Ichimoku : IIchimoku
	{
		public virtual decimal ConversionLine { get; }
		public virtual decimal BaseLine { get; }
		public virtual decimal LeadingSpanA { get; }
		public virtual decimal LeadingSpanB { get; }
		public virtual decimal LaggingSpan { get; }
		public virtual decimal LaggingSpanXPriceDiff { get; }
		public virtual ICandle Candle { get; }
		public virtual bool SpanAIsGreaterThanB { get; } 
		public virtual bool SpansAreCrossing { get; }
		public virtual bool PriceAndBaseAreCrossing { get; }
		public virtual bool ConversionAndBaseAreCrossing { get; }
		public virtual bool Mature { get; }
		public virtual Trend PriceXCloudPosition { get; }
		public virtual Trend LaggedXPricePosition { get; }
		public virtual bool LaggingAreCrossingPrice { get; }
		public virtual bool IsAboveTheCloud => PriceXCloudPosition == Trend.Up;
		public virtual bool IsBellowTheCloud => PriceXCloudPosition == Trend.Down;
		public virtual bool IsInsideTheCloud => PriceXCloudPosition == Trend.Neutral;
		public virtual bool IsLaggedAboveThePrice => LaggedXPricePosition == Trend.Up;

		public Ichimoku(IList<IIchimoku> references, int conversionLinePeriods, int baseLinePeriods, int laggingSpan2Periods, int displacement)
		{
			var candles = references.Select(r => r.Candle);
			Candle = candles.Last();
			LaggingSpan = Candle.Close;

			if (references.Count > conversionLinePeriods)
				ConversionLine = candles.TakeLast(conversionLinePeriods).ToList().MaxMinAverage();

			if (references.Count > baseLinePeriods)
				BaseLine = candles.TakeLast(baseLinePeriods).ToList().MaxMinAverage();
				LeadingSpanA = ConversionLine.Avarage(BaseLine);

			if (references.Count > laggingSpan2Periods)
			{
				LeadingSpanB = candles.TakeLast(laggingSpan2Periods).ToList().MaxMinAverage();
			}

			if (references.Count > laggingSpan2Periods + displacement)
			{
				var reference = references.ElementAtBackwardIndex(displacement);
				SpanAIsGreaterThanB = LeadingSpanA > LeadingSpanB;
				SpansAreCrossing = references.SelectList(i => i.LeadingSpanA - i.LeadingSpanB).LastValueIsCrossing();
				PriceAndBaseAreCrossing = references.SelectList(i => i.Candle.Close - i.BaseLine).LastValueIsCrossing();
				ConversionAndBaseAreCrossing = references.SelectList(i => i.ConversionLine - i.BaseLine).LastValueIsCrossing();
				PriceXCloudPosition = calculateCloseXCloudPosition(reference.LeadingSpanA, reference.LeadingSpanB);

				LaggingSpanXPriceDiff = Candle.Close - reference.Candle.Close;
				SpansAreCrossing = references.SelectList(i => i.LaggingSpanXPriceDiff).LastValueIsCrossing();
				LaggedXPricePosition = calculatePriceXLaggingPosition();

				Mature = true;
			}
		}

		private Trend calculateCloseXCloudPosition(decimal referenceLeadingA, decimal referenceLeadingB)
		{
			if (Candle.Close > referenceLeadingA && Candle.Close > referenceLeadingB) return Trend.Up;
			if (Candle.Close < referenceLeadingA && Candle.Close < referenceLeadingB) return Trend.Down;
			return Trend.Neutral;
		}
		private Trend calculatePriceXLaggingPosition()
		{
			if (LaggingSpanXPriceDiff > 0) return Trend.Up;
			if (LaggingSpanXPriceDiff < 0) return Trend.Down;
			return Trend.Neutral;
		}

		public Ichimoku(IList<IIchimoku> references, IIchimokuConfig config) : this(references, config.ConversionLine, config.BaseLine, config.LeadingSpanB, config.LaggingSpan)
		{

		}
	}
}
