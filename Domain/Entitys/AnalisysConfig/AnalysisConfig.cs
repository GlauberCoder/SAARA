using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using System.Collections.Generic;

namespace Domain.Entitys.AnalisysConfig
{
	public class AnalysisConfig : BaseEntity<AnalysisConfig>, IAnalysisConfig
	{
		public virtual IAccount Author { get; set; }
		public virtual IList<IEMAConfig> EMAs { get; set; }
		public virtual IList<IMACDConfig> MACDs { get; set; }
	}
}
