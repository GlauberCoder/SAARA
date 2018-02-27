using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Abstractions.Services
{
	/// <summary>
	/// The Ichimoku Cloud is a versatile indicator that defines support and resistance, identifies trend direction, gauges momentum and provides trading signals.
	/// http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:ichimoku_cloud
	/// </summary>
	public interface IIchimokuCloudAnalyser
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

		IIchimokuCloudAnalyser Calculate(IIchimokuCloudConfig config, ICandleAnalyser analysis);
	}
}
