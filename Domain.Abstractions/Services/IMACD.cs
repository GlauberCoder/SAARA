using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Services
{
    public interface IMACD : ICanBeClassifiedByAltitude
    {
		decimal ShortEMA { get; set; }
		decimal LongEMA { get; set; }
		decimal Value { get; set; }
		decimal Signal { get; set; }
		decimal Histogram { get; set; }
		ICandle Candle { get; set; }
		Trend Trend { get; }
		TradeSignal TradeSignal { get; }
		TradeSignal CrossTradeSignal { get; }
		IMACD CalculateValue();
		IMACD CalculateSignal(IList<IMACD> macds, int signalEMA);
		IMACD CalculateHistogram();
	}
}
