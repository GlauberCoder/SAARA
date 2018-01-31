using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Abstractions.Services
{
<<<<<<< HEAD:Domain.Abstractions/Services/IEMAAnaliser.cs
	public interface IEMAAnaliser
=======
    public interface IEMAAnalyser 
>>>>>>> SAARA-3:Domain.Abstractions/Services/IEMAAnalyser.cs
	{
		decimal ShortEMA { get; }
		decimal LongEMA { get; }
		IEMAAnalyser Calculate(IEMAConfig config, ICandleAnalyser analysis);
	}
}
