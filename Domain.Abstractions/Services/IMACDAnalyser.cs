using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using System.Collections.Generic;

namespace Domain.Abstractions.Services
{
	/// <summary>
	/// MACD Analyser provides tools to explore MACD oscillator, by calculating trend and trade signals
	/// </summary>
	public interface IMACDAnalyser
	{
		IMACD MACD { get; }
		IList<IMACD> MACDs { get; }
		/// <summary>
		/// Trend based on actual MACD and Signal
		/// </summary>
		Trend Trend { get; set; }
		/// <summary>
		/// Trade signal based on MACD and Signal, given that there was a cross
		/// </summary>
		TradeSignal CrossSignal { get; set; }
		/// <summary>
		/// Trade based on MACD, given that there was a cross between MACD and line 0
		/// </summary>
		TradeSignal CenterCrossSignal { get; set; }
		/// <summary>
		/// Trend based on comparing the trend of MACD and the trend of candle's closing price
		/// </summary>
		Trend DivergenceSignal { get; set; }
		IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis);
		IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis, ICandle candle);
	}
}
