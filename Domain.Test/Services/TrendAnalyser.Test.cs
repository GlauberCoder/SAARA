using Domain.Abstractions.Enums;
using Domain.Extensions;
using Domain.Services;
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
			[
				Theory(DisplayName = "The trend analyser should return a correct trend on analysing by most recents using altitude analyser by variation"),
				InlineData(Trend.Up, 0.05, 0.05, new double[] { 100, 94, 98, 94, 94, 96, 102, 98, 94, 100, 96, 104, 92, 104, 94, 100, 110, 104, 100, 98 }, Altitude.Top),
				InlineData(Trend.Up, 0.05, 0.05, new double[] { 100, 94, 98, 94, 94, 96, 102, 98, 94, 100, 96, 104, 92, 104, 94, 100, 110, 104, 100, 98 }, Altitude.Bottom),
			]
			public void The_trend_analyser_should_return_a_correct_trend_on_analysing_by_most_recent_using_altitude_analyser_by_variation(Trend expected, decimal minTopLength, decimal minBottomLength, double[] values, Altitude altitude)
			{
				var analyser = new AltitudeAnalyser().ByVariation(minTopLength, minBottomLength);
				var actual = new TrendAnalyser()
															.ByMostRecents(altitude)
															.Identify(values.CastAs<decimal>(), analyser);
				Assert.Equal(expected, actual);
			}
			[
				Theory(DisplayName = "The trend analyser should return a correct trend on analysing by first vs last recents using altitude analyser by variation"),
				InlineData(Trend.Up, 0.05, 0.05, new double[] { 100, 94, 98, 94, 94, 96, 102, 98, 94, 100, 96, 104, 92, 104, 94, 100, 110, 104, 100, 98 }, Altitude.Top),
				InlineData(Trend.Neutral, 0.05, 0.05, new double[] { 100, 94, 98, 94, 94, 96, 102, 98, 94, 100, 96, 104, 92, 104, 94, 100, 110, 104, 100, 98 }, Altitude.Bottom),
			]
			public void The_trend_analyser_should_return_a_correct_trend_on_analysing_by_first_vs_last_using_altitude_analyser_by_variation(Trend expected, decimal minTopLength, decimal minBottomLength, double[] values, Altitude altitude)
			{
				var analyser = new AltitudeAnalyser().ByVariation(minTopLength, minBottomLength);
				var actual = new TrendAnalyser()
															.ByFirstVsLast(altitude)
															.Identify(values.CastAs<decimal>(), analyser);
				Assert.Equal(expected, actual);
			}
			[
				Theory(DisplayName = "The trend analyser should return a correct trend on analysing by highest and lowest using altitude analyser by variation"),
				InlineData(Trend.Up, 0.05, 0.05, new double[] { 100, 94, 98, 94, 94, 96, 102, 98, 94, 100, 96, 104, 92, 104, 94, 100, 110, 104, 100, 98 }, Altitude.Top),
				InlineData(Trend.Up, 0.05, 0.05, new double[] { 100, 94, 98, 94, 94, 96, 102, 98, 94, 100, 96, 104, 92, 104, 94, 100, 110, 104, 100, 98 }, Altitude.Bottom),
			]
			public void The_trend_analyser_should_return_a_correct_trend_on_analysing_by_highest_and_lowest_using_altitude_analyser_by_variation(Trend expected, decimal minTopLength, decimal minBottomLength, double[] values, Altitude altitude)
			{
				var analyser = new AltitudeAnalyser().ByVariation(minTopLength, minBottomLength);
				var actual = new TrendAnalyser()
															.ByMostRecents(altitude)
															.Identify(values.CastAs<decimal>(), analyser);
				Assert.Equal(expected, actual);
			}
		}
	}
}
