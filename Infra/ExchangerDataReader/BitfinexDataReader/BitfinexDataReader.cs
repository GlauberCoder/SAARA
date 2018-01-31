using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
using Infra.Abstractions.ExchangerDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Extensions;
using Util.Extensions;

namespace Infra.ExchangerDataReader.BitcoinTradeDataReader
{
	public class BitfinexDataReader : ExchangerDataReader, IBitfinexDataReader
	{
		private enum ValuesPosition
		{
			Timespan = 0,
			Open = 1,
			Close = 2,
			High = 3,
			Low = 4,
			Vol = 5

		}
		private string symbolKey => "{{symbol}}";
		private string timespanKey => "{{timespan}}";
		private string endTimeKey => "{{endTime}}";
		protected override string BaseURL => @"https://api.bitfinex.com/v2/";

		protected override string QueryURL => $@"candles/trade:{timespanKey}:t{symbolKey}/hist?end={endTimeKey}&limit=1";
		
		protected override IDictionary<string, string> GetParameters(ISymbol symbol, CandleTimespan timespan, DateTime date)
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add(symbolKey, symbol.Name);
			dictionary.Add(timespanKey, timespan.stringFormat());
			dictionary.Add(endTimeKey, date.ToUniversalTime().MillisecondsSince1970().ToString());

			return dictionary;
		}

		public override ICandle GetCandleFrom(string response)
		{
			var values = response.Deserilize<string[][]>()[0];

			return new Candle()
			{
				Date = getValue(values, ValuesPosition.Timespan).ToDouble().TimestampToDateTime(),
				Open = getValue(values, ValuesPosition.Open).ToDecimal(),
				Close = getValue(values, ValuesPosition.Close).ToDecimal(),
				High = getValue(values, ValuesPosition.High).ToDecimal(),
				Low = getValue(values, ValuesPosition.Low).ToDecimal(),
				Vol = getValue(values, ValuesPosition.Vol).ToDecimal()
			};
		}

		private string getValue(string[] values, ValuesPosition position)
		{
			return values[(int)position];
		}
	}
}
