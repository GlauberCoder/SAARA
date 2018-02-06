using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Extensions;
using Newtonsoft.Json;
using Domain.Extensions;

namespace Infra.ExchangerDataReader.BittrexDataReader
{
	public class BittrexDataReader : ExchangerDataReader
	{
		public string currencyPairKey = "{{currencyPairKey}}";
		public string tickIntervalKey = "{{tickInterval}}";
		public string candleTimeKey = "{{candleTime}}";
		private DateTime candleDate { get; set; }
		private CandleTimespan candleTimespan { get; set; }

		protected override string BaseURL => @"https://bittrex.com/Api/v2.0/pub/";
		protected override string QueryURL => $@"market/GetTicks?marketName={currencyPairKey}&tickInterval={tickIntervalKey}&_={candleTimeKey}";


		public override ICandle GetCandleFrom(string response)
		{
			var json = JsonConvert.DeserializeObject<BittrexCandleData>(response);
			var lastCandle = getCandleByDate(json.Result, CandleTimespan.OneHour, DateTime.Now );
			return new Candle()
			{
				Date = lastCandle.Timespan,
				Open = lastCandle.Open,
				Close = lastCandle.Close,
				High = lastCandle.High,
				Low = lastCandle.Low,
				Vol = lastCandle.Volume
			};
		}

		protected override IDictionary<string, string> GetParameters(ISymbol symbol, CandleTimespan timespan, DateTime date)
		{
			var dictionary = new Dictionary<string, string>();

			dictionary.Add(currencyPairKey, symbol.Name);
			dictionary.Add(tickIntervalKey, stringFormat(timespan));
			dictionary.Add(candleTimeKey, date.ToUniversalTime().MillisecondsSince1970().ToString());

			return dictionary;
		}
		
		private BittrexCandle getCandleByDate(IList<BittrexCandle> candles, CandleTimespan timespan, DateTime date){
			var time = date.StartFor(timespan).ToUniversalTime();

			return candles.FirstOrDefault(c => c.Timespan == time);
		}

		public string stringFormat(CandleTimespan timespan)
		{
			switch (timespan)
			{
				case CandleTimespan.OneMinute:
					return "oneMin";
				case CandleTimespan.FiveMinutes:
					return "fiveMin";
				case CandleTimespan.FifteenMinutes:
					return "fifteenMin";
				case CandleTimespan.ThirtyMinutes:
					return "thirtyMin";
				case CandleTimespan.OneHour:
					return "hour";
				default:
					return null;
			}
		}

	}
}
