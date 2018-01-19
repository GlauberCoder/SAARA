using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Entitys.AnalisysConfig
{
	public class RSIConfig : BaseEntity, IRSIConfig
	{
		public virtual int Length { get; set; }
	}
}
