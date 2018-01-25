using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.Movements;
using Domain.Abstractions.Entitys.Operations;
using System;
using System.Collections.Generic;

namespace Domain.Entitys.Operations
{
	public abstract class Operation : BaseEntity<Operation>, IOperation
	{
		public virtual IList<IMovement> Movements { get; set; }
		public virtual IExchangerSymbol ExchangerSymbol { get; set; }
		public virtual DateTime Start { get; set; }
		public virtual DateTime End { get; set; }
	}
}
