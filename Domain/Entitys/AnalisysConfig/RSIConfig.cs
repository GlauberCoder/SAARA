using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entitys.AnalisysConfig
{
	public class RSIConfig : BaseEntity<RSIConfig>, IRSIConfig
	{
		public virtual int Length { get; set; }
	}
}
