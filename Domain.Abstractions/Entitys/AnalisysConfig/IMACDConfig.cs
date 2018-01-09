using Domain.Abstractions.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Entitys.AnalisysConfig
{
	public interface IMACDConfig : IBaseEntity
	{
		int ShortEMA { get; }
		int LongEMA { get; }
		int SignalEMA { get; set; }
	}
}
