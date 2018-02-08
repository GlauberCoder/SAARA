﻿using Domain.Abstractions.Entitys.AnalisysConfig;

namespace Domain.Entitys.AnalisysConfig
{
	public class MACDConfig : BaseEntity<MACDConfig>, IMACDConfig
	{
		//TODO: composicao do EMA
		public virtual int EMA1 { get; set; }
		public virtual int EMA2 { get; set; }

		public virtual int ShortEMA { get { return EMA1 > EMA2 ? EMA2 : EMA1; } }
		public virtual int LongEMA { get { return EMA1 > EMA2 ? EMA1 : EMA2; } }
		public virtual int SignalEMA { get; set; }
		public virtual decimal CrossoverTolerance { get; set; }
		public virtual decimal CrossunderTolerance { get; set; }
		public int CandlesCountToCrossover { get; set; }
	}
}
