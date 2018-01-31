using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Abstractions.Services
{
<<<<<<< HEAD:Domain.Abstractions/Services/IMACDAnaliser.cs
	public interface IMACDAnaliser
=======
    public interface IMACDAnalyser
>>>>>>> SAARA-3:Domain.Abstractions/Services/IMACDAnalyser.cs
	{
		decimal LongEMA { get; set; }
		decimal ShortEMA { get; set; }
		decimal MACD { get; set; }
		decimal Signal { get; set; }
		decimal Histogram { get; set; }
		IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis);
		IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis, ICandle candle);
	}
}
