using Domain.Abstractions;
using Domain.Abstractions.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entitys
{
	public class BaseEntity : IBaseEntity
	{
		public virtual long ID { get; set; }
		public virtual string Name { get; set; }
	}
}
