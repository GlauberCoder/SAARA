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
		public virtual ICandle Read(ISymbol symbol, CandleTimespan timespan)
		{
			ICandle candle = null;
			var url = GetUrlFrom(symbol, timespan);

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

		public abstract string GetUrlFrom(ISymbol symbol, CandleTimespan timespan);
	}
}
