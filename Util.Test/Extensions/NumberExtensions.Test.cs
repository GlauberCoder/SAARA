using System;
using System.Collections.Generic;
using System.Text;
using Util.Extensions;
using Xunit;

namespace Util.Test.Extensions
{
	public class NumberExtensionsTest
	{
		[
			Theory(DisplayName = "The Percentage Of Change Of Decimal In Another Should Be"),
			InlineData(100, 10, 20),
			InlineData(100, 20, 40),
			InlineData(100, 5, 10),
			InlineData(-50, 20, 10),
			InlineData(-50, 40, 20),
			InlineData(-50, 10, 5)

		]
		public void The_Percentage_Of_Change_Of_A_Decimal_In_Another_Should_Be(decimal expected, decimal value, decimal total)
		{
			var actual = value.PercentageOfChange(total);
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The Absolute Percentage Of Change Of Decimal In Another Should Be"),
			InlineData(100, 10, 20),
			InlineData(100, 20, 40),
			InlineData(100, 5, 10),
			InlineData(50, 20, 10),
			InlineData(50, 40, 20),
			InlineData(50, 10, 5)

		]
		public void The_Absolute_Percentage_Of_Change_Of_A_Decimal_In_Another_Should_Be(decimal expected, decimal value, decimal total)
		{
			var actual = value.AbsolutePercentageOfChange(total);
			Assert.Equal(expected, actual);
		}
	}
}
