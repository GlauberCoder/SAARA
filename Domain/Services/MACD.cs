using Domain.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Extensions;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Entitys;

namespace Domain.Services
{
	public class MACD : IMACD
	{
		public virtual decimal ShortEMA { get; set; }
		public virtual decimal LongEMA { get; set; }
		public virtual decimal Value { get; set; }
		public virtual decimal Signal { get; set; }
		public virtual decimal Histogram { get; set; }
		public virtual Altitude Altitude { get; set; }
		public virtual ICandle Candle { get; set; }
		public virtual IMACD CalculateValue()
		{
			Value = ShortEMA - LongEMA;
			return this;
		}
		public virtual IMACD CalculateSignal(IList<IMACD> macds, int signalEMA)
		{
			Signal = macds.EMA(signalEMA);
			return this;
		}
		public virtual IMACD CalculateHistogram()
		{
			Histogram = Value - Signal;
			return this;
		}

		public virtual decimal ValueForAltitude()
		{
			return Value;
		}

		public virtual void ClassifyByAltitude(Altitude altitude)
		{
			Altitude = altitude;
		}

		public virtual Trend Trend => (Value > Signal) ? Trend.Up : Trend.Down;
		public virtual TradeSignal CrossTradeSignal => Trend == Trend.Up ? TradeSignal.Long : TradeSignal.Short;
		public virtual TradeSignal TradeSignal
		{
			get
			{
				if (CrossTradeSignal == TradeSignal.Long)
					return Value > 0 ? TradeSignal.StrongLong : TradeSignal.WeakLong;

				if (CrossTradeSignal == TradeSignal.Short)
					return Value < 0 ? TradeSignal.StrongShort : TradeSignal.WeakShort;

				return TradeSignal.Hold;
			}
		}
		
	}
}
