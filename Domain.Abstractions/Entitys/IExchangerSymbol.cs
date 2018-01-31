using System;

namespace Domain.Abstractions.Entitys
{
	public interface IExchangerSymbol : IBaseEntity<IExchangerSymbol>
	{
		string Description { get; set; }
		decimal Cambio { get; set; }
		IExchanger Exchanger { get; set; }
	}
}
