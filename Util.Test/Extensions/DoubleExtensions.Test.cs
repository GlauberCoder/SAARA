using System;
using System.Linq;
using Util.Extensions;
using Xunit;

namespace Infra.Test
{
	public class BitfinexDataReaderTest
	{
		
		[
			Theory(DisplayName = "The Proporção Of A Double In Another Should Be"),
			InlineData(0.1, 20, 200),
			InlineData(0.2, 40, 200),
			InlineData(0.05, 10, 200)
		]
		public void The_Proportion_Of_A_Double_In_Another_Should_Be(double expected, double value, double total)
		{
			var actual = value.ProportionOn(total);
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The Percentage Of A Double In Another Should Be"),
			InlineData(10, 20, 200),
			InlineData(20, 40, 200),
			InlineData(5, 10, 200)
		]
		public void The_Percentage_Of_A_Double_In_Another_Should_Be(double expected, double value, double total)
		{
			var actual = value.PercentageOn(total);
			Assert.Equal(expected, actual);
		}

	}
}
