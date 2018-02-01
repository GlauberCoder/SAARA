using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Infra.ExchangerDataReader.BitcoinTradeDataReader
{
	class Data
	{
		public virtual Pagination Pagination { get; set; }
		public virtual List<Trade> Trades { get; set; }
	}
}
