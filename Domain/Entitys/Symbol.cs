using Domain.Abstractions.Entitys;
using System;

namespace Domain.Entitys
{
	public class Symbol : BaseEntity, ISymbol
	{
		public virtual string Description { get; set; }
	}
}
