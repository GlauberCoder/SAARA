using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.ExchangerDataReader.BittrexDataReader
{
    class BittrexCandleData
    {
		public virtual bool Success { get; set; }
		public virtual string Message { get; set; }
		public virtual IList<BittrexCandle> Result { get; set; }
	}
}
