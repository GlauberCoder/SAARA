using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Extensions;
using Util.Extensions;
using Domain.Entitys;

namespace Infra.ExchangerDataReader.BinanceDataReader
{
    public class BinanceDataReader : ExchangerDataReader
	{
		private string symbolKey => "{{symbol}}";
		private string intervalKey => "{{interval}}";
		private string endTimeKey => "{{endTime}}";
		private string startTimeKey => "{{startTime}}";
		protected override string BaseURL => @"https://api.binance.com/api/v1/";

		protected override string QueryURL => $@"klines?symbol={symbolKey}&interval={intervalKey}&startTime={startTimeKey}&endTime={endTimeKey}";

		protected override IDictionary<string, string> GetParameters(ISymbol symbol, CandleTimespan timespan, DateTime date)
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add(symbolKey, symbol.Name);
			dictionary.Add(intervalKey, timespan.stringFormat());
			dictionary.Add(startTimeKey, date.StartFor(timespan).ToUniversalTime().MillisecondsSince1970().ToString());
			dictionary.Add(endTimeKey, date.FinishFor(timespan).ToUniversalTime().MillisecondsSince1970().ToString());

			return dictionary;
		}

		public override ICandle GetCandleFrom(string response)
		{
			var values = response.Deserilize<string[][]>()[0];

			return new Candle()
			{
				Date = getValue(values, BinanceValuesPosition.OpenTime).ToDouble().TimestampToDateTime(),
				Open = getValue(values, BinanceValuesPosition.Open).ToDecimal(),
				Close = getValue(values, BinanceValuesPosition.Close).ToDecimal(),
				High = getValue(values, BinanceValuesPosition.High).ToDecimal(),
				Low = getValue(values, BinanceValuesPosition.Low).ToDecimal(),
				Vol = getValue(values, BinanceValuesPosition.Volume).ToDecimal(),
			};
		}

		private string getValue(string[] values, BinanceValuesPosition position)
		{
			return values[(int)position];
		}
	}
}
