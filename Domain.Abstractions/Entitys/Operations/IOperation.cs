using Domain.Abstractions.Entitys.Movements;
using System;
using System.Collections.Generic;

namespace Domain.Abstractions.Entitys.Operations
{
	public interface IOperation : IBaseEntity
	{
		IList<IMovement> Movements { get; set; }
		IExchangerSymbol ExchangerSymbol { get; set; }
		DateTime Start { get; set; }
		DateTime End { get; set; }
	}
}
