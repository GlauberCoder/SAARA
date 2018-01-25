﻿using Domain.Abstractions.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Entitys.AnalisysConfig
{
	public interface IEMAConfig : IBaseEntity<IEMAConfig>
	{
		int ShortEMA { get; }
		int LongEMA { get; }
	}
}
