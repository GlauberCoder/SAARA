using Domain.Abstractions.Enums;
using System;

namespace Domain.Abstractions.Entitys
{
	public interface ICandle : IBaseEntity
	{
		IExchangerSymbol Symbol { get; set; }
		decimal Open { get; set; }
		decimal Close { get; set; }
		decimal Max { get; set; }
		decimal Min { get; set; }
		decimal Vol { get; set; }
		CandleTimespan TimespanType { get; set; }
		DateTime OpenTime { get; set; }
		DateTime CloseTime { get; set; }
	}
}
