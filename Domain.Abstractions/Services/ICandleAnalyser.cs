using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using System;
using System.Collections.Generic;

namespace Domain.Abstractions.Services
{
	public interface ICandleAnalyser
	{
		IList<ICandle> Previous { get; set; }
		ICandle Main { get; set; }
		IAnalysisConfig Config { get; set; }
		decimal EMA(int length);
	}
}
