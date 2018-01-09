using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class BaseEntity
	{
		public virtual long ID { get; set; }
		public virtual string Name { get; set; }
	}
}
