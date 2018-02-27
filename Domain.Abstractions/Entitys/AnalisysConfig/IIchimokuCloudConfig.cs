using Domain.Abstractions.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Entitys.AnalisysConfig
{
	public interface IIchimokuCloudConfig : IBaseEntity<IIchimokuCloudConfig>
	{
		int ConversionLine { get; }
		int BaseLine { get; }
		int LeadingSpanB { get; }
		int LaggingSpan { get; }
	}
}
