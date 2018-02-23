using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Services
{
	public interface IAltitudeAnalyserConfigured<T>
		where T : ICanBeClassifiedByAltitude
	{
		/// <summary>
		/// Identify altitude
		/// </summary>
		/// <param name="values">Base to identify altitudes</param>
		/// <returns>A list of altitudes for each value in the list</returns>
		IList<T> Identify(IList<T> values);
	}
}
