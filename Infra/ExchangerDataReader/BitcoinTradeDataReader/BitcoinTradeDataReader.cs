﻿using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
using Infra.Abstractions.ExchangerDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Infra.ExchangerDataReader.BitcoinTradeDataReader
{
	public class BitcoinTradeDataReader : ExchangerDataReader, IBitcoinTradeDataReader
	{
		private string symbolKey => "{{symbol}}";
		private string startTimeKey => "{{startTime}}";
		private string endTimeKey => "{{endTime}}";
		private string pageSizeKey => "{{pageSize}}";
		private string currentPageKey => "{{currentPage}}";

		protected override string BaseURL => @"https://api.bitcointrade.com.br/v1/";

		protected override string QueryURL => $@"public/{symbolKey}/trades?start_time={startTimeKey}&end_time={endTimeKey}&page_size={pageSizeKey}&current_page={currentPageKey}";

		public override ICandle GetCandleFrom(string response)
		{
			var json = JsonConvert.DeserializeObject<BitcoinTradeData>(response);
			var trades = json.Data.Trades;

			var priceSortedTrades = trades.OrderBy(x => x.Unit_Price);
			var dateSortedTrades = trades.OrderBy(x => x.Date);

			return new Candle()
			{
				Date = dateSortedTrades.First().Date,
				Open = dateSortedTrades.First().Unit_Price,
				Close = dateSortedTrades.Last().Unit_Price,
				High = priceSortedTrades.Last().Unit_Price,
				Low = priceSortedTrades.First().Unit_Price,
				Vol = trades.Sum(x => x.Amount)
			};
		}

		protected override IDictionary<string, string> GetParameters(ISymbol symbol, CandleTimespan timespan, DateTime date)
		{
			var dictionary = GetTimeValues(timespan, date);
			dictionary.Add(symbolKey, symbol.Name);
			dictionary.Add(pageSizeKey, "200");
			dictionary.Add(currentPageKey, "1");

			return dictionary;
		}

		public IDictionary<string, string> GetTimeValues(CandleTimespan timespan, DateTime date)
		{
			return GetISOFormatDates( GetNearExactTime(timespan, date), (int)timespan);
		}

		public DateTime GetNearExactTime(CandleTimespan timespan, DateTime date)
		{
			var minute = date.Minute;
			switch (timespan)
			{
				case CandleTimespan.OneMinute:
					return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);

				case CandleTimespan.FiveMinutes:
				case CandleTimespan.FifteenMinutes:
				case CandleTimespan.ThirtyMinutes:
					minute = GetNearExactNumber(minute, (int) timespan);
					return new DateTime(date.Year, date.Month, date.Day, date.Hour, minute, 0);

				case CandleTimespan.OneHour:
					return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);

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

		private IDictionary<string, string> GetISOFormatDates(DateTime endDateTime, int timeSpan)
		{
			var dateFormat = "yyyy-MM-ddTHH:mm:ss-03:00";

			var startTime = endDateTime.AddMinutes(-timeSpan).ToString(dateFormat);
			var endTime = endDateTime.AddSeconds(-1).ToString(dateFormat);

			var dictionary = new Dictionary<string, string>();

			dictionary.Add(startTimeKey, startTime);
			dictionary.Add(endTimeKey, endTime);

			return dictionary;
		}
	}
}