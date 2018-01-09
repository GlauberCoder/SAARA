using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AnalisysConfig
{
	public class EMAConfig : BaseEntity
	{

		public virtual int EMA1 { get; set; }
		public virtual int EMA2 { get; set; }

		public virtual int ShortEMA { get { return EMA1 > EMA2 ? EMA1 : EMA2; } }
		public virtual int LongEMA { get { return EMA1 > EMA2 ? EMA2 : EMA1; } }


	}
}
