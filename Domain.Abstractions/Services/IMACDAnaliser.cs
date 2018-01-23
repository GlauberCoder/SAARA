using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Abstractions.Services
{
	public interface IMACDAnaliser
	{
		decimal LongEMA { get; set; }
		decimal ShortEMA { get; set; }
		decimal MACD { get; set; }
		decimal Signal { get; set; }
		decimal Histogram { get; set; }
		IMACDAnaliser Calculate(IMACDConfig config, ICandleAnalyser analysis);
		IMACDAnaliser Calculate(IMACDConfig config, ICandleAnalyser analysis, ICandle candle);
	}
}
