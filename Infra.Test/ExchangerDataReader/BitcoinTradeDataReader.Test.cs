using Domain.Abstractions.Enums;
using Infra.ExchangerDataReader.BitcoinTradeDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Infra.Test
{
	public class BitcoinTradeDataReaderTest
    {
		[
			Fact(DisplayName = "The BitcoinTrade template URL should be correct")
		]
		public void The_BitcoinTradeTemplate_Url_Should_Be_Correct()
		{
			var excpected = "https://api.bitcointrade.com.br/v1/public/{{symbol}}/trades?start_time={{startTime}}&end_time={{endTime}}&page_size={{pageSize}}&current_page={{currentPage}}";
			var actual = new BitcoinTradeDataReader().TemplateURL;
			Assert.Equal(excpected, actual);
		}
		
	}
}
