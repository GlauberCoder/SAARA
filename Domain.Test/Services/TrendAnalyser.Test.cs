using Domain.Abstractions.Enums;
using Domain.Entitys.AnalisysConfig;
using Domain.Extensions;
using Domain.Services;
using Domain.Test.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Domain.Test.Services
{
	public class TrendAnalyserTest
	{
		public class AltitudeAnalyserTest
		{
			public TrendAnalyserConfig GetConfig(AltitudeAnalyserMode altitudeAnalyserMode, decimal minTopLength, decimal minBottomLength, TrendAnalyserMode mode)
			{
				var altitudeAnalyserConfig = new AltitudeAnalyserConfig { Mode = altitudeAnalyserMode, MinTop = minTopLength, MinBottom = minBottomLength };
				return new TrendAnalyserConfig { AltitudeAnalyserConfig = altitudeAnalyserConfig, Mode = mode };
			}

			[
				Theory(DisplayName = "The trend analyser should return a correct trend on analysing by most recents using altitude analyser by variation"),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 0.05, 0.05, new double[] { 100, 94, 98, 94, 94, 96, 102, 98, 94, 100, 96, 104, 92, 104, 94, 100, 110, 104, 100, 98 }, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, new double[] { 100, 94, 98, 94, 94, 96, 102, 98, 94, 100, 96, 104, 92, 104, 94, 100, 110, 104, 100, 98 }, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 0.05, 0.05, new double[] { 100, 94, 98, 94, 94, 96, 102, 98, 94, 100, 96, 104, 92, 104, 94, 100, 110, 104, 100, 98 }, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, new double[] { 100, 94, 98, 94, 94, 96, 102, 98, 94, 100, 96, 104, 92, 104, 94, 100, 110, 104, 100, 98 }, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 0.05, 0.05, new double[] { 100, 94, 98, 94, 94, 96, 102, 98, 94, 100, 96, 104, 92, 104, 94, 100, 110, 104, 100, 98 }, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, new double[] { 100, 94, 98, 94, 94, 96, 102, 98, 94, 100, 96, 104, 92, 104, 94, 100, 110, 104, 100, 98 }, TrendAnalyserMode.HighestAndLowest),

				]
			public void The_trend_analyser_should_return_a_correct_trend_on_analysing_by_most_recent_using_altitude_analyser_by_variation(Trend expected, AltitudeAnalyserMode altitudeAnalyserMode, decimal minTopLength, decimal minBottomLength, double[] values, TrendAnalyserMode mode)
			{
				var config = GetConfig(altitudeAnalyserMode, minTopLength, minBottomLength, mode);
				var actual = new TrendAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values));
				Assert.Equal(expected, actual);
			}
		}
	}
}
