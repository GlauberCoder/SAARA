using Domain.Abstractions.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Entitys.AnalisysConfig
{
	public interface IAnalysisConfig : IBaseEntity
	{
		IAccount Author { get; set; }
		IList<IEMAConfig> EMAs { get; set; }
		IList<IMACDConfig> MACDs { get; set; }
	}
}
