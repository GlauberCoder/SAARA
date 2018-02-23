using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Services
{
	public interface ITrendAnalyserConfigured<T>
		where T : ICanBeClassifiedByAltitude
	{
		/// <summary>
		/// Identify trend
		/// </summary>
		/// <param name="values">Base to identify trend</param>
		/// <returns>The trend</returns>
		Trend Identify(IList<T> values);
	}
}
