using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Services
{
	public interface ITrendAnalyser
	{
		/// <summary>
		/// Sets the analyser to identify trend by first versus last tops (or bottoms)
		/// </summary>
		/// <param name="altitude">Altitude used, it can be top or bottom</param>
		/// <returns>This analyser</returns>
		ITrendAnalyser ByFirstVsLast(Altitude altitude);

		/// <summary>
		/// Sets the analyser to identify trend by most recents tops (or bottoms)
		/// <summary>
		/// <param name="altitude">Altitude used, it can be top or bottom</param>
		/// <returns>This analyser</returns>
		ITrendAnalyser ByMostRecents(Altitude altitude);

		/// <summary>
		/// Sets the analyser to identify trend by higher high - if top - or lower low - if bottom
		/// </summary>
		/// <param name="altitude">Altitude used, it can be top or bottom</param>
		/// <returns>This analyser</returns>
		ITrendAnalyser ByHigherHighLowerLow(Altitude altitude);

		/// <summary>
		/// Identify trend
		/// </summary>
		/// <param name="values">Base to identify trend</param>
		/// <returns>The trend</returns>
		Trend Identify(IList<decimal> values);
	}
}
