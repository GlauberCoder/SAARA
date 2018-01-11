using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Extensions
{
	public static class DoubleExtensions
	{
		public static DateTime TimestampToDateTime(this double value)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(value);
		}
	}
}
