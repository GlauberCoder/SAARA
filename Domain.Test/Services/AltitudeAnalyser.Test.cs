using Domain.Abstractions.Enums;
using Domain.Entitys.AnalisysConfig;
using Domain.Services;
using Domain.Test.Mock;
using System.Collections.Generic;
using Xunit;

namespace Domain.Test.Services
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
				data.Add(new List<decimal> { 100m, 101m, 103m, 105m, 90m, 92m, 89m, 90m, 100m });
				data.Add(new List<decimal> { 1m, 1.2m, 2m, 1.3m, 1.4m, 1.2m, 1.5m, 1.6m, 1.5m });
				data.Add(new List<decimal> { 1m, 1.2m, 2m, 1.3m });
				data.Add(new List<decimal> { 1.2m, 2m, 1.3m, 1.2m });
				data.Add(new List<decimal> { 2m, 1.3m, 1.4m, 1.2m });
				data.Add(new List<decimal> { 1.3m, 1.4m, 1.2m, 1.5m });
				data.Add(new List<decimal> { 1.2m, 1.5m, 1.6m, 1.5m });
				return data;
			}
		}
		[
			Theory(DisplayName = "The altitude analyser should return a correct altitude list on analysing by variation"),
			InlineData(Altitude.Neutral, 0, 10, 10, 4),
			InlineData(Altitude.Neutral, 1, 10, 10, 4),
			InlineData(Altitude.Neutral, 2, 10, 10, 4),
			InlineData(Altitude.Top, 3, 10, 10, 4),
			InlineData(Altitude.Neutral, 4, 10, 10, 4),
			InlineData(Altitude.Neutral, 5, 10, 10, 4),
			InlineData(Altitude.Bottom, 6, 10, 10, 4),
			InlineData(Altitude.Neutral, 7, 10, 10, 4),
			InlineData(Altitude.Top, 8, 10, 10, 4),

			InlineData(Altitude.Top, 0, 5, 5, 0),
			InlineData(Altitude.Bottom, 1, 5, 5, 0),
			InlineData(Altitude.Neutral, 2, 5, 5, 0),
			InlineData(Altitude.Neutral, 3, 5, 5, 0),
			InlineData(Altitude.Neutral, 4, 5, 5, 0),
			InlineData(Altitude.Neutral, 5, 5, 5, 0),
			InlineData(Altitude.Top, 6, 5, 5, 0),
			InlineData(Altitude.Neutral, 7, 5, 5, 0),
			InlineData(Altitude.Bottom, 8, 5, 5, 0),
			InlineData(Altitude.Neutral, 9, 5, 5, 0),
			InlineData(Altitude.Neutral, 10, 5, 5, 0),
			InlineData(Altitude.Top, 11, 5, 5, 0),
			InlineData(Altitude.Bottom, 12, 5, 5, 0),
			InlineData(Altitude.Top, 13, 5, 5, 0),
			InlineData(Altitude.Bottom, 14, 5, 5, 0),
			InlineData(Altitude.Neutral, 15, 5, 5, 0),
			InlineData(Altitude.Top, 16, 5, 5, 0),
			InlineData(Altitude.Neutral, 17, 5, 5, 0),
			InlineData(Altitude.Neutral, 18, 5, 5, 0),
			InlineData(Altitude.Bottom, 19, 5, 5, 0),

			InlineData(Altitude.Neutral, 0, 5, 5, 1),
			InlineData(Altitude.Neutral, 1, 5, 5, 1),
			InlineData(Altitude.Neutral, 2, 5, 5, 1),
			InlineData(Altitude.Top, 3, 5, 5, 1),
			InlineData(Altitude.Neutral, 4, 5, 5, 1),
			InlineData(Altitude.Bottom, 5, 5, 5, 1),
			InlineData(Altitude.Neutral, 6, 5, 5, 1),
			InlineData(Altitude.Top, 7, 5, 5, 1),
			InlineData(Altitude.Bottom, 8, 5, 5, 1),
			InlineData(Altitude.Neutral, 9, 5, 5, 1),
			InlineData(Altitude.Top, 10, 5, 5, 1),
			InlineData(Altitude.Neutral, 11, 5, 5, 1),
			InlineData(Altitude.Neutral, 12, 5, 5, 1),
			InlineData(Altitude.Bottom, 13, 5, 5, 1),
			InlineData(Altitude.Top, 14, 5, 5, 1),
			InlineData(Altitude.Neutral, 15, 5, 5, 1),
			InlineData(Altitude.Bottom, 16, 5, 5, 1),
			InlineData(Altitude.Neutral, 17, 5, 5, 1),
			InlineData(Altitude.Neutral, 18, 5, 5, 1),
			InlineData(Altitude.Top, 19, 5, 5, 1),

			InlineData(Altitude.Top, 0, 5, 5, 2),
			InlineData(Altitude.Neutral, 1, 5, 5, 2),
			InlineData(Altitude.Neutral, 2, 5, 5, 2),
			InlineData(Altitude.Bottom, 3, 5, 5, 2),
			InlineData(Altitude.Neutral, 4, 5, 5, 2),
			InlineData(Altitude.Top, 5, 5, 5, 2),
			InlineData(Altitude.Bottom, 6, 5, 5, 2),
			InlineData(Altitude.Neutral, 7, 5, 5, 2),
			InlineData(Altitude.Neutral, 8, 5, 5, 2),
			InlineData(Altitude.Top, 9, 5, 5, 2),
			InlineData(Altitude.Neutral, 10, 5, 5, 2),
			InlineData(Altitude.Bottom, 11, 5, 5, 2),
			InlineData(Altitude.Top, 12, 5, 5, 2),
			InlineData(Altitude.Neutral, 13, 5, 5, 2),
			InlineData(Altitude.Bottom, 14, 5, 5, 2),
			InlineData(Altitude.Neutral, 15, 5, 5, 2),
			InlineData(Altitude.Top, 16, 5, 5, 2),
			InlineData(Altitude.Neutral, 17, 5, 5, 2),
			InlineData(Altitude.Neutral, 18, 5, 5, 2),
			InlineData(Altitude.Bottom, 19, 5, 5, 2),

			InlineData(Altitude.Neutral, 0, 5, 5, 3),
			InlineData(Altitude.Top, 1, 5, 5, 3),
			InlineData(Altitude.Neutral, 2, 5, 5, 3),
			InlineData(Altitude.Bottom, 3, 5, 5, 3),
			InlineData(Altitude.Neutral, 4, 5, 5, 3),
			InlineData(Altitude.Top, 5, 5, 5, 3),
			InlineData(Altitude.Neutral, 6, 5, 5, 3),
			InlineData(Altitude.Bottom, 7, 5, 5, 3),
			InlineData(Altitude.Top, 8, 5, 5, 3),
			InlineData(Altitude.Neutral, 9, 5, 5, 3),
			InlineData(Altitude.Bottom, 10, 5, 5, 3),
			InlineData(Altitude.Top, 11, 5, 5, 3),
			InlineData(Altitude.Neutral, 12, 5, 5, 3),
			InlineData(Altitude.Neutral, 13, 5, 5, 3),
			InlineData(Altitude.Bottom, 14, 5, 5, 3),
			InlineData(Altitude.Neutral, 15, 5, 5, 3),
			InlineData(Altitude.Top, 16, 5, 5, 3),
			InlineData(Altitude.Neutral, 17, 5, 5, 3),
			InlineData(Altitude.Bottom, 18, 5, 5, 3),
			InlineData(Altitude.Top, 19, 5, 5, 3)
		]
		public void The_altitude_analyser_should_return_a_correct_altitude_list_on_analysing_by_variation(Altitude expected, int index, decimal minTopLength, decimal minBottomLength, int dataIndex)
		{
			var config = new AltitudeAnalyserConfig { Mode = AltitudeAnalyserMode.Variation, MinTop = minTopLength, MinBottom = minBottomLength };
			var actual = new AltitudeAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]))[index].Altitude;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The altitude analyser should return a correct altitude list on analysing by length"),
			InlineData(Altitude.Neutral, 0, 3, 3, 5),
			InlineData(Altitude.Neutral, 1, 3, 3, 5),
			InlineData(Altitude.Top, 2, 3, 3, 5),
			InlineData(Altitude.Neutral, 3, 3, 3, 5),
			InlineData(Altitude.Neutral, 4, 3, 3, 5),
			InlineData(Altitude.Bottom, 5, 3, 3, 5),
			InlineData(Altitude.Neutral, 6, 3, 3, 5),
			InlineData(Altitude.Top, 7, 3, 3, 5),
			InlineData(Altitude.Bottom, 8, 3, 3, 5),

			InlineData(Altitude.Top, 0, 3, 3, 0),
			InlineData(Altitude.Bottom, 1, 3, 3, 0),
			InlineData(Altitude.Top, 2, 3, 3, 0),
			InlineData(Altitude.Bottom, 3, 3, 3, 0),
			InlineData(Altitude.Neutral, 4, 3, 3, 0),
			InlineData(Altitude.Neutral, 5, 3, 3, 0),
			InlineData(Altitude.Top, 6, 3, 3, 0),
			InlineData(Altitude.Neutral, 7, 3, 3, 0),
			InlineData(Altitude.Bottom, 8, 3, 3, 0),
			InlineData(Altitude.Neutral, 9, 3, 3, 0),
			InlineData(Altitude.Neutral, 10, 3, 3, 0),
			InlineData(Altitude.Top, 11, 3, 3, 0),
			InlineData(Altitude.Bottom, 12, 3, 3, 0),
			InlineData(Altitude.Neutral, 13, 3, 3, 0),
			InlineData(Altitude.Neutral, 14, 3, 3, 0),
			InlineData(Altitude.Neutral, 15, 3, 3, 0),
			InlineData(Altitude.Top, 16, 3, 3, 0),
			InlineData(Altitude.Neutral, 17, 3, 3, 0),
			InlineData(Altitude.Neutral, 18, 3, 3, 0),
			InlineData(Altitude.Bottom, 19, 3, 3, 0),

			InlineData(Altitude.Neutral, 0, 3, 3, 1),
			InlineData(Altitude.Neutral, 1, 3, 3, 1),
			InlineData(Altitude.Neutral, 2, 3, 3, 1),
			InlineData(Altitude.Neutral, 3, 3, 3, 1),
			InlineData(Altitude.Neutral, 4, 3, 3, 1),
			InlineData(Altitude.Neutral, 5, 3, 3, 1),
			InlineData(Altitude.Neutral, 6, 3, 3, 1),
			InlineData(Altitude.Neutral, 7, 3, 3, 1),
			InlineData(Altitude.Neutral, 8, 3, 3, 1),
			InlineData(Altitude.Neutral, 9, 3, 3, 1),
			InlineData(Altitude.Top, 10, 3, 3, 1),
			InlineData(Altitude.Neutral, 11, 3, 3, 1),
			InlineData(Altitude.Neutral, 12, 3, 3, 1),
			InlineData(Altitude.Bottom, 13, 3, 3, 1),
			InlineData(Altitude.Top, 14, 3, 3, 1),
			InlineData(Altitude.Neutral, 15, 3, 3, 1),
			InlineData(Altitude.Bottom, 16, 3, 3, 1),
			InlineData(Altitude.Neutral, 17, 3, 3, 1),
			InlineData(Altitude.Neutral, 18, 3, 3, 1),
			InlineData(Altitude.Top, 19, 3, 3, 1),

			InlineData(Altitude.Top, 0, 3, 3, 2),
			InlineData(Altitude.Neutral, 1, 3, 3, 2),
			InlineData(Altitude.Neutral, 2, 3, 3, 2),
			InlineData(Altitude.Neutral, 3, 3, 3, 2),
			InlineData(Altitude.Neutral, 4, 3, 3, 2),
			InlineData(Altitude.Neutral, 5, 3, 3, 2),
			InlineData(Altitude.Bottom, 6, 3, 3, 2),
			InlineData(Altitude.Neutral, 7, 3, 3, 2),
			InlineData(Altitude.Neutral, 8, 3, 3, 2),
			InlineData(Altitude.Top, 9, 3, 3, 2),
			InlineData(Altitude.Neutral, 10, 3, 3, 2),
			InlineData(Altitude.Neutral, 11, 3, 3, 2),
			InlineData(Altitude.Neutral, 12, 3, 3, 2),
			InlineData(Altitude.Neutral, 13, 3, 3, 2),
			InlineData(Altitude.Bottom, 14, 3, 3, 2),
			InlineData(Altitude.Neutral, 15, 3, 3, 2),
			InlineData(Altitude.Top, 16, 3, 3, 2),
			InlineData(Altitude.Neutral, 17, 3, 3, 2),
			InlineData(Altitude.Neutral, 18, 3, 3, 2),
			InlineData(Altitude.Bottom, 19, 3, 3, 2),

			InlineData(Altitude.Neutral, 0, 3, 3, 3),
			InlineData(Altitude.Top, 1, 3, 3, 3),
			InlineData(Altitude.Neutral, 2, 3, 3, 3),
			InlineData(Altitude.Bottom, 3, 3, 3, 3),
			InlineData(Altitude.Neutral, 4, 3, 3, 3),
			InlineData(Altitude.Top, 5, 3, 3, 3),
			InlineData(Altitude.Neutral, 6, 3, 3, 3),
			InlineData(Altitude.Neutral, 7, 3, 3, 3),
			InlineData(Altitude.Neutral, 8, 3, 3, 3),
			InlineData(Altitude.Neutral, 9, 3, 3, 3),
			InlineData(Altitude.Bottom, 10, 3, 3, 3),
			InlineData(Altitude.Top, 11, 3, 3, 3),
			InlineData(Altitude.Neutral, 12, 3, 3, 3),
			InlineData(Altitude.Neutral, 13, 3, 3, 3),
			InlineData(Altitude.Bottom, 14, 3, 3, 3),
			InlineData(Altitude.Neutral, 15, 3, 3, 3),
			InlineData(Altitude.Neutral, 16, 3, 3, 3),
			InlineData(Altitude.Neutral, 17, 3, 3, 3),
			InlineData(Altitude.Neutral, 18, 3, 3, 3),
			InlineData(Altitude.Top, 19, 3, 3, 3)
		]
		public void The_altitude_analyser_should_return_a_correct_altitude_list_on_analysing_by_length(Altitude expected, int index, int minTopLength, int minBottomLength, int dataIndex)
		{
			var config = new AltitudeAnalyserConfig { Mode = AltitudeAnalyserMode.Length, MinTop = minTopLength, MinBottom = minBottomLength };
			var actual = new AltitudeAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]))[index].Altitude;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The altitude analyser should return a correct altitude list on analysing by variation with minTopVariation greater than minBottomVariation"),
			InlineData(Altitude.Neutral, 0, 7, 3, 0),
			InlineData(Altitude.Neutral, 1, 7, 3, 0),
			InlineData(Altitude.Neutral, 2, 7, 3, 0),
			InlineData(Altitude.Neutral, 3, 7, 3, 0),
			InlineData(Altitude.Neutral, 4, 7, 3, 0),
			InlineData(Altitude.Neutral, 5, 7, 3, 0),
			InlineData(Altitude.Top, 6, 7, 3, 0),
			InlineData(Altitude.Neutral, 7, 7, 3, 0),
			InlineData(Altitude.Bottom, 8, 7, 3, 0),
			InlineData(Altitude.Neutral, 9, 7, 3, 0),
			InlineData(Altitude.Neutral, 10, 7, 3, 0),
			InlineData(Altitude.Top, 11, 7, 3, 0),
			InlineData(Altitude.Bottom, 12, 7, 3, 0),
			InlineData(Altitude.Top, 13, 7, 3, 0),
			InlineData(Altitude.Bottom, 14, 7, 3, 0),
			InlineData(Altitude.Neutral, 15, 7, 3, 0),
			InlineData(Altitude.Top, 16, 7, 3, 0),
			InlineData(Altitude.Neutral, 17, 7, 3, 0),
			InlineData(Altitude.Neutral, 18, 7, 3, 0),
			InlineData(Altitude.Bottom, 19, 7, 3, 0),

			InlineData(Altitude.Neutral, 0, 7, 3, 1),
			InlineData(Altitude.Neutral, 1, 7, 3, 1),
			InlineData(Altitude.Neutral, 2, 7, 3, 1),
			InlineData(Altitude.Top, 3, 7, 3, 1),
			InlineData(Altitude.Neutral, 4, 7, 3, 1),
			InlineData(Altitude.Bottom, 5, 7, 3, 1),
			InlineData(Altitude.Neutral, 6, 7, 3, 1),
			InlineData(Altitude.Top, 7, 7, 3, 1),
			InlineData(Altitude.Bottom, 8, 7, 3, 1),
			InlineData(Altitude.Neutral, 9, 7, 3, 1),
			InlineData(Altitude.Top, 10, 7, 3, 1),
			InlineData(Altitude.Neutral, 11, 7, 3, 1),
			InlineData(Altitude.Neutral, 12, 7, 3, 1),
			InlineData(Altitude.Bottom, 13, 7, 3, 1),
			InlineData(Altitude.Top, 14, 7, 3, 1),
			InlineData(Altitude.Neutral, 15, 7, 3, 1),
			InlineData(Altitude.Bottom, 16, 7, 3, 1),
			InlineData(Altitude.Neutral, 17, 7, 3, 1),
			InlineData(Altitude.Neutral, 18, 7, 3, 1),
			InlineData(Altitude.Top, 19, 7, 3, 1),

			InlineData(Altitude.Top, 0, 7, 3, 2),
			InlineData(Altitude.Neutral, 1, 7, 3, 2),
			InlineData(Altitude.Neutral, 2, 7, 3, 2),
			InlineData(Altitude.Bottom, 3, 7, 3, 2),
			InlineData(Altitude.Neutral, 4, 7, 3, 2),
			InlineData(Altitude.Top, 5, 7, 3, 2),
			InlineData(Altitude.Bottom, 6, 7, 3, 2),
			InlineData(Altitude.Neutral, 7, 7, 3, 2),
			InlineData(Altitude.Neutral, 8, 7, 3, 2),
			InlineData(Altitude.Top, 9, 7, 3, 2),
			InlineData(Altitude.Neutral, 10, 7, 3, 2),
			InlineData(Altitude.Bottom, 11, 7, 3, 2),
			InlineData(Altitude.Top, 12, 7, 3, 2),
			InlineData(Altitude.Neutral, 13, 7, 3, 2),
			InlineData(Altitude.Bottom, 14, 7, 3, 2),
			InlineData(Altitude.Neutral, 15, 7, 3, 2),
			InlineData(Altitude.Top, 16, 7, 3, 2),
			InlineData(Altitude.Neutral, 17, 7, 3, 2),
			InlineData(Altitude.Neutral, 18, 7, 3, 2),
			InlineData(Altitude.Bottom, 19, 7, 3, 2),

			InlineData(Altitude.Neutral, 0, 7, 3, 3),
			InlineData(Altitude.Top, 1, 7, 3, 3),
			InlineData(Altitude.Neutral, 2, 7, 3, 3),
			InlineData(Altitude.Bottom, 3, 7, 3, 3),
			InlineData(Altitude.Neutral, 4, 7, 3, 3),
			InlineData(Altitude.Top, 5, 7, 3, 3),
			InlineData(Altitude.Neutral, 6, 7, 3, 3),
			InlineData(Altitude.Bottom, 7, 7, 3, 3),
			InlineData(Altitude.Top, 8, 7, 3, 3),
			InlineData(Altitude.Neutral, 9, 7, 3, 3),
			InlineData(Altitude.Bottom, 10, 7, 3, 3),
			InlineData(Altitude.Top, 11, 7, 3, 3),
			InlineData(Altitude.Neutral, 12, 7, 3, 3),
			InlineData(Altitude.Neutral, 13, 7, 3, 3),
			InlineData(Altitude.Bottom, 14, 7, 3, 3),
			InlineData(Altitude.Neutral, 15, 7, 3, 3),
			InlineData(Altitude.Top, 16, 7, 3, 3),
			InlineData(Altitude.Neutral, 17, 7, 3, 3),
			InlineData(Altitude.Bottom, 18, 7, 3, 3),
			InlineData(Altitude.Top, 19, 7, 3, 3)
		]
		public void The_altitude_analyser_should_return_a_correct_altitude_list_on_analysing_by_variation_top_greater_than_Bottom(Altitude expected, int index, decimal minTopLength, decimal minBottomLength, int dataIndex)
		{
			var config = new AltitudeAnalyserConfig { Mode = AltitudeAnalyserMode.Variation, MinTop = minTopLength, MinBottom = minBottomLength };
			var actual = new AltitudeAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]))[index].Altitude;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The altitude analyser should return a correct altitude list on analysing by length with minTopVariation greater than minBottomVariation"),
			InlineData(Altitude.Top, 0, 4, 2, 0),
			InlineData(Altitude.Bottom, 1, 4, 2, 0),
			InlineData(Altitude.Neutral, 2, 4, 2, 0),
			InlineData(Altitude.Neutral, 3, 4, 2, 0),
			InlineData(Altitude.Neutral, 4, 4, 2, 0),
			InlineData(Altitude.Neutral, 5, 4, 2, 0),
			InlineData(Altitude.Top, 6, 4, 2, 0),
			InlineData(Altitude.Neutral, 7, 4, 2, 0),
			InlineData(Altitude.Bottom, 8, 4, 2, 0),
			InlineData(Altitude.Neutral, 9, 4, 2, 0),
			InlineData(Altitude.Neutral, 10, 4, 2, 0),
			InlineData(Altitude.Top, 11, 4, 2, 0),
			InlineData(Altitude.Bottom, 12, 4, 2, 0),
			InlineData(Altitude.Neutral, 13, 4, 2, 0),
			InlineData(Altitude.Neutral, 14, 4, 2, 0),
			InlineData(Altitude.Neutral, 15, 4, 2, 0),
			InlineData(Altitude.Top, 16, 4, 2, 0),
			InlineData(Altitude.Neutral, 17, 4, 2, 0),
			InlineData(Altitude.Neutral, 18, 4, 2, 0),
			InlineData(Altitude.Bottom, 19, 4, 2, 0),

			InlineData(Altitude.Neutral, 0, 4, 2, 1),
			InlineData(Altitude.Neutral, 1, 4, 2, 1),
			InlineData(Altitude.Neutral, 2, 4, 2, 1),
			InlineData(Altitude.Neutral, 3, 4, 2, 1),
			InlineData(Altitude.Neutral, 4, 4, 2, 1),
			InlineData(Altitude.Neutral, 5, 4, 2, 1),
			InlineData(Altitude.Neutral, 6, 4, 2, 1),
			InlineData(Altitude.Neutral, 7, 4, 2, 1),
			InlineData(Altitude.Neutral, 8, 4, 2, 1),
			InlineData(Altitude.Neutral, 9, 4, 2, 1),
			InlineData(Altitude.Neutral, 10, 4, 2, 1),
			InlineData(Altitude.Neutral, 11, 4, 2, 1),
			InlineData(Altitude.Neutral, 12, 4, 2, 1),
			InlineData(Altitude.Neutral, 13, 4, 2, 1),
			InlineData(Altitude.Neutral, 14, 4, 2, 1),
			InlineData(Altitude.Neutral, 15, 4, 2, 1),
			InlineData(Altitude.Neutral, 16, 4, 2, 1),
			InlineData(Altitude.Neutral, 17, 4, 2, 1),
			InlineData(Altitude.Neutral, 18, 4, 2, 1),
			InlineData(Altitude.Top, 19, 4, 2, 1),

			InlineData(Altitude.Top, 0, 4, 2, 2),
			InlineData(Altitude.Neutral, 1, 4, 2, 2),
			InlineData(Altitude.Neutral, 2, 4, 2, 2),
			InlineData(Altitude.Bottom, 3, 4, 2, 2),
			InlineData(Altitude.Neutral, 4, 4, 2, 2),
			InlineData(Altitude.Top, 5, 4, 2, 2),
			InlineData(Altitude.Bottom, 6, 4, 2, 2),
			InlineData(Altitude.Neutral, 7, 4, 2, 2),
			InlineData(Altitude.Neutral, 8, 4, 2, 2),
			InlineData(Altitude.Top, 9, 4, 2, 2),
			InlineData(Altitude.Neutral, 10, 4, 2, 2),
			InlineData(Altitude.Bottom, 11, 4, 2, 2),
			InlineData(Altitude.Top, 12, 4, 2, 2),
			InlineData(Altitude.Neutral, 13, 4, 2, 2),
			InlineData(Altitude.Bottom, 14, 4, 2, 2),
			InlineData(Altitude.Neutral, 15, 4, 2, 2),
			InlineData(Altitude.Top, 16, 4, 2, 2),
			InlineData(Altitude.Neutral, 17, 4, 2, 2),
			InlineData(Altitude.Neutral, 18, 4, 2, 2),
			InlineData(Altitude.Bottom, 19, 4, 2, 2),

			InlineData(Altitude.Neutral, 0, 4, 2, 3),
			InlineData(Altitude.Neutral, 1, 4, 2, 3),
			InlineData(Altitude.Neutral, 2, 4, 2, 3),
			InlineData(Altitude.Neutral, 4, 2, 2, 3),
			InlineData(Altitude.Neutral, 4, 4, 2, 3),
			InlineData(Altitude.Top, 5, 4, 2, 3),
			InlineData(Altitude.Neutral, 6, 4, 2, 3),
			InlineData(Altitude.Bottom, 7, 4, 2, 3),
			InlineData(Altitude.Top, 8, 4, 2, 3),
			InlineData(Altitude.Neutral, 9, 4, 2, 3),
			InlineData(Altitude.Bottom, 10, 4, 2, 3),
			InlineData(Altitude.Top, 11, 4, 2, 3),
			InlineData(Altitude.Neutral, 12, 4, 2, 3),
			InlineData(Altitude.Neutral, 13, 4, 2, 3),
			InlineData(Altitude.Bottom, 14, 4, 2, 3),
			InlineData(Altitude.Neutral, 15, 4, 2, 3),
			InlineData(Altitude.Neutral, 16, 4, 2, 3),
			InlineData(Altitude.Neutral, 17, 4, 2, 3),
			InlineData(Altitude.Neutral, 18, 4, 2, 3),
			InlineData(Altitude.Top, 19, 4, 2, 3)
		]
		public void The_altitude_analyser_should_return_a_correct_altitude_list_on_analysing_by_length_top_greater_than_Bottom(Altitude expected, int index, decimal minTopLength, decimal minBottomLength, int dataIndex)
		{
			var config = new AltitudeAnalyserConfig { Mode = AltitudeAnalyserMode.Length, MinTop = minTopLength, MinBottom = minBottomLength };
			var actual = new AltitudeAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]))[index].Altitude;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The altitude analyser should return a correct altitude list on analysing by variation with minBottomVariation greater than minTopVariation"),
			InlineData(Altitude.Top, 0, 3, 7, 0),
			InlineData(Altitude.Bottom, 1, 3, 7, 0),
			InlineData(Altitude.Neutral, 2, 3, 7, 0),
			InlineData(Altitude.Neutral, 3, 3, 7, 0),
			InlineData(Altitude.Neutral, 4, 3, 7, 0),
			InlineData(Altitude.Neutral, 5, 3, 7, 0),
			InlineData(Altitude.Top, 6, 3, 7, 0),
			InlineData(Altitude.Neutral, 7, 3, 7, 0),
			InlineData(Altitude.Bottom, 8, 3, 7, 0),
			InlineData(Altitude.Neutral, 9, 3, 7, 0),
			InlineData(Altitude.Neutral, 10, 3, 7, 0),
			InlineData(Altitude.Top, 11, 3, 7, 0),
			InlineData(Altitude.Bottom, 12, 3, 7, 0),
			InlineData(Altitude.Top, 13, 3, 7, 0),
			InlineData(Altitude.Bottom, 14, 3, 7, 0),
			InlineData(Altitude.Neutral, 15, 3, 7, 0),
			InlineData(Altitude.Top, 16, 3, 7, 0),
			InlineData(Altitude.Neutral, 17, 3, 7, 0),
			InlineData(Altitude.Neutral, 18, 3, 7, 0),
			InlineData(Altitude.Bottom, 19, 3, 7, 0),

			InlineData(Altitude.Neutral, 0, 3, 7, 1),
			InlineData(Altitude.Neutral, 1, 3, 7, 1),
			InlineData(Altitude.Neutral, 2, 3, 7, 1),
			InlineData(Altitude.Top, 3, 3, 7, 1),
			InlineData(Altitude.Neutral, 4, 3, 7, 1),
			InlineData(Altitude.Bottom, 5, 3, 7, 1),
			InlineData(Altitude.Neutral, 6, 3, 7, 1),
			InlineData(Altitude.Top, 7, 3, 7, 1),
			InlineData(Altitude.Bottom, 8, 3, 7, 1),
			InlineData(Altitude.Neutral, 9, 3, 7, 1),
			InlineData(Altitude.Top, 10, 3, 7, 1),
			InlineData(Altitude.Neutral, 11, 3, 7, 1),
			InlineData(Altitude.Neutral, 12, 3, 7, 1),
			InlineData(Altitude.Bottom, 13, 3, 7, 1),
			InlineData(Altitude.Top, 14, 3, 7, 1),
			InlineData(Altitude.Neutral, 15, 3, 7, 1),
			InlineData(Altitude.Bottom, 16, 3, 7, 1),
			InlineData(Altitude.Neutral, 17, 3, 7, 1),
			InlineData(Altitude.Neutral, 18, 3, 7, 1),
			InlineData(Altitude.Top, 19, 3, 7, 1),

			InlineData(Altitude.Top, 0, 3, 7, 2),
			InlineData(Altitude.Neutral, 1, 3, 7, 2),
			InlineData(Altitude.Neutral, 2, 3, 7, 2),
			InlineData(Altitude.Bottom, 3, 3, 7, 2),
			InlineData(Altitude.Neutral, 4, 3, 7, 2),
			InlineData(Altitude.Top, 5, 3, 7, 2),
			InlineData(Altitude.Bottom, 6, 3, 7, 2),
			InlineData(Altitude.Neutral, 7, 3, 7, 2),
			InlineData(Altitude.Neutral, 8, 3, 7, 2),
			InlineData(Altitude.Top, 9, 3, 7, 2),
			InlineData(Altitude.Neutral, 10, 3, 7, 2),
			InlineData(Altitude.Bottom, 11, 3, 7, 2),
			InlineData(Altitude.Top, 12, 3, 7, 2),
			InlineData(Altitude.Neutral, 13, 3, 7, 2),
			InlineData(Altitude.Bottom, 14, 3, 7, 2),
			InlineData(Altitude.Neutral, 15, 3, 7, 2),
			InlineData(Altitude.Top, 16, 3, 7, 2),
			InlineData(Altitude.Neutral, 17, 3, 7, 2),
			InlineData(Altitude.Neutral, 18, 3, 7, 2),
			InlineData(Altitude.Bottom, 19, 3, 7, 2),

			InlineData(Altitude.Neutral, 0, 3, 7, 3),
			InlineData(Altitude.Top, 1, 3, 7, 3),
			InlineData(Altitude.Neutral, 2, 3, 7, 3),
			InlineData(Altitude.Bottom, 3, 3, 7, 3),
			InlineData(Altitude.Neutral, 4, 3, 7, 3),
			InlineData(Altitude.Top, 5, 3, 7, 3),
			InlineData(Altitude.Neutral, 6, 3, 7, 3),
			InlineData(Altitude.Bottom, 7, 3, 7, 3),
			InlineData(Altitude.Top, 8, 3, 7, 3),
			InlineData(Altitude.Neutral, 9, 3, 7, 3),
			InlineData(Altitude.Bottom, 10, 3, 7, 3),
			InlineData(Altitude.Top, 11, 3, 7, 3),
			InlineData(Altitude.Neutral, 12, 3, 7, 3),
			InlineData(Altitude.Neutral, 13, 3, 7, 3),
			InlineData(Altitude.Bottom, 14, 3, 7, 3),
			InlineData(Altitude.Neutral, 15, 3, 7, 3),
			InlineData(Altitude.Top, 16, 3, 7, 3),
			InlineData(Altitude.Neutral, 17, 3, 7, 3),
			InlineData(Altitude.Bottom, 18, 3, 7, 3),
			InlineData(Altitude.Top, 19, 3, 7, 3)
		]
		public void The_altitude_analyser_should_return_a_correct_altitude_list_on_analysing_by_variation_Bottom_greater_than_Top(Altitude expected, int index, decimal minTopLength, decimal minBottomLength, int dataIndex)
		{
			var config = new AltitudeAnalyserConfig { Mode = AltitudeAnalyserMode.Variation, MinTop = minTopLength, MinBottom = minBottomLength };
			var actual = new AltitudeAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]))[index].Altitude;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The altitude analyser should return a correct altitude list on analysing by length with minBottom greater than minTop"),
			InlineData(Altitude.Top, 0, 2, 4, 0),
			InlineData(Altitude.Bottom, 1, 2, 4, 0),
			InlineData(Altitude.Top, 2, 2, 4, 0),
			InlineData(Altitude.Bottom, 3, 2, 4, 0),
			InlineData(Altitude.Neutral, 4, 2, 4, 0),
			InlineData(Altitude.Neutral, 5, 2, 4, 0),
			InlineData(Altitude.Top, 6, 2, 4, 0),
			InlineData(Altitude.Neutral, 7, 2, 4, 0),
			InlineData(Altitude.Neutral, 8, 2, 4, 0),
			InlineData(Altitude.Neutral, 9, 2, 4, 0),
			InlineData(Altitude.Neutral, 10, 2, 4, 0),
			InlineData(Altitude.Neutral, 11, 2, 4, 0),
			InlineData(Altitude.Bottom, 12, 2, 4, 0),
			InlineData(Altitude.Top, 13, 2, 4, 0),
			InlineData(Altitude.Bottom, 14, 2, 4, 0),
			InlineData(Altitude.Neutral, 15, 2, 4, 0),
			InlineData(Altitude.Top, 16, 2, 4, 0),
			InlineData(Altitude.Neutral, 17, 2, 4, 0),
			InlineData(Altitude.Neutral, 18, 2, 4, 0),
			InlineData(Altitude.Bottom, 19, 2, 4, 0),

			InlineData(Altitude.Neutral, 0, 2, 4, 1),
			InlineData(Altitude.Neutral, 1, 2, 4, 1),
			InlineData(Altitude.Neutral, 2, 2, 4, 1),
			InlineData(Altitude.Top, 3, 2, 4, 1),
			InlineData(Altitude.Neutral, 4, 2, 4, 1),
			InlineData(Altitude.Bottom, 5, 2, 4, 1),
			InlineData(Altitude.Neutral, 6, 2, 4, 1),
			InlineData(Altitude.Top, 7, 2, 4, 1),
			InlineData(Altitude.Bottom, 8, 2, 4, 1),
			InlineData(Altitude.Neutral, 9, 2, 4, 1),
			InlineData(Altitude.Top, 10, 2, 4, 1),
			InlineData(Altitude.Neutral, 11, 2, 4, 1),
			InlineData(Altitude.Neutral, 12, 2, 4, 1),
			InlineData(Altitude.Bottom, 13, 2, 4, 1),
			InlineData(Altitude.Top, 14, 2, 4, 1),
			InlineData(Altitude.Neutral, 15, 2, 4, 1),
			InlineData(Altitude.Bottom, 16, 2, 4, 1),
			InlineData(Altitude.Neutral, 17, 2, 4, 1),
			InlineData(Altitude.Neutral, 18, 2, 4, 1),
			InlineData(Altitude.Top, 19, 2, 4, 1),

			InlineData(Altitude.Top, 0, 2, 4, 2),
			InlineData(Altitude.Neutral, 1, 2, 4, 2),
			InlineData(Altitude.Neutral, 2, 2, 4, 2),
			InlineData(Altitude.Neutral, 3, 2, 4, 2),
			InlineData(Altitude.Neutral, 4, 2, 4, 2),
			InlineData(Altitude.Neutral, 5, 2, 4, 2),
			InlineData(Altitude.Bottom, 6, 2, 4, 2),
			InlineData(Altitude.Neutral, 7, 2, 4, 2),
			InlineData(Altitude.Neutral, 8, 2, 4, 2),
			InlineData(Altitude.Top, 9, 2, 4, 2),
			InlineData(Altitude.Neutral, 10, 2, 4, 2),
			InlineData(Altitude.Neutral, 11, 2, 4, 2),
			InlineData(Altitude.Neutral, 12, 2, 4, 2),
			InlineData(Altitude.Neutral, 13, 2, 4, 2),
			InlineData(Altitude.Bottom, 14, 2, 4, 2),
			InlineData(Altitude.Neutral, 15, 2, 4, 2),
			InlineData(Altitude.Top, 16, 2, 4, 2),
			InlineData(Altitude.Neutral, 17, 2, 4, 2),
			InlineData(Altitude.Neutral, 18, 2, 4, 2),
			InlineData(Altitude.Bottom, 19, 2, 4, 2),

			InlineData(Altitude.Neutral, 0, 2, 4, 3),
			InlineData(Altitude.Top, 1, 2, 4, 3),
			InlineData(Altitude.Neutral, 2, 2, 4, 3),
			InlineData(Altitude.Neutral, 3, 2, 4, 3),
			InlineData(Altitude.Neutral, 4, 2, 4, 3),
			InlineData(Altitude.Neutral, 5, 2, 4, 3),
			InlineData(Altitude.Neutral, 6, 2, 4, 3),
			InlineData(Altitude.Neutral, 7, 2, 4, 3),
			InlineData(Altitude.Neutral, 8, 2, 4, 3),
			InlineData(Altitude.Neutral, 9, 2, 4, 3),
			InlineData(Altitude.Neutral, 10, 2, 4, 3),
			InlineData(Altitude.Neutral, 11, 2, 4, 3),
			InlineData(Altitude.Neutral, 12, 2, 4, 3),
			InlineData(Altitude.Neutral, 13, 2, 4, 3),
			InlineData(Altitude.Bottom, 14, 2, 4, 3),
			InlineData(Altitude.Neutral, 15, 2, 4, 3),
			InlineData(Altitude.Top, 16, 2, 4, 3),
			InlineData(Altitude.Neutral, 17, 2, 4, 3),
			InlineData(Altitude.Bottom, 18, 2, 4, 3),
			InlineData(Altitude.Top, 19, 2, 4, 3)
		]
		public void The_altitude_analyser_should_return_a_correct_altitude_list_on_analysing_by_length_bottom_greater_than_top(Altitude expected, int index, decimal minTopLength, decimal minBottomLength, int dataIndex)
		{
			var config = new AltitudeAnalyserConfig { Mode = AltitudeAnalyserMode.Length, MinTop = minTopLength, MinBottom = minBottomLength };
			var actual = new AltitudeAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]))[index].Altitude;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The relative index used on analysing by length should be correct"),
			InlineData(1, 6, Altitude.Top, 0, 3),
			InlineData(1, 7, Altitude.Top, 0, 3),
			InlineData(0, 8, Altitude.Top, 0, 3),
			InlineData(2, 9, Altitude.Bottom, 0, 3),
			InlineData(0, 10, Altitude.Bottom, 0, 3),
			InlineData(1, 5, Altitude.Top, 0, 3),
			InlineData(2, 5, Altitude.Top, 1, 3),
			InlineData(2, 5, Altitude.Top, 2, 3),
			InlineData(5, 5, Altitude.Bottom, 3, 3),
			InlineData(5, 5, Altitude.Bottom, 5, 3)
		]
		public void The_relative_index_used_on_analysing_by_length_should_be_correct(int expected, int dataIndex, Altitude altitude, int index, int length)
		{
			var actual = new AltitudeAnalyser<FoolICanBeClassifiedByAltitude>().RelativeIndexFrom(FoolICanBeClassifiedByAltitude.From(values[dataIndex]), altitude, index, length);
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The altitude analyser should return the correct indexes given altitudes"),
			InlineData(new int[] { }, new Altitude[] { Altitude.Neutral }, Altitude.Top),
			InlineData(new int[] { 0 }, new Altitude[] { Altitude.Neutral }, Altitude.Neutral),
			InlineData(new int[] { 1 }, new Altitude[] { Altitude.Neutral, Altitude.Top }, Altitude.Top),
			InlineData(new int[] { 0 }, new Altitude[] { Altitude.Neutral, Altitude.Top }, Altitude.Neutral),
			InlineData(new int[] { 2, 4, 6 }, new Altitude[] { Altitude.Bottom, Altitude.Top, Altitude.Neutral, Altitude.Top, Altitude.Neutral, Altitude.Bottom, Altitude.Neutral }, Altitude.Neutral),
			InlineData(new int[] { 1, 3 }, new Altitude[] { Altitude.Bottom, Altitude.Top, Altitude.Neutral, Altitude.Top, Altitude.Neutral, Altitude.Bottom, Altitude.Neutral }, Altitude.Top),
			InlineData(new int[] { 0, 5 }, new Altitude[] { Altitude.Bottom, Altitude.Top, Altitude.Neutral, Altitude.Top, Altitude.Neutral, Altitude.Bottom, Altitude.Neutral }, Altitude.Bottom)
		]
		public void The_altitude_analyser_should_return_the_correct_indexes_given_altitudes(int[] expected, Altitude[] values, Altitude altitude)
		{
			var actual = new AltitudeAnalyser<FoolICanBeClassifiedByAltitude>().IndexesFrom(values, altitude);
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The cast in altitude analyser configured should be correct"),
			InlineData(3, 3.4, 3.4)
		]
		public void The_cast_in_altitude_analyser_configured_should_be_correct(int expected, decimal minTopLength, decimal minBottomLength)
		{
			var config = new AltitudeAnalyserConfig { Mode = AltitudeAnalyserMode.Length, MinTop = minTopLength, MinBottom = minBottomLength };
			var actual =  ((AltitudeAnalyser<FoolICanBeClassifiedByAltitude>) new AltitudeAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config)).MinBottomIdentifier;
			Assert.Equal(expected, actual);
		}
	}
}
