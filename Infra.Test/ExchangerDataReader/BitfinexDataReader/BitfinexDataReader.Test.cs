using Domain.Abstractions.Enums;
using Domain.Entitys;
using Infra.ExchangerDataReader.BitcoinTradeDataReader;
using System;
using Xunit;

namespace Infra.Test
{
	public class BitfinexDataReaderTest
	{
		[
			Fact(DisplayName = "The Bitfinex template URL should be correct")
		]
		public void The_Bitfinex_Url_Should_Be_Correct()
		{
			var excpected = "https://api.bitfinex.com/v2/candles/trade:{{timespan}}:t{{symbol}}/hist?end={{endTime}}&limit=1";
			var actual = new BitfinexDataReader().TemplateURL;
			Assert.Equal(excpected, actual);
		}

		[
			Theory(DisplayName = "The Bitfinex URL should be"),
			InlineData("BTCUSD", CandleTimespan.OneMinute, "https://api.bitfinex.com/v2/candles/trade:1m:tBTCUSD/hist?end=1507641010000&limit=1"),
			InlineData("BTCUSD", CandleTimespan.FiveMinutes, "https://api.bitfinex.com/v2/candles/trade:5m:tBTCUSD/hist?end=1507641010000&limit=1"),
			InlineData("BTCUSD", CandleTimespan.FifteenMinutes, "https://api.bitfinex.com/v2/candles/trade:15m:tBTCUSD/hist?end=1507641010000&limit=1"),
			InlineData("BTCUSD", CandleTimespan.ThirtyMinutes, "https://api.bitfinex.com/v2/candles/trade:30m:tBTCUSD/hist?end=1507641010000&limit=1"),
			InlineData("BTCUSD", CandleTimespan.OneHour, "https://api.bitfinex.com/v2/candles/trade:1h:tBTCUSD/hist?end=1507641010000&limit=1"),
			InlineData("ETHUSD", CandleTimespan.OneMinute, "https://api.bitfinex.com/v2/candles/trade:1m:tETHUSD/hist?end=1507641010000&limit=1")
		]
		public void The_Bitfinex_URL_Should_Be(string symbolName, CandleTimespan timespan, string expected)
		{
			var actual = new BitfinexDataReader().GetUrlFrom(new Symbol() { Name = symbolName }, timespan, new DateTime(2017, 10, 10, 10, 10, 10));
			Assert.Equal(expected, actual);
		}

		[
			Theory(DisplayName = "The Bitfinex Open Value To Response Shoul Be"),
			InlineData(13758, "[[1515696720000,13758,13753,13758,13753,3.26320671]]"),
			InlineData(13758.52, "[[1515696720000,13758.52,13753,13758,13753,3.26320671]]"),
		]
		public void The_Bitfinex_Open_Value_To_Response_Should_Be(decimal expected, string response)
		{
			var actual = new BitfinexDataReader().GetCandleFrom(response);
			Assert.Equal(expected, actual.Open);
		}

		[
			Theory(DisplayName = "The Bitfinex Close Value To Response Shoul Be"),
			InlineData(13753, "[[1515696720000,13758,13753,13758,13753,3.26320671]]"),
			InlineData(13753.07, "[[1515696720000,13758.52,13753.07,13758,13753,3.26320671]]"),
		]
		public void The_Bitfinex_Close_Value_To_Response_Should_Be(decimal expected, string response)
		{
			var actual = new BitfinexDataReader().GetCandleFrom(response);
			Assert.Equal(expected, actual.Close);
		}

		[
			Theory(DisplayName = "The Bitfinex High Value To Response Shoul Be"),
			InlineData(13759, "[[1515696720000,13758,13753,13759,13753,3.26320671]]"),
			InlineData(13759.69, "[[1515696720000,13758.52,13753.69,13759.69,13753,3.26320671]]"),
		]
		public void The_Bitfinex_High_Value_To_Response_Should_Be(decimal expected, string response)
		{
			var actual = new BitfinexDataReader().GetCandleFrom(response);
			Assert.Equal(expected, actual.High);
		}

		[
			Theory(DisplayName = "The Bitfinex Low Value To Response Shoul Be"),
			InlineData(13751, "[[1515696720000,13758,13753,13759,13751,3.26320671]]"),
			InlineData(13751.69, "[[1515696720000,13758.52,13759.69,13751.69,13751.69,3.26320671]]"),
		]
		public void The_Bitfinex_Low_Value_To_Response_Should_Be(decimal expected, string response)
		{
			var actual = new BitfinexDataReader().GetCandleFrom(response);
			Assert.Equal(expected, actual.Low);
		}

		[
			Theory(DisplayName = "The Bitfinex Volume Value To Response Shoul Be"),
			InlineData(3, "[[1515696720000,13758,13753,13759,13751,3]]"),
			InlineData(3.26320671, "[[1515696720000,13758.52,13759.69,13751.69,13753,3.26320671]]"),
		]
		public void The_Bitfinex_Volume_Value_To_Response_Should_Be(decimal expected, string response)
		{
			var actual = new BitfinexDataReader().GetCandleFrom(response);
			Assert.Equal(expected, actual.Vol);
		}
	}
}
