using Domain.Abstractions;
using Domain.Abstractions.Entitys;
using System;

namespace Domain.Entitys
{
	public class ExchangerSymbol : Symbol, IExchangerSymbol
	{
		public virtual decimal Cambio { get; set; }
		public virtual IExchanger Exchanger { get; set; }
	}
}
