using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Services
{
	public interface IAltitudeAnalyser<T>
		where T : ICanBeClassifiedByAltitude
	{
		/// <summary>
		/// Set the analyser to identify altitude by a min variation
		/// </summary>
		/// <param name="topMinVariation">Min variation from a bottom to form a top</param>
		/// <param name="bottomMinVariation">Min variation from a top to form a bottom</param>
		/// <returns>This analyser</returns>
		IAltitudeAnalyserConfigured<T> ByVariation(decimal topMinVariation, decimal bottomMinVariation);

		/// <summary>
		/// Set the analyser to identify altitude by a min separation length
		/// </summary>
		/// <param name="minTopLength">Min separation to identify a top</param>
		/// <param name="minBottomLength">Min separation to identify a bottom</param>
		/// <returns>This analyser</returns>
		IAltitudeAnalyserConfigured<T> ByLength(int minTopLength, int minBottomLength);

		IAltitudeAnalyserConfigured<T> Configure(IAltitudeAnalyserConfig config);
	}
}
