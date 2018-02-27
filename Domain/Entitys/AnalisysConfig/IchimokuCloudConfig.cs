using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Entitys.AnalisysConfig
{
	public class IchimokuCloudConfig : BaseEntity<IchimokuCloudConfig>, IIchimokuCloudConfig
	{
		public virtual int ConversionLine { get; set; }
		public virtual int BaseLine { get; set; }
		public virtual int LeadingSpanB { get; set; }
		public virtual int LaggingSpan { get; set; }
	}
}
