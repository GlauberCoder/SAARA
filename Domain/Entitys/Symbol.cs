using Domain.Abstractions.Entitys;

namespace Domain.Entitys
{
	public class Symbol : BaseEntity, ISymbol
	{
		public virtual string Description { get; set; }
	}
}
