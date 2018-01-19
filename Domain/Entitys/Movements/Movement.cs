using Domain.Abstractions.Entitys.Movements;
using Domain.Abstractions.Enums;

namespace Domain.Entitys.Movements
{
	public abstract class Movement : BaseEntity, IMovement
	{
		public virtual decimal Investment { get; set; }
		public virtual decimal Tax { get; set; }
		public virtual MovementType Transaction { get; set; }
		public virtual decimal Price { get; set; }
	}
}
