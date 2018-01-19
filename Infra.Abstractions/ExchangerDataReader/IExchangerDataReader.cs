using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Abstractions.ExchangerDataReader
{
	public interface IExchangerDataReader
	{
		ICandle ReadOneMinuteCandle(ISymbol symbol, DateTime? date);

		ICandle Read(ISymbol symbol, CandleTimespan timespan, DateTime? date);
	}
}
