﻿using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Entitys.AnalisysConfig
{
	public class MACDConfig : BaseEntity, IMACDConfig
	{

		public virtual int EMA1 { get; set; }
		public virtual int EMA2 { get; set; }
		public virtual int ShortEMA { get { return EMA1 > EMA2 ? EMA1 : EMA2; } }
		public virtual int LongEMA { get { return EMA1 > EMA2 ? EMA2 : EMA1; } }
		public virtual int SignalEMA { get; set; }
	}
}
