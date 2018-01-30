using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Extensions;
using Newtonsoft.Json;

namespace Infra.ExchangerDataReader.PoloniexDataReader
{
	public class PoloniexDataReader : ExchangerDataReader
	{
		public string currencyPairKey = "{{currencyPairKey}}";
		public string startTimeKey = "{{startTime}}";
		public string endTimeKey = "{{endTime}}";
		public string commandKey = "{{command}}";

		protected override string BaseURL => @"https://poloniex.com/public?";
		protected override string QueryURL => $@"command={commandKey}&currencyPair={currencyPairKey}&start={startTimeKey}&end={endTimeKey}";


		public override ICandle GetCandleFrom(string response)
		{
			var trades = JsonConvert.DeserializeObject<Trade[]>(response);

			var priceSortedTrades = trades.OrderBy(x => x.rate);
			var dateSortedTrades = trades.OrderBy(x => x.date);
			return new Candle()
			{
				Date = dateSortedTrades.First().date,
				Open = dateSortedTrades.First().rate,
				Close = dateSortedTrades.Last().rate,
				High = priceSortedTrades.Last().rate,
				Low = priceSortedTrades.First().rate,
				Vol = trades.Sum(x => x.amount)
			};
		}

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
			var afterTime = date.AddMilliseconds(transformTimespanToMilliseconds(timespan));
			dictionary.Add(commandKey, "returnTradeHistory");
			dictionary.Add(currencyPairKey, symbol.Name);
			dictionary.Add(startTimeKey, formatDateToMilliseconds(GetNearExactTime(timespan, date)));
			dictionary.Add(endTimeKey, formatDateToMilliseconds(GetNearExactTime(timespan, afterTime)));

			return dictionary;
		}

		public DateTime GetNearExactTime(CandleTimespan timespan, DateTime date)
		{
			var minute = date.Minute;
			switch (timespan)
			{
				case CandleTimespan.OneMinute:
					return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0).ToUniversalTime();
		
				case CandleTimespan.FiveMinutes:
				case CandleTimespan.FifteenMinutes:
				case CandleTimespan.ThirtyMinutes:
					minute = GetNearExactNumber(minute, (int)timespan);
					return new DateTime(date.Year, date.Month, date.Day, date.Hour, minute, 0).ToUniversalTime();

				case CandleTimespan.OneHour:
					return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0).ToUniversalTime();

				default:
					throw new InvalidCastException("Timespan not recognized");
			}
		}

		public int GetNearExactNumber(int number, int fraction)
		{
			if (fraction == 0)
				return 0;
			return (number % fraction == 0) ? (number) : (number - number % fraction);
		}

		private string formatTimespan(CandleTimespan timespan)
		{
			return (((int)timespan * 60)).ToString();
		}

		private long transformTimespanToMilliseconds(CandleTimespan timespan)
		{
			return (long)(timespan) * 60000;
		}

		private string formatDateToMilliseconds(DateTime date)
		{
			return $"{(long)(date - new DateTime(1970, 1, 1)).TotalMilliseconds / 1000}";

		}

	}
}
