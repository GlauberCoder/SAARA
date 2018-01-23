using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using System;

namespace Infra.Abstractions.ExchangerDataReader
{
	public interface IExchangerDataReader
	{
		ICandle ReadOneMinuteCandle(ISymbol symbol, DateTime? date);

		ICandle Read(ISymbol symbol, CandleTimespan timespan, DateTime? date);
	}
}
