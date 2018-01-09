using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entitys
{
	public class Candle : BaseEntity, ICandle
	{
		public virtual IExchangerSymbol Symbol { get; set; }
		public virtual decimal Open { get; set; }
		public virtual decimal Close { get; set; }
		public virtual decimal Max { get; set; }
		public virtual decimal Min { get; set; }
		public virtual decimal Vol { get; set; }
		public virtual CandleTimespan TimespanType { get; set; }
		public virtual DateTime OpenTime { get; set; }
		public virtual DateTime CloseTime { get; set; }

	}
}
