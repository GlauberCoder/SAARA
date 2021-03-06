﻿using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entitys.AnalisysConfig
{
	public class AnalysisConfig : BaseEntity, IAnalysisConfig
	{
		public virtual IAccount Author { get; set; }
		public virtual IList<IEMAConfig> EMAs { get; set; }
		public virtual IList<IMACDConfig> MACDs { get; set; }

	}
}
