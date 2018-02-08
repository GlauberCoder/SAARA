using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Entitys.AnalisysConfig
{
	public class MACDConfig : BaseEntity<MACDConfig>, IMACDConfig
	{
		public IEMAConfig EMAConfig { get; set; }
		public virtual int SignalEMA { get; set; }
		public virtual decimal CrossoverTolerance { get; set; }
		public virtual decimal CrossunderTolerance { get; set; }
		public int CandlesCountToCrossover { get; set; }
	}
}
