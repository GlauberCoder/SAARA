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
		private string SymbolKey => "{{symbol}}";
		private string TimespanKey => "{{timespan}}";
		private string EndTimeKey => "{{endTime}}";
		protected override string BaseURL => @"https://api.bitfinex.com/v2/";

		protected override string QueryURL => $@"candles/trade:{TimespanKey}:t{SymbolKey}/hist?end={EndTimeKey}&limit=1";
		

		public override string GetUrlFrom(ISymbol symbol, CandleTimespan timespan, DateTime date)
		{
			var url = TemplateURL;

			foreach (var item in GetParameters(symbol, timespan, date))
				url = url.Replace(item.Key, item.Value);

			return url;
		}


		protected override IDictionary<string, string> GetParameters(ISymbol symbol, CandleTimespan timespan, DateTime date)
		{
			var dictionary = new Dictionary<string, string>
			{
				{ SymbolKey, symbol.Name },
				{ TimespanKey, GetTimespanValueFrom(timespan) },
				{ EndTimeKey, GetTimeInMillisecondsFrom(date) }
			};

			return dictionary;
		}

		private string GetTimeInMillisecondsFrom(DateTime date)
		{
			return (
						 date
						.ToUniversalTime()
						.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
						.TotalMilliseconds
					)
					.ToString();
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
			var values = response.Deserilize<string[][]>()[0];

			return new Candle()
			{
				Date = GetValue(values, ValuesPosition.Timespan).ToDouble().TimestampToDateTime(),
				Open = GetValue(values, ValuesPosition.Open).ToDecimal(),
				Close = GetValue(values, ValuesPosition.Close).ToDecimal(),
				High = GetValue(values, ValuesPosition.High).ToDecimal(),
				Low = GetValue(values, ValuesPosition.Low).ToDecimal(),
				Vol = GetValue(values, ValuesPosition.Vol).ToDecimal()
			};
		}

		private string GetValue(string[] values, ValuesPosition position)
		{
			return values[(int)position];
		}
	}
}
