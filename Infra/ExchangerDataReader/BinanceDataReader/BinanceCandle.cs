using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.ExchangerDataReader.BinanceDataReader
{
    class BinanceCandle
    {
		public long openTime;
		public decimal open;
		public decimal high;
		public decimal low;
		public decimal close;
		public decimal volume;
		public long closeTime;
		public decimal quoteAssetVolume;
		public int numberOfTrades;
		public decimal takerBuyBaseAssetVolume;
		public decimal takerBuyQuoteAssetVolume;
		public decimal ignore;
	}

	enum BinanceValuesPosition
	{
		OpenTime = 0,
		Open = 1,
		High = 2,
		Low = 3,
		Close = 4,
		Volume = 5,
		CloseTime = 6,
		QuoteAssetVolume = 7,
		NumberOfTrades = 8,
		takerBuyBaseAssetVolume = 9,
		takerBuyQuoteAssetVolume = 10,
		ignore = 11
	}
}

