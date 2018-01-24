using Domain.Abstractions.Entitys;
using System;

namespace Domain.Entitys
{
	public class Symbol : BaseEntity<Symbol>, ISymbol
	{
		public virtual string Description { get; set; }
	}
}
