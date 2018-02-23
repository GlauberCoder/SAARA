using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entitys.AnalisysConfig
{
	public class TrendAnalyserConfig : ITrendAnalyserConfig
	{
		public IAltitudeAnalyserConfig AltitudeAnalyserConfig { get; set; }
		public TrendAnalyserMode Mode { get; set; }
	}
}
