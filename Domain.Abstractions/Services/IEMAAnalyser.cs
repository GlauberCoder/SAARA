using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Abstractions.Services
{
	/// <summary>
	///  Exponential Moving Averages smooth the price data to form a trend following indicator, defining the current direction with a lag. It can be used to identify the direction of the trend or define potential support and resistance levels.
	/// http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:moving_averages
	/// </summary>
	public interface IEMAAnalyser
	{
		/// <summary>
		/// Short-term Exponential Moving Average
		/// </summary>
		decimal ShortEMA { get; }
		/// <summary>
		/// Long-term Exponential Moving Average
		/// </summary>
		decimal LongEMA { get; }
		IEMAAnalyser Calculate(IEMAConfig config, ICandleAnalyser analysis);
	}
}
