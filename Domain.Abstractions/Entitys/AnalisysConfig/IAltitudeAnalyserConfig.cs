using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Entitys.AnalisysConfig
{
	public interface IAltitudeAnalyserConfig
	{
		AltitudeAnalyserMode Mode { get; set; }
		decimal MinTop { get; set; }
		decimal MinBottom { get; set; }
	}
}
