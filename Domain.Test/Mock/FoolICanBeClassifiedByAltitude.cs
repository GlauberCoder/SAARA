using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public static IList<FoolICanBeClassifiedByAltitude> From(IList<decimal> values)
		{
			return values.Select(v => new FoolICanBeClassifiedByAltitude { Value = v }).ToList();
		}
	}
}
