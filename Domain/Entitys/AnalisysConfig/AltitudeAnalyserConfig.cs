using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entitys.AnalisysConfig
{
	public class AltitudeAnalyserConfig : IAltitudeAnalyserConfig
	{
		public AltitudeAnalyserMode Mode { get; set; }
		public decimal MinTop { get; set; }
		public decimal MinBottom { get; set; }
	}
}
