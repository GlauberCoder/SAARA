using Domain.Abstractions.Entitys;

namespace Domain.Entitys
{
	public class ExchangerSymbol : Symbol, IExchangerSymbol
	{
		public virtual decimal Cambio { get; set; }
		public virtual IExchanger Exchanger { get; set; }
	}
}
