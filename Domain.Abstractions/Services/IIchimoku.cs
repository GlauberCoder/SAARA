using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Services
{
	/// <summary>
	/// The Ichimoku Cloud is a versatile indicator that defines support and resistance, identifies trend direction, gauges momentum and provides trading signals.
	/// http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:ichimoku_cloud
	/// </summary>
	public interface IIchimoku
	{
		/// <summary>
		/// Also known as Tenkan-sen, an average of the 9-day high and 9-day low
		/// </summary>
		decimal ConversionLine { get; }
		/// <summary>
		/// Also known as Kijun-sen, an average of the 26-day high and 26-day low
		/// </summary>
		decimal BaseLine { get; }
		/// <summary>
		/// Also known as Senkou Span A, the midpoint between the Conversion Line and the Base Line
		/// </summary>
		decimal LeadingSpanA { get; }
		/// <summary>
		/// Also known as Senkou Span B, the midpoint of the 52-day high-low range
		/// </summary>
		decimal LeadingSpanB { get; }
		/// <summary>
		/// Also known as Chikou Span, Close plotted 26 days in the past
		/// </summary>
		decimal LaggingSpan { get; }

		/// <summary>
		/// Also known as Chikou Span, Close plotted 26 days in the past
		/// </summary>
		ICandle Candle { get; }
		decimal LaggingSpanXPriceDiff { get; }
		bool LaggingAreCrossingPrice { get; }
		bool Mature { get; }
		bool SpanAIsGreaterThanB { get; }
		bool SpansAreCrossing { get; }
		bool PriceAndBaseAreCrossing { get; }
		bool ConversionAndBaseAreCrossing { get; }
		bool IsAboveTheCloud { get; }
		bool IsBellowTheCloud { get; }
		bool IsInsideTheCloud { get; }
		Trend PriceXCloudPosition { get; }
		bool IsLaggedAboveThePrice { get; }
		Trend LaggedXPricePosition { get; }
	}
}
