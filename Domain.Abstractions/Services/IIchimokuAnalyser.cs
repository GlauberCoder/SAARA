using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;

namespace Domain.Abstractions.Services
{
	/// <summary>
	/// The Ichimoku Cloud is a versatile indicator that defines support and resistance, identifies trend direction, gauges momentum and provides trading signals.
	/// http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:ichimoku_cloud
	/// </summary>
	public interface IIchimokuAnalyser
	{
		TradeSignal ConversionXBaseLineSignal { get; }
		TradeSignal PriceXBaseLineSignal { get; }
		Trend SpanCrossSignal { get; }
		Trend PriceXCloudSignal { get; }
		TradeSignal SpanCrossTradeSignal { get; }
		TradeSignal LaggingCrossPriceTradeSignal { get; }
		IIchimokuAnalyser Calculate(IIchimokuConfig config, ICandleAnalyser analysis);

	}
}
