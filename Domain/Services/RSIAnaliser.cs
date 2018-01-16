using Domain.Abstractions;
using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Entitys;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
	public class RSIAnaliser : IRSIAnaliser
	{
		public virtual decimal RSI => throw new NotImplementedException();
		public virtual PriceSignal PriceSignal => throw new NotImplementedException();
		public virtual Trend RSIRange => throw new NotImplementedException();
		public virtual Trend Divergence => throw new NotImplementedException();
		public virtual Trend FailSwing => throw new NotImplementedException();
		public virtual Trend PositiveNegativeReversal => throw new NotImplementedException();
		public virtual IRSIAnaliser Calculate(IRSIConfig config, ICandleAnalyser analysis)
		{
			throw new NotImplementedException();
		}

		public virtual IRSIAnaliser Calculate(IRSIConfig config, ICandleAnalyser analysis, ICandle candle)
		{
			throw new NotImplementedException();
		}
	}
}
