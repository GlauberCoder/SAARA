using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.ExchangerDataReader.PoloniexDataReader
{
    class Trade
    {
		public long globalTradeID { get; set; }
		public long tradeId { get; set; }
		public virtual DateTime date { get; set; }
		public string type { get; set; }
		public decimal rate { get; set; }
		public decimal amount { get; set; }
		public decimal total { get; set; }
	}
}
