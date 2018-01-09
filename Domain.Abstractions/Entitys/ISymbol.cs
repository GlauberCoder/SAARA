using System;

namespace Domain.Abstractions.Entitys
{
	public interface ISymbol : IBaseEntity
	{
		string Description { get; set; }
	}
}
