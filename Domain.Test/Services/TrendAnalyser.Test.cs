using Domain.Abstractions.Enums;
using Domain.Entitys.AnalisysConfig;
using Domain.Services;
using Domain.Test.Mock;
using System.Collections.Generic;
using Xunit;

namespace Domain.Test.Services
{
	public class TrendAnalyserTest
	{
		public class AltitudeAnalyserTest
		{

			IList<IList<decimal>> values
			{
				get
				{
					var data = new List<IList<decimal>>();
					data.Add(new List<decimal> { 100m, 94m, 98m, 94m, 94m, 96m, 102m, 98m, 94m, 100m, 96m, 104m, 92m, 104m, 94m, 100m, 110m, 104m, 100m, 98m });
					data.Add(new List<decimal> { 82m, 94m, 98m, 100m, 94m, 89m, 102m, 104m, 94m, 100m, 106m, 104m, 103m, 98m, 108m, 102m, 100m, 108m, 110m, 111m });
					data.Add(new List<decimal> { 111m, 110m, 108m, 100m, 102m, 108m, 98m, 103m, 104m, 106m, 100m, 94m, 104m, 102m, 89m, 94m, 100m, 98m, 94m, 82m });
					data.Add(new List<decimal> { 94m, 108m, 102m, 100m, 104m, 111m, 104m, 94m, 110m, 100m, 89m, 103m, 100m, 94m, 82m, 98m, 106m, 102m, 98m, 108m });
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
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 5, 5, 0, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 0, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 5, 5, 0, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 0, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 5, 5, 0, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 0, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 5, 5, 0, TrendAnalyserMode.Highest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 0, TrendAnalyserMode.Highest),

				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 5, 5, 1, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 1, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 5, 5, 1, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 1, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 5, 5, 1, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 1, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 5, 5, 1, TrendAnalyserMode.Highest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 1, TrendAnalyserMode.Highest),

				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 5, 5, 2, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 3, 3, 2, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 5, 5, 2, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 3, 3, 2, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 5, 5, 2, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 3, 3, 2, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 5, 5, 2, TrendAnalyserMode.Highest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 3, 3, 2, TrendAnalyserMode.Highest),

				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 5, 5, 3, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 3, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Neutral, AltitudeAnalyserMode.Variation, 5, 5, 3, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Neutral, AltitudeAnalyserMode.Length, 3, 3, 3, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 5, 5, 3, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 3, 3, 3, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 5, 5, 3, TrendAnalyserMode.Highest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 3, 3, 3, TrendAnalyserMode.Highest),
			]
			public void The_trend_analyser_should_return_a_correct_trend_on_analysing_altitude(Trend expected, AltitudeAnalyserMode altitudeAnalyserMode, decimal minTopLength, decimal minBottomLength, int dataIndex, TrendAnalyserMode mode)
			{
				var config = GetConfig(altitudeAnalyserMode, minTopLength, minBottomLength, mode);
				var actual = new TrendAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]));
				Assert.Equal(expected, actual);
			}


			[
				Theory(DisplayName = "The trend analyser should return a correct trend on analysing altitude with minTops greater than minBottoms on uptrend"),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 7, 3, 1, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Neutral, AltitudeAnalyserMode.Length, 4, 2, 1, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 7, 3, 1, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Neutral, AltitudeAnalyserMode.Length, 4, 2, 1, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 7, 3, 1, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Neutral, AltitudeAnalyserMode.Length, 4, 2, 1, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 7, 3, 1, TrendAnalyserMode.Highest),
				InlineData(Trend.Neutral, AltitudeAnalyserMode.Length, 4, 2, 1, TrendAnalyserMode.Highest)
			]
			public void The_trend_analyser_should_return_a_correct_trend_on_analysing_altitude_with_minTops_greater_than_minBottoms_on_uptrend(Trend expected, AltitudeAnalyserMode altitudeAnalyserMode, decimal minTopLength, decimal minBottomLength, int dataIndex, TrendAnalyserMode mode)
			{
				var config = GetConfig(altitudeAnalyserMode, minTopLength, minBottomLength, mode);
				var actual = new TrendAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]));
				Assert.Equal(expected, actual);
			}

			[
				Theory(DisplayName = "The trend analyser should return a correct trend on analysing altitude with minBottoms greater than minTops on uptrend"),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 3, 7, 1, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 2, 4, 1, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 3, 7, 1, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 2, 4, 1, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 3, 7, 1, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 2, 4, 1, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 3, 7, 1, TrendAnalyserMode.Highest),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 2, 4, 1, TrendAnalyserMode.Highest)
			]
			public void The_trend_analyser_should_return_a_correct_trend_on_analysing_altitude_with_minBottoms_greater_than_minTops_on_uptrend(Trend expected, AltitudeAnalyserMode altitudeAnalyserMode, decimal minTopLength, decimal minBottomLength, int dataIndex, TrendAnalyserMode mode)
			{
				var config = GetConfig(altitudeAnalyserMode, minTopLength, minBottomLength, mode);
				var actual = new TrendAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]));
				Assert.Equal(expected, actual);
			}


			[
				Theory(DisplayName = "The trend analyser should return a correct trend on analysing altitude with minTops greater than minBottoms on donwtrend"),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 7, 3, 2, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 4, 2, 2, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 7, 3, 2, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 4, 2, 2, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 7, 3, 2, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 4, 2, 2, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 7, 3, 2, TrendAnalyserMode.Highest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 4, 2, 2, TrendAnalyserMode.Highest)
			]
			public void The_trend_analyser_should_return_a_correct_trend_on_analysing_altitude_with_minTops_greater_than_minBottoms_on_downtrend(Trend expected, AltitudeAnalyserMode altitudeAnalyserMode, decimal minTopLength, decimal minBottomLength, int dataIndex, TrendAnalyserMode mode)
			{
				var config = GetConfig(altitudeAnalyserMode, minTopLength, minBottomLength, mode);
				var actual = new TrendAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]));
				Assert.Equal(expected, actual);
			}


			[
				Theory(DisplayName = "The trend analyser should return a correct trend on analysing altitude with minBottoms greater than minTops on downtrend"),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 3, 7, 2, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 2, 4, 2, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 3, 7, 2, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 2, 4, 2, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 3, 7, 2, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 2, 4, 2, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 3, 7, 2, TrendAnalyserMode.Highest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 2, 4, 2, TrendAnalyserMode.Highest)
			]
			public void The_trend_analyser_should_return_a_correct_trend_on_analysing_altitude_with_minBottoms_greater_than_minTops_on_downtrend(Trend expected, AltitudeAnalyserMode altitudeAnalyserMode, decimal minTopLength, decimal minBottomLength, int dataIndex, TrendAnalyserMode mode)
			{
				var config = GetConfig(altitudeAnalyserMode, minTopLength, minBottomLength, mode);
				var actual = new TrendAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]));
				Assert.Equal(expected, actual);
			}

			[
				Theory(DisplayName = "The trend analyser should return a correct trend on analysing altitude with minTops greater than minBottoms"),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 7, 3, 3, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 4, 2, 3, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Neutral, AltitudeAnalyserMode.Variation, 7, 3, 3, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 4, 2, 3, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 7, 3, 3, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 4, 2, 3, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 7, 3, 3, TrendAnalyserMode.Highest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 4, 2, 3, TrendAnalyserMode.Highest)
			]
			public void The_trend_analyser_should_return_a_correct_trend_on_analysing_altitude_with_minTops_greater_than_minBottoms(Trend expected, AltitudeAnalyserMode altitudeAnalyserMode, decimal minTopLength, decimal minBottomLength, int dataIndex, TrendAnalyserMode mode)
			{
				var config = GetConfig(altitudeAnalyserMode, minTopLength, minBottomLength, mode);
				var actual = new TrendAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]));
				Assert.Equal(expected, actual);
			}

			[
				Theory(DisplayName = "The trend analyser should return a correct trend on analysing altitude with minBottoms greater than minTops"),
				InlineData(Trend.Up, AltitudeAnalyserMode.Variation, 3, 7, 3, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Up, AltitudeAnalyserMode.Length, 2, 4, 3, TrendAnalyserMode.MostRecents),
				InlineData(Trend.Neutral, AltitudeAnalyserMode.Variation, 3, 7, 3, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Neutral, AltitudeAnalyserMode.Length, 2, 4, 3, TrendAnalyserMode.FirstAndLast),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 3, 7, 3, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Length, 2, 4, 3, TrendAnalyserMode.HighestAndLowest),
				InlineData(Trend.Down, AltitudeAnalyserMode.Variation, 3, 7, 3, TrendAnalyserMode.Highest),
				InlineData(Trend.Neutral, AltitudeAnalyserMode.Length, 2, 4, 3, TrendAnalyserMode.Highest)
			]
			public void The_trend_analyser_should_return_a_correct_trend_on_analysing_altitude_with_minBottoms_greater_than_minTops(Trend expected, AltitudeAnalyserMode altitudeAnalyserMode, decimal minTopLength, decimal minBottomLength, int dataIndex, TrendAnalyserMode mode)
			{
				var config = GetConfig(altitudeAnalyserMode, minTopLength, minBottomLength, mode);
				var actual = new TrendAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]));
				Assert.Equal(expected, actual);
			}
		}
	}
}
