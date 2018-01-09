using System;

namespace Domain.Abstractions.Entitys
{
	public interface IExchangerSymbol : ISymbol
	{
		decimal Cambio { get; set; }
		IExchanger Exchanger { get; set; }
	}
}
