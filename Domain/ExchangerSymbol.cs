using System;

namespace Domain
{
	public class ExchangerSymbol : Symbol
	{
		public virtual decimal Cambio { get; set; }
		public virtual Exchanger Exchanger { get; set; }
	}
}
