using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class Candle : BaseEntity
	{
		public virtual ExchangerSymbol Symbol { get; set; }
		public virtual decimal Open { get; set; }
		public virtual decimal Close { get; set; }
		public virtual decimal Max { get; set; }
		public virtual decimal Min { get; set; }
		public virtual decimal Vol { get; set; }
		public virtual CandleTimespanType TimespanType { get; set; }
		public virtual DateTime OpenTime { get; set; }
		public virtual DateTime CloseTime { get; set; }

	}
}
