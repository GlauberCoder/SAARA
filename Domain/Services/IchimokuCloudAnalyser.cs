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
		public virtual decimal ConversionLine { get; private set; }
		public virtual decimal BaseLine { get; private set; }
		public virtual decimal LeadingSpanA { get; private set; }
		public virtual decimal LeadingSpanB { get; private set; }
		public virtual decimal LaggingSpan { get; private set; }

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

			ConversionLine = CalculateAverageBetweenHighAndLow(candles, config.ConversionLine);
			BaseLine = CalculateAverageBetweenHighAndLow(candles, config.BaseLine);
			LeadingSpanA = CalculateMidPoint(ConversionLine, BaseLine);
			LeadingSpanB = CalculateAverageBetweenHighAndLow(candles, config.LeadingSpanB);
			LaggingSpan = TakeLastCloseCandleValue(candles, config.LaggingSpan);

			return this;
		}
		private decimal CalculateAverageBetweenHighAndLow(IList<ICandle> candles, int takeLast)
		{
			candles = candles.TakeLast(takeLast).ToList();
			var high = candles.Max().Close;
			var low = candles.Min().Close;
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

	}
}
