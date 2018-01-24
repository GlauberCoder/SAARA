using Domain.Abstractions;
using Domain.Abstractions.Entitys;
using System;

namespace Domain.Entitys
{
	public class ExchangerSymbol : BaseEntity<ExchangerSymbol>, IExchangerSymbol
	{
		public virtual string Description { get; set; }
		public virtual decimal Cambio { get; set; }
		public virtual IExchanger Exchanger { get; set; }
	}
}
