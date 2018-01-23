using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Abstractions.Services
{
	public interface IEMAAnaliser
	{
		decimal ShortEMA { get; set; }
		decimal LongEMA { get; set; }
		IEMAAnaliser Calculate(IEMAConfig config, ICandleAnalyser analysis);
	}
}
