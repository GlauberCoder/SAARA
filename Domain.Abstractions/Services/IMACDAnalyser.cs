using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Services
{
    public interface IMACDAnalyser
	{
		decimal LongEMA { get; set; }
		decimal ShortEMA { get; set; }
		decimal MACD { get; set; }
		decimal Signal { get; set; }
		decimal Histogram { get; set; }
		IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis);
		IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis, ICandle candle);
	}
}
