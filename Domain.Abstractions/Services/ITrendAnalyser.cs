using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Services
{
	public interface ITrendAnalyser<T>
		where T : ICanBeClassifiedByAltitude
	{
		/// <summary>
		/// Sets the analyser to identify trend by first versus last tops (or bottoms)
		/// </summary>
		/// <param name="altitude">Altitude used, it can be top or bottom</param>
		/// <returns>This analyser</returns>
		ITrendAnalyserConfigured<T> Configure(ITrendAnalyserConfig config);
	}
}
