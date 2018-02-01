using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Abstractions.Services
{
	public interface IEMAAnalyser
	{
		decimal ShortEMA { get; }
		decimal LongEMA { get; }
		IEMAAnalyser Calculate(IEMAConfig config, ICandleAnalyser analysis);
	}
}
