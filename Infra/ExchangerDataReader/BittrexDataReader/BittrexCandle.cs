using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Infra.ExchangerDataReader.BittrexDataReader
{
	class BittrexCandle
	{
		[JsonProperty (PropertyName = "BV")]
		public virtual decimal BaseVolume { get; set; }
		[JsonProperty(PropertyName = "C")]
		public virtual decimal Close { get; set; }
		[JsonProperty(PropertyName = "H")]
		public virtual decimal High { get; set; }
		[JsonProperty(PropertyName = "L")]
		public virtual decimal Low { get; set; }
		[JsonProperty(PropertyName = "O")]
		public virtual decimal Open { get; set; }
		[JsonProperty(PropertyName = "T")]
		public virtual DateTime Timespan { get; set; }
		[JsonProperty(PropertyName = "V")]
		public virtual decimal Volume { get; set; }
	}
}
