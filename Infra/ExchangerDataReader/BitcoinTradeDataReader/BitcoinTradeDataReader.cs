using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
using Infra.Abstractions.ExchangerDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Extensions;

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
			return GetISOFormatDates(date.StartFor(timespan), (int)timespan);
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
