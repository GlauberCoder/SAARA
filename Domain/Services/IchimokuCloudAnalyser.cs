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
	public class IchimokuCloudAnalyser : IIchimokuCloudAnalyser
	{
		public virtual decimal ConversionLine { get; set; }
		public virtual decimal BaseLine { get; set; }
		public virtual decimal LeadingSpanA { get; set; }
		public virtual decimal LeadingSpanB { get; set; }
		public virtual decimal LaggingSpan { get; set; }
		public virtual IList<IIchimokuCloudAnalyser> Previous { get; set; }
		public virtual IIchimokuCloudAnalyser ReferenceToCloud { get; set; }
		public virtual ICandle Candle { get; set; }
		//TODO: identify lagging
		public IchimokuCloudAnalyser()
		{
		}
		public IchimokuCloudAnalyser(IIchimokuCloudConfig config, ICandleAnalyser analysis) : this()
		{
			Calculate(config, analysis);
		}
		public virtual IIchimokuCloudAnalyser Calculate(IIchimokuCloudConfig config, ICandleAnalyser analysis)
		{
			var candles = analysis.Previous;
			CalculateIchimokuCloudBase(config, candles);
			//calculatePreviousIchimokuCloud(config, candles);

			return this;
		}
		public virtual IIchimokuCloudAnalyser CalculateIchimokuCloudBase(IIchimokuCloudConfig config, IList<ICandle> previousCandles)
		{
			ConversionLine = CalculateAverageBetweenHighAndLow(previousCandles, config.ConversionLine);
			BaseLine = CalculateAverageBetweenHighAndLow(previousCandles, config.BaseLine);
			LeadingSpanA = CalculateMidPoint(ConversionLine, BaseLine);
			LeadingSpanB = CalculateAverageBetweenHighAndLow(previousCandles, config.LeadingSpanB);
			LaggingSpan = TakeLastCloseCandleValue(previousCandles, config.LaggingSpan);

			return this;
		}
		private decimal CalculateAverageBetweenHighAndLow(IList<ICandle> candles, int takeLast)
		{
			candles = candles.TakeLast(takeLast).ToList();
			var high = candles.Max(c => c.Close);
			var low = candles.Min(c => c.Close);
			return CalculateMidPoint(high, low);
		}
		private decimal CalculateMidPoint(decimal point, decimal otherPoint)
		{
			return (point + otherPoint) / 2;
		}
		private decimal TakeLastCloseCandleValue(IList<ICandle> candles, int pastCount)
		{
			return candles.TakeLast(pastCount).FirstOrDefault().Close;
		}
		public virtual TradeSignal CalculateConversionBaseCrossover()
		{
			if(conversionAndBaseAreCrossing() && ConversionLine > BaseLine)
				return isBellowTheCloud(ConversionLine) ? TradeSignal.WeakLong : isAboveTheCloud(ConversionLine) ? TradeSignal.StrongLong : TradeSignal.Hold;
			if(conversionAndBaseAreCrossing() && ConversionLine < BaseLine)
				return isBellowTheCloud(ConversionLine) ? TradeSignal.StrongShort : isAboveTheCloud(ConversionLine) ? TradeSignal.WeakShort : TradeSignal.Hold;
			return TradeSignal.Hold;
		}
		private bool conversionAndBaseAreCrossing()
		{
			var candles = Previous;
			candles.Add(this);
			var result = candles.Select(p => p.ConversionLine - p.BaseLine).ToList().LastValueIsCrossing();
			candles.Remove(this);
			return result;
		}
		private IIchimokuCloudAnalyser calculatePreviousIchimokuCloud(IIchimokuCloudConfig config, IList<ICandle> candles)
		{
			Previous = new List<IIchimokuCloudAnalyser>();
			foreach (var candle in candles)
				Previous.Add( new IchimokuCloudAnalyser { Candle = candle }
										.CalculateIchimokuCloudBase(config, candles.TakePrevious(candle, candles.Count)) );
			ReferenceToCloud = Previous.TakeLast(config.LaggingSpan).FirstOrDefault();
			return this;
		}
		private IchimokuCloud ichimokuCloudColor(IIchimokuCloudConfig config)
		{
			if (ReferenceToCloud.LeadingSpanA == ReferenceToCloud.LeadingSpanB)
				return IchimokuCloud.Neutral;
			return ReferenceToCloud.LeadingSpanA > ReferenceToCloud.LeadingSpanB ? IchimokuCloud.Green : IchimokuCloud.Red;
		}
		private bool isInsideTheCloud(decimal value)
		{
			return !isAboveTheCloud(value) && !isBellowTheCloud(value);
		}
		private bool isBellowTheCloud(decimal value)
		{
			return value < ReferenceToCloud.LeadingSpanA && value < ReferenceToCloud.LeadingSpanB;
		}
		private bool isAboveTheCloud(decimal value)
		{
			return value > ReferenceToCloud.LeadingSpanA && value > ReferenceToCloud.LeadingSpanB;
		}
	}
}
