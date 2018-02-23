using Domain.Entitys.AnalisysConfig;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Domain.Test.Entitys.AnalysisConfig
{
	public class MACDConfigTest
	{

		[
			Theory(DisplayName = "The short EMA of MACD config should be the lowest value"),
			InlineData(1, 2, 1),
			InlineData(1, 1, 1),
			InlineData(1, 1, 2)
		]
		public void The_short_EMA_of_MACD_config_should_be_the_lowest_value(int expected, int value1, int value2)
		{
			var actual = new MACDConfig { EMAConfig = new EMAConfig { EMA1 = value1, EMA2 = value2 } }.EMAConfig.ShortEMA;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The long EMA of MACD config should be the greatest value"),
			InlineData(2, 2, 1),
			InlineData(1, 1, 1),
			InlineData(2, 1, 2)
		]
		public void The_long_EMA_of_MACD_config_should_be_the_greatest_value(int expected, int value1, int value2)
		{
			var actual = new MACDConfig { EMAConfig = new EMAConfig { EMA1 = value1, EMA2 = value2 } }.EMAConfig.LongEMA;
			Assert.Equal(expected, actual);
		}
	}
}
