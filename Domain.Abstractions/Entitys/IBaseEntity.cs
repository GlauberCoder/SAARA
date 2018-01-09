using System;

namespace Domain.Abstractions.Entitys
{
	public interface IBaseEntity
	{
		long ID { get; set; }
		string Name { get; set; }
	}
}
