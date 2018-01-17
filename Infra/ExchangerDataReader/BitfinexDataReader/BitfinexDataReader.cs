using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
using Infra.Abstractions.ExchangerDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
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
		protected override string BaseURL => @"https://api.bitfinex.com/v2/";

		protected override string QueryURL => $@"candles/trade:{timespanKey}:t{symbolKey}/last";
		

		public override string GetUrlFrom(ISymbol symbol, CandleTimespan timespan, DateTime date)
		{
			var url = TemplateURL;

			foreach (var item in GetParameters(symbol, timespan, date))
				url = url.Replace(item.Key, item.Value);

			return url;
		}


		protected override IDictionary<string, string> GetParameters(ISymbol symbol, CandleTimespan timespan, DateTime date)
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add(symbolKey, symbol.Name);
			dictionary.Add(timespanKey, GetTimespanValueFrom(timespan));

			return dictionary;
		}

		private string GetTimespanValueFrom(CandleTimespan timespan)
		{
			switch (timespan)
			{
				case CandleTimespan.OneMinute:
					return "1m";
				case CandleTimespan.FiveMinutes:
					return "5m"; 
				case CandleTimespan.FifteenMinutes:
					return "15m";
				case CandleTimespan.ThirtyMinutes:
					return "30m";
				case CandleTimespan.OneHour:
					return "1h";
				default:
					throw new InvalidCastException("Timespan not recognized");
			}
		}

		public override ICandle GetCandleFrom(string response)
		{
			var values = response.Deserilize<string[]>();

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
