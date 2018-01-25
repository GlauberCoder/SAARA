using System;

namespace Domain.Abstractions.Entitys
{
	public interface IBaseEntity
	{
		long InstanceID { get; }
		bool Saving { get; }
		long ID { get; set; }
		string Name { get; set; }
		string Identification();
		DateTime? Exclusion { get; set; }
		bool Excluded { get; }
		void Normalize();
		object Simplify();
		bool Saved { get; }
	}

	public interface IBaseEntity<T> : IBaseEntity
	{
	}

}
