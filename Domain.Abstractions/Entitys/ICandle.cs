using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using System;

namespace Domain.Abstractions.Entitys
{
	public interface ICandle : IBaseEntity<ICandle>, ICanBeClassifiedByAltitude
	{
		IExchangerSymbol Symbol { get; set; }
		decimal Open { get; set; }
		decimal Close { get; set; }
		decimal High { get; set; }
		decimal Low { get; set; }
		decimal Vol { get; set; }
		CandleTimespan TimespanType { get; set; }
		DateTime Date { get; set; }
	}
}
