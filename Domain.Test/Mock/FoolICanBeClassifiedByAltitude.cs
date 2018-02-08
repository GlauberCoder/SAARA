using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Test.Mock
{
	class FoolICanBeClassifiedByAltitude : ICanBeClassifiedByAltitude
	{
		public decimal Value { get; set; }
		public Altitude Altitude { get; set; }

		public void ClassifyByAltitude(Altitude altitude)
		{
			Altitude = altitude;
		}
		public decimal ValueForAltitude()
		{
			return Value;
		}
		public static IList<FoolICanBeClassifiedByAltitude> From(IList<double> values)
		{
			var result = new List<FoolICanBeClassifiedByAltitude>();
			foreach (var value in values.CastAs<decimal>())
				result.Add(new FoolICanBeClassifiedByAltitude { Value = value });
			return result;
		}

		
	}
}
