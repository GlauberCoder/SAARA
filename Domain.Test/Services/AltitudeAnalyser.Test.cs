using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Extensions;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Domain.Test.Services
{
	public class AltitudeAnalyserTest
	{
		[
			Theory(DisplayName = "The altitude analyser should return a correct altitude list on analysing by vatiation"),
			InlineData(Altitude.Neutral, 0,  0.10,  0.10, new double[] { 100, 101, 103, 105, 90, 92, 89, 90, 100 }),
			InlineData(Altitude.Neutral, 1,  0.10,  0.10, new double[] { 100, 101, 103, 105, 90, 92, 89, 90, 100 }),
			InlineData(Altitude.Neutral, 2,  0.10,  0.10, new double[] { 100, 101, 103, 105, 90, 92, 89, 90, 100 }),
			InlineData(Altitude.Top, 3,  0.10,  0.10, new double[] { 100, 101, 103, 105, 90, 92, 89, 90, 100 }),
			InlineData(Altitude.Neutral, 4,  0.10,  0.10, new double[] { 100, 101, 103, 105, 90, 92, 89, 90, 100 }),
			InlineData(Altitude.Neutral, 5,  0.10,  0.10, new double[] { 100, 101, 103, 105, 90, 92, 89, 90, 100 }),
			InlineData(Altitude.Neutral, 6,  0.10,  0.10, new double[] { 100, 101, 103, 105, 90, 92, 89, 90, 100 }),
			InlineData(Altitude.Bottom, 7,  0.10,  0.10, new double[] { 100, 101, 103, 105, 90, 92, 89, 90, 100 }),
			InlineData(Altitude.Neutral, 8,  0.10,  0.10, new double[] { 100, 101, 103, 105, 90, 92, 89, 90, 100 })
		]
		public void The_altitude_analyser_should_return_a_correct_altitude_list_on_analysing_by_variation(Altitude expected, int index, int minTopLength, int minBottomLength, double[] values)
		{
			var actual = new AltitudeAnalyser()
															.ByVariation(minTopLength, minBottomLength)
															.Identify(values.CastAs<decimal>())[index];
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
			var actual = new AltitudeAnalyser().AltitudeFrom(value, reference, topMinVariation, bottomMinVariation);
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The altitude analyser should return a correct altitude list on analysing by length"),
			InlineData(Altitude.Neutral, 0, 3, 3, new double[] { 1, 1.2, 2, 1.3, 1.4, 1.2, 1.5, 1.6, 1.5 }),
			InlineData(Altitude.Neutral, 1, 3, 3, new double[] { 1, 1.2, 2, 1.3, 1.4, 1.2, 1.5, 1.6, 1.5 }),
			InlineData(Altitude.Top, 2, 3, 3, new double[] { 1, 1.2, 2, 1.3, 1.4, 1.2, 1.5, 1.6, 1.5 }),
			InlineData(Altitude.Neutral, 3, 3, 3, new double[] { 1, 1.2, 2, 1.3, 1.4, 1.2, 1.5, 1.6, 1.5 }),
			InlineData(Altitude.Neutral, 4, 3, 3, new double[] { 1, 1.2, 2, 1.3, 1.4, 1.2, 1.5, 1.6, 1.5 }),
			InlineData(Altitude.Bottom, 5, 3, 3, new double[] { 1, 1.2, 2, 1.3, 1.4, 1.2, 1.5, 1.6, 1.5 }),
			InlineData(Altitude.Neutral, 6, 3, 3, new double[] { 1, 1.2, 2, 1.3, 1.4, 1.2, 1.5, 1.6, 1.5 }),
			InlineData(Altitude.Neutral, 7, 3, 3, new double[] { 1, 1.2, 2, 1.3, 1.4, 1.2, 1.5, 1.6, 1.5 }),
			InlineData(Altitude.Neutral, 8, 3, 3, new double[] { 1, 1.2, 2, 1.3, 1.4, 1.2, 1.5, 1.6, 1.5 })
		]
		public void The_altitude_analyser_should_return_a_correct_altitude_list_on_analysing_by_length(Altitude expected, int index, int minTopLength, int minBottomLength, double[] values)
		{
			var actual = new AltitudeAnalyser()
															.ByLength(minTopLength, minBottomLength)
															.Identify(values.CastAs<decimal>())[index];
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The relative index used on analysing by length should be correct"),
			InlineData(1, new double[] { 1, 1.2, 2, 1.3 }, Altitude.Top),
			InlineData(1, new double[] { 1.2, 2, 1.3, 1.2 }, Altitude.Top),
			InlineData(0, new double[] { 2, 1.3, 1.4, 1.2 }, Altitude.Top),
			InlineData(2, new double[] { 1.3, 1.4, 1.2, 1.5 }, Altitude.Bottom),
			InlineData(0, new double[] { 1.2, 1.5, 1.6, 1.5 }, Altitude.Bottom)
		]
		public void The_relative_index_used_on_analysing_by_length_should_be_correct(int expected, double[] values, Altitude altitude)
		{
			var actual = new AltitudeAnalyser().RelativeIndexFrom(values.CastAs<decimal>(), altitude);
			Assert.Equal(expected, actual);
		}
		
		[
			Theory(DisplayName = "The altitude congruence should be"),
			InlineData
			(
				new Altitude[] { Altitude.Bottom, Altitude.Top, Altitude.Neutral },
				new Altitude[] { Altitude.Bottom, Altitude.Top, Altitude.Neutral },
				new Altitude[] { Altitude.Bottom, Altitude.Top, Altitude.Neutral }
			),
			InlineData
			(
				new Altitude[] { Altitude.Neutral, Altitude.Neutral, Altitude.Top, Altitude.Neutral, Altitude.Neutral },
				new Altitude[] { Altitude.Bottom, Altitude.Neutral, Altitude.Top, Altitude.Top, Altitude.Neutral },
				new Altitude[] { Altitude.Top, Altitude.Bottom, Altitude.Top }
			)
		]
		public void The_altitude_congruence_should_be(Altitude[] expected, Altitude[] values, Altitude[] otherValues)
		{
			var actual = new AltitudeAnalyser().Congruence(values, otherValues);
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The prepended list should be"),
			InlineData(new Altitude[] { Altitude.Neutral }, new Altitude[] { }, Altitude.Neutral, 1),
			InlineData(new Altitude[] { Altitude.Neutral, Altitude.Neutral }, new Altitude[] { }, Altitude.Neutral, 2),
			InlineData(new Altitude[] { Altitude.Neutral, Altitude.Top }, new Altitude[] { Altitude.Top }, Altitude.Neutral, 1),
			InlineData(new Altitude[] { Altitude.Top }, new Altitude[] { }, Altitude.Top, 1)
		]
		public void The_prepended_list_should_be(Altitude[] expected, Altitude[] values, Altitude altitude, int count)
		{
			var actual = new AltitudeAnalyser().PrependAltitudeValues(values, altitude, count);
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The indexes should be"),
			InlineData(new int[] { }, new Altitude[] { Altitude.Neutral }, Altitude.Top),
			InlineData(new int[] { 0 }, new Altitude[] { Altitude.Neutral }, Altitude.Neutral),
			InlineData(new int[] { 1 }, new Altitude[] { Altitude.Neutral, Altitude.Top }, Altitude.Top),
			InlineData(new int[] { 0 }, new Altitude[] { Altitude.Neutral, Altitude.Top }, Altitude.Neutral),
			InlineData(new int[] { 2, 4, 6 }, new Altitude[] { Altitude.Bottom, Altitude.Top, Altitude.Neutral, Altitude.Top, Altitude.Neutral, Altitude.Bottom, Altitude.Neutral }, Altitude.Neutral),
			InlineData(new int[] { 1, 3 }, new Altitude[] { Altitude.Bottom, Altitude.Top, Altitude.Neutral, Altitude.Top, Altitude.Neutral, Altitude.Bottom, Altitude.Neutral }, Altitude.Top),
			InlineData(new int[] { 0, 5 }, new Altitude[] { Altitude.Bottom, Altitude.Top, Altitude.Neutral, Altitude.Top, Altitude.Neutral, Altitude.Bottom, Altitude.Neutral }, Altitude.Bottom)
		]
		public void The_indexes_should_be(int[] expected, Altitude[] values, Altitude altitude)
		{
			var actual = new AltitudeAnalyser().IndexesFrom(values, altitude);
			Assert.Equal(expected, actual);
		}
	}
}
