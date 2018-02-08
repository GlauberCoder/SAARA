using Domain.Abstractions.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Entitys.AnalisysConfig
{
	public interface IMACDConfig : IBaseEntity<IMACDConfig>
	{
		IEMAConfig EMAConfig { get; set; }
		int SignalEMA { get; set; }
		decimal CrossoverTolerance { get; set; }
		decimal CrossunderTolerance { get; set; }
		int CandlesCountToCrossover { get; set; }
	}
}
