using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AnalisysConfig
{
	public class AnalysisConfig : BaseEntity
	{
		public virtual Account Author { get; set; }
		public virtual IList<EMAConfig> EMAs { get; set; }
		public virtual IList<MACDConfig> MACDs { get; set; }

	}
}
