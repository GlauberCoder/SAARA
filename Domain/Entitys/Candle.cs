using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using System;

namespace Domain.Entitys
{
	public class Candle : BaseEntity<Candle>, ICandle
	{
		public virtual IExchangerSymbol Symbol { get; set; }
		public virtual decimal Open { get; set; }
		public virtual decimal Close { get; set; }
		public virtual decimal High { get; set; }
		public virtual decimal Low { get; set; }
		public virtual decimal Vol { get; set; }
		public virtual CandleTimespan TimespanType { get; set; }
		public virtual DateTime Date { get; set; }
	}
}
