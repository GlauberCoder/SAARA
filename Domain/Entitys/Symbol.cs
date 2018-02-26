using Domain.Abstractions.Entitys;

namespace Domain.Entitys
{
	public class Symbol : BaseEntity<Symbol>, ISymbol
	{
		public virtual string Description { get; set; }

	}
}
