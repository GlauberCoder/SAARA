using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Entitys.AnalisysConfig
{
	public class EMAConfig : BaseEntity<EMAConfig>, IEMAConfig
	{

		public virtual int EMA1 { get; set; }
		public virtual int EMA2 { get; set; }

		public virtual int ShortEMA { get { return EMA1 > EMA2 ? EMA2 : EMA1; } }
		public virtual int LongEMA { get { return EMA1 > EMA2 ? EMA1 : EMA2; } }

		public decimal CrossoverTolerance { get; set; }
		public decimal CrossunderTolerance { get; set; }
		public decimal AverageDistanceTolerance { get; set; }
	}
}
