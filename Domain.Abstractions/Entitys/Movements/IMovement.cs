using Domain.Abstractions.Enums;

namespace Domain.Abstractions.Entitys.Movements
{
	public interface IMovement : IBaseEntity<IMovement>
	{
		decimal Investment { get; set; }
		decimal Tax { get; set; }
		MovementType Transaction { get; set; }
		decimal Price { get; set; }
	}
}
