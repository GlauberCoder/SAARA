using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Services
{
	public interface ICanBeClassifiedByAltitude
	{
		Altitude Altitude { get; }
		decimal ValueForAltitude();
		void ClassifyByAltitude(Altitude altitude);
	}
}
