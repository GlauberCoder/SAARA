using Domain.Abstractions.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Entitys.AnalisysConfig
{
	public interface IMACDConfig : IBaseEntity<IMACDConfig>
	{
		int ShortEMA { get; }
		int LongEMA { get; }
		int SignalEMA { get; set; }
		decimal CrossoverTolerance { get; set; }
		decimal CrossunderTolerance { get; set; }
	}
}
