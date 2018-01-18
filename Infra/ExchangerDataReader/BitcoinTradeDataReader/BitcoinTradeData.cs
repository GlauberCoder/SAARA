using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.ExchangerDataReader.BitcoinTradeDataReader
{
	class BitcoinTradeData
	{
		public virtual string Message { get; set; }
		public virtual Data Data { get; set; }
	}
}
