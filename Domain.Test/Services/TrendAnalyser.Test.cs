using Domain.Abstractions.Enums;
using Domain.Entitys.AnalisysConfig;
using Domain.Extensions;
using Domain.Services;
using Domain.Test.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using Util.Extensions;
using Xunit;

namespace Domain.Test.Services
{
	public class TrendAnalyserTest
	{
		public class AltitudeAnalyserTest
		{

			IList<IList<decimal>> values {
				get {
					var data = new List<IList<decimal>>();
					data.Add(new List<decimal> { 100m, 94m, 98m, 94m, 94m, 96m, 102m, 98m, 94m, 100m, 96m, 104m, 92m, 104m, 94m, 100m, 110m, 104m, 100m, 98m });
					return data;
				}
			}
			
			public TrendAnalyserConfig GetConfig(AltitudeAnalyserMode altitudeAnalyserMode, decimal minTopLength, decimal minBottomLength, TrendAnalyserMode mode)
			{
				var altitudeAnalyserConfig = new AltitudeAnalyserConfig { Mode = altitudeAnalyserMode, MinTop = minTopLength, MinBottom = minBottomLength };
				return new TrendAnalyserConfig { AltitudeAnalyserConfig = altitudeAnalyserConfig, Mode = mode };
			}

			[
				Theory(DisplayName = "The trend analyser should return a correct trend on analysing altitude"),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 0.05, 0.05, 0, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 0, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 0.05, 0.05, 0, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 0, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 0.05, 0.05, 0, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 0, TrendAnalyserMode.HighestAndLowest),
			]
			public void The_trend_analyser_should_return_a_correct_trend_on_analysing_altitude(Trend expected, AltitudeAnalyserMode altitudeAnalyserMode, decimal minTopLength, decimal minBottomLength, int dataIndex, TrendAnalyserMode mode)
			{
				var config = GetConfig(altitudeAnalyserMode, minTopLength, minBottomLength, mode);
				var actual = new TrendAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]));
				Assert.Equal(expected, actual);
			}
		}
	}
}
