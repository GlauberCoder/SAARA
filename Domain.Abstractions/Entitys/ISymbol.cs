using System;

namespace Domain.Abstractions.Entitys
{
	public interface ISymbol : IBaseEntity<ISymbol>
	{
		string Description { get; set; }
	}
}
