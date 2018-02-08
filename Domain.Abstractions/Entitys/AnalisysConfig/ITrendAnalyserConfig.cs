using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Entitys.AnalisysConfig
{
	public interface ITrendAnalyserConfig
	{
		IAltitudeAnalyserConfig AltitudeAnalyserConfig { get; set; }
		TrendAnalyserMode Mode { get; set; }
	}
}
