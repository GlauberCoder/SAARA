using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Extensions;
using Newtonsoft.Json;
using Domain.Extensions;

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

		protected override IDictionary<string, string> GetParameters(ISymbol symbol, CandleTimespan timespan, DateTime date)
		{
			var dictionary = new Dictionary<string, string>();
			var afterTime = date.Add(timespan);
			dictionary.Add(commandKey, "returnTradeHistory");
			dictionary.Add(currencyPairKey, symbol.Name);
			dictionary.Add(startTimeKey, date.StartFor(timespan).ToUniversalTime().SecondsSince1970().ToString() );
			dictionary.Add(endTimeKey, date.FinishFor(timespan).ToUniversalTime().SecondsSince1970().ToString() );

			return dictionary;
		}

	}
}
