using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Entitys.AnalisysConfig
{
	public class RSIConfig : BaseEntity<RSIConfig>, IRSIConfig
	{
		public virtual int Length { get; set; }
	}
}
