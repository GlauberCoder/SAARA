using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Services
{
    public interface IEMAAnalyser 
	{
		decimal ShortEMA { get; set; }
		decimal LongEMA { get; set; }
		IEMAAnalyser Calculate(IEMAConfig config, ICandleAnalyser analysis);
	}
}
