using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using System.Collections.Generic;

namespace Domain.Abstractions.Services
{
	public interface IMACDAnalyser
	{
		IMACD MACD { get; }
		IList<IMACD> MACDs { get; }
		Trend Trend { get; set; }
		TradeSignal CrossSignal { get; set; }
		TradeSignal CenterCrossSignal { get; set; }
		Trend DivergenceSignal { get; set; }
		IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis);
		IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis, ICandle candle);
	}
}
