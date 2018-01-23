using Domain.Abstractions.Entitys;

namespace Domain.Entitys
{
	public class BaseEntity : IBaseEntity
	{
		public virtual long ID { get; set; }
		public virtual string Name { get; set; }
	}
}
