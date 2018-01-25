using System.Collections.Generic;

namespace Domain.Abstractions.Entitys.AnalisysConfig
{
	public interface IAnalysisConfig : IBaseEntity<IAnalysisConfig>
	{
		IAccount Author { get; set; }
		IList<IEMAConfig> EMAs { get; set; }
		IList<IMACDConfig> MACDs { get; set; }
	}
}
