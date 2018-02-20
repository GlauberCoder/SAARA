using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Services
{
	/// <summary>
	/// Moving Average Convergence/Divergence Oscillator (MACD) is a momentum indicator that offers trend following and momentum. The MACD fluctuates above and below the zero line as the moving averages converge, cross and diverge.
	/// http://http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:moving_average_convergence_divergence_macd
	/// </summary>
	public interface IMACD : ICanBeClassifiedByAltitude
	{
		/// <summary>
		/// Short-term Exponential Moving Average
		/// </summary>
		decimal ShortEMA { get; set; }
		/// <summary>
		/// Long-term Exponential Moving Average
		/// </summary>
		decimal LongEMA { get; set; }
		/// <summary>
		/// Moving Average Convergence/Divergence (MACD) is the diference between Short-EMA and Long-EMA
		/// </summary>
		decimal Value { get; set; }
		/// <summary>
		/// Signal is MACD's Exponential Moving Average
		/// </summary>
		decimal Signal { get; set; }
		/// <summary>
		/// Histogram is the difference between MACD and Signal
		/// </summary>
		decimal Histogram { get; set; }
		/// <summary>
		/// Candle used to calculate MACD
		/// </summary>
		ICandle Candle { get; set; }
		/// <summary>
		/// Trend based on actual MACD and Signal
		/// </summary>
		Trend Trend { get; }
		/// <summary>
		/// Trade signal based on actual trend
		/// </summary>
		TradeSignal TradeSignal { get; }
		/// <summary>
		/// Trade signal based on MACD and Signal, given that there was a cross
		/// </summary>
		TradeSignal CrossTradeSignal { get; }
		IMACD CalculateValue();
		IMACD CalculateSignal(IList<IMACD> macds, int signalEMA);
		IMACD CalculateHistogram();
	}
}
