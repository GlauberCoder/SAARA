using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
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
	public class AltitudeAnalyserTest
	{
		IList<IList<decimal>> values
		{
			get
			{
				var data = new List<IList<decimal>>();
				data.Add(new List<decimal> { 100m, 101m, 103m, 105m, 90m, 92m, 89m, 90m, 100m });
				data.Add(new List<decimal> { 100m, 94m, 98m, 94m, 94m, 96m, 102m, 98m, 94m, 100m, 96m, 104m, 92m, 104m, 94m, 100m, 110m, 104m, 100m, 98m });
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
			Theory(DisplayName = "The altitude analyser should return a correct altitude list on analysing by vatiation"),
			InlineData(Altitude.Neutral, 0, 0.10, 0.10, 0),
			InlineData(Altitude.Neutral, 1, 0.10, 0.10, 0),
			InlineData(Altitude.Neutral, 2, 0.10, 0.10, 0),
			InlineData(Altitude.Top, 3, 0.10, 0.10, 0),
			InlineData(Altitude.Neutral, 4, 0.10, 0.10, 0),
			InlineData(Altitude.Neutral, 5, 0.10, 0.10, 0),
			InlineData(Altitude.Bottom, 6, 0.10, 0.10, 0),
			InlineData(Altitude.Neutral, 7, 0.10, 0.10, 0),
			InlineData(Altitude.Neutral, 8, 0.10, 0.10, 0),

			InlineData(Altitude.Top, 0, 0.05, 0.05, 1),
			InlineData(Altitude.Bottom, 1, 0.05, 0.05, 1),
			InlineData(Altitude.Neutral, 2, 0.05, 0.05, 1),
			InlineData(Altitude.Neutral, 3, 0.05, 0.05, 1),
			InlineData(Altitude.Neutral, 4, 0.05, 0.05, 1),
			InlineData(Altitude.Neutral, 5, 0.05, 0.05, 1),
			InlineData(Altitude.Top, 6, 0.05, 0.05, 1),
			InlineData(Altitude.Neutral, 7, 0.05, 0.05, 1),
			InlineData(Altitude.Bottom, 8, 0.05, 0.05, 1),
			InlineData(Altitude.Neutral, 9, 0.05, 0.05, 1),
			InlineData(Altitude.Neutral, 10, 0.05, 0.05, 1),
			InlineData(Altitude.Top, 11, 0.05, 0.05, 1),
			InlineData(Altitude.Bottom, 12, 0.05, 0.05, 1),
			InlineData(Altitude.Top, 13, 0.05, 0.05, 1),
			InlineData(Altitude.Bottom, 14, 0.05, 0.05, 1),
			InlineData(Altitude.Neutral, 15, 0.05, 0.05, 1),
			InlineData(Altitude.Top, 16, 0.05, 0.05, 1),
			InlineData(Altitude.Neutral, 17, 0.05, 0.05, 1),
			InlineData(Altitude.Neutral, 18, 0.05, 0.05, 1),
			InlineData(Altitude.Neutral, 19, 0.05, 0.05, 1)
		]
		public void The_altitude_analyser_should_return_a_correct_altitude_list_on_analysing_by_variation(Altitude expected, int index, decimal minTopLength, decimal minBottomLength, int dataIndex)
		{
			var config = new AltitudeAnalyserConfig { Mode = AltitudeAnalyserMode.Variation, MinTop = minTopLength, MinBottom = minBottomLength };
			var actual = new AltitudeAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]))[index].Altitude;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The altitude from reference and variation should be"),
			InlineData(Altitude.Neutral, 91, 100, 0.10, 0.10),
			InlineData(Altitude.Bottom, 90, 100, 0.10, 0.10),
			InlineData(Altitude.Bottom, 89, 100, 0.10, 0.10),
			InlineData(Altitude.Neutral, 109, 100, 0.10, 0.10),
			InlineData(Altitude.Top, 110, 100, 0.10, 0.10),
			InlineData(Altitude.Top, 111, 100, 0.10, 0.10)
		]
		public void The_altitude_from_reference_and_variation_should_be(Altitude expected, decimal value, decimal reference, decimal topMinVariation, decimal bottomMinVariation)
		{
			var actual = new AltitudeAnalyser<FoolICanBeClassifiedByAltitude>().AltitudeFrom(value, reference, topMinVariation, bottomMinVariation);
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The altitude analyser should return a correct altitude list on analysing by length"),
			InlineData(Altitude.Neutral, 0, 3, 3, 2),
			InlineData(Altitude.Neutral, 1, 3, 3, 2),
			InlineData(Altitude.Top, 2, 3, 3, 2),
			InlineData(Altitude.Neutral, 3, 3, 3, 2),
			InlineData(Altitude.Neutral, 4, 3, 3, 2),
			InlineData(Altitude.Bottom, 5, 3, 3, 2),
			InlineData(Altitude.Neutral, 6, 3, 3, 2),
			InlineData(Altitude.Neutral, 7, 3, 3, 2),
			InlineData(Altitude.Neutral, 8, 3, 3, 2),

			InlineData(Altitude.Top, 0, 3, 3, 1),
			InlineData(Altitude.Bottom, 1, 3, 3, 1),
			InlineData(Altitude.Top, 2, 3, 3, 1),
			InlineData(Altitude.Bottom, 3, 3, 3, 1),
			InlineData(Altitude.Neutral, 4, 3, 3, 1),
			InlineData(Altitude.Neutral, 5, 3, 3, 1),
			InlineData(Altitude.Top, 6, 3, 3, 1),
			InlineData(Altitude.Neutral, 7, 3, 3, 1),
			InlineData(Altitude.Bottom, 8, 3, 3, 1),
			InlineData(Altitude.Neutral, 9, 3, 3, 1),
			InlineData(Altitude.Neutral, 10, 3, 3, 1),
			InlineData(Altitude.Top, 11, 3, 3, 1),
			InlineData(Altitude.Bottom, 12, 3, 3, 1),
			InlineData(Altitude.Neutral, 13, 3, 3, 1),
			InlineData(Altitude.Neutral, 14, 3, 3, 1),
			InlineData(Altitude.Neutral, 15, 3, 3, 1),
			InlineData(Altitude.Top, 16, 3, 3, 1),
			InlineData(Altitude.Neutral, 17, 3, 3, 1),
			InlineData(Altitude.Neutral, 18, 3, 3, 1),
			InlineData(Altitude.Neutral, 19, 3, 3, 1)
		]
		public void The_altitude_analyser_should_return_a_correct_altitude_list_on_analysing_by_length(Altitude expected, int index, int minTopLength, int minBottomLength, int dataIndex)
		{
			var config = new AltitudeAnalyserConfig { Mode = AltitudeAnalyserMode.Length, MinTop = minTopLength, MinBottom = minBottomLength };
			var actual = new AltitudeAnalyser<FoolICanBeClassifiedByAltitude>().Configure(config).Identify(FoolICanBeClassifiedByAltitude.From(values[dataIndex]))[index].Altitude;
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The relative index used on analysing by length should be correct"),
			InlineData(1, 3, Altitude.Top, 0, 3),
			InlineData(1, 4, Altitude.Top, 0, 3),
			InlineData(0, 5, Altitude.Top, 0, 3),
			InlineData(2, 6, Altitude.Bottom, 0, 3),
			InlineData(0, 7, Altitude.Bottom, 0, 3),
			InlineData(1, 2, Altitude.Top, 0, 3),
			InlineData(2, 2, Altitude.Top, 1, 3),
			InlineData(2, 2, Altitude.Top, 2, 3),
			InlineData(5, 2, Altitude.Bottom, 3, 3),
			InlineData(5, 2, Altitude.Bottom, 5, 3)
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
