using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.ExchangerDataReader.BitcoinTradeDataReader
{
	class Pagination
	{
		public virtual int Total_Pages { get; set; }
		public virtual int Current_Page { get; set; }
		public virtual int Page_Size { get; set; }
		public virtual int Registers_Count { get; set; }
	}
}
