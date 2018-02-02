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


	}
}
