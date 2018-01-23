using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
using Infra.Abstractions.ExchangerDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Infra.ExchangerDataReader
{
	public abstract class ExchangerDataReader : IExchangerDataReader
	{ 
		protected abstract string BaseURL { get; }
		protected abstract string QueryURL { get; }
		public virtual string TemplateURL { get { return $"{BaseURL}{QueryURL}"; } }

		public virtual string GetUrlFrom(ISymbol symbol, CandleTimespan timespan, DateTime date)
		{
			var url = TemplateURL;

			foreach (var item in GetParameters(symbol, timespan, date))
				url = url.Replace(item.Key, item.Value);

			return url;
		}

		public virtual ICandle ReadOneMinuteCandle(ISymbol symbol, DateTime? date)
		{
			return Read(symbol, CandleTimespan.OneMinute, date);
		}

		public virtual ICandle Read(ISymbol symbol, CandleTimespan timespan, DateTime? date)
		{
			ICandle candle = null;
			var baseDate = date ?? DateTime.Now;
			var url = GetUrlFrom(symbol, timespan, baseDate);

			var request = (HttpWebRequest)WebRequest.Create(url);
			request.AutomaticDecompression = DecompressionMethods.GZip;

			using (var response = (HttpWebResponse)request.GetResponse())
			{
				using (var stream = response.GetResponseStream())
				{
					using (var reader = new StreamReader(stream))
					{
						candle = GetCandleFrom(reader.ReadToEnd());
					}
				}
			}

			return candle;
		}

		public abstract ICandle GetCandleFrom(string response);

		protected abstract IDictionary<string, string> GetParameters(ISymbol symbol, CandleTimespan timespan, DateTime date);

	}
}
