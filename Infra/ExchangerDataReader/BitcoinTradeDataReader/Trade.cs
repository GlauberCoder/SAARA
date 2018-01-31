using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.ExchangerDataReader.BitcoinTradeDataReader
{
	class Trade
	{
		public virtual string Type { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual decimal Unit_Price { get; set; }
		public virtual string Active_Order_Code { get; set; }
		public virtual string Passive_Order_Code { get; set; }
		public virtual DateTime Date { get; set; }

	}
}
