using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Entitys;
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
		string json => "{\"message\":null,\"data\":{\"pagination\":{\"total_pages\":25,\"current_page\":1,\"page_size\":20,\"registers_count\":489},\"trades\":[{\"type\":\"buy\",\"amount\":0.00712485,\"unit_price\":33800,\"active_order_code\":\"SkGXWl6Vf\",\"passive_order_code\":\"H1hW_g64z\",\"date\":\"2018-01-17T15:51:54.093Z\"},{\"type\":\"buy\",\"amount\":0.14806124,\"unit_price\":33800,\"active_order_code\":\"rySMZgpVf\",\"passive_order_code\":\"H1hW_g64z\",\"date\":\"2018-01-17T15:51:40.733Z\"},{\"type\":\"buy\",\"amount\":0.01222473,\"unit_price\":34000,\"active_order_code\":\"r1Bgbx6VG\",\"passive_order_code\":\"rJmq51aNG\",\"date\":\"2018-01-17T15:51:09.077Z\"},{\"type\":\"buy\",\"amount\":0.0190441,\"unit_price\":33800,\"active_order_code\":\"r1Bgbx6VG\",\"passive_order_code\":\"rJTeoJTVz\",\"date\":\"2018-01-17T15:51:09.073Z\"},{\"type\":\"buy\",\"amount\":0.00535355,\"unit_price\":33500,\"active_order_code\":\"r1OTgeTVz\",\"passive_order_code\":\"r1PvgxTNz\",\"date\":\"2018-01-17T15:50:23.850Z\"},{\"type\":\"buy\",\"amount\":0.99785931,\"unit_price\":33500,\"active_order_code\":\"r1OTgeTVz\",\"passive_order_code\":\"S195jy6Ez\",\"date\":\"2018-01-17T15:50:23.843Z\"},{\"type\":\"buy\",\"amount\":0.0301515,\"unit_price\":33499.99,\"active_order_code\":\"r1OTgeTVz\",\"passive_order_code\":\"B1rIkg6Ez\",\"date\":\"2018-01-17T15:50:23.837Z\"},{\"type\":\"buy\",\"amount\":0.00607114,\"unit_price\":33499,\"active_order_code\":\"r1OTgeTVz\",\"passive_order_code\":\"SkbU2JTVz\",\"date\":\"2018-01-17T15:50:23.830Z\"},{\"type\":\"buy\",\"amount\":0.00113835,\"unit_price\":33489.99,\"active_order_code\":\"r1OTgeTVz\",\"passive_order_code\":\"Bk2ylxTVG\",\"date\":\"2018-01-17T15:50:23.830Z\"},{\"type\":\"buy\",\"amount\":0.00381597,\"unit_price\":33400,\"active_order_code\":\"r1OTgeTVz\",\"passive_order_code\":\"BJaW0y6Vf\",\"date\":\"2018-01-17T15:50:23.823Z\"},{\"type\":\"buy\",\"amount\":0.00988335,\"unit_price\":33399,\"active_order_code\":\"r1OTgeTVz\",\"passive_order_code\":\"SJP2xe6VM\",\"date\":\"2018-01-17T15:50:23.823Z\"},{\"type\":\"buy\",\"amount\":0.02878941,\"unit_price\":33390,\"active_order_code\":\"r1OTgeTVz\",\"passive_order_code\":\"S1PRklT4z\",\"date\":\"2018-01-17T15:50:23.820Z\"},{\"type\":\"sell\",\"amount\":0.05756074,\"unit_price\":33000.01,\"active_order_code\":\"BkwaggTVz\",\"passive_order_code\":\"HJT8xg6Vf\",\"date\":\"2018-01-17T15:50:23.267Z\"},{\"type\":\"sell\",\"amount\":0.13124031,\"unit_price\":33000.99,\"active_order_code\":\"BkwaggTVz\",\"passive_order_code\":\"rJzsel64z\",\"date\":\"2018-01-17T15:50:23.263Z\"},{\"type\":\"sell\",\"amount\":0.01131191,\"unit_price\":33001,\"active_order_code\":\"BkwaggTVz\",\"passive_order_code\":\"rycqeea4G\",\"date\":\"2018-01-17T15:50:23.260Z\"},{\"type\":\"sell\",\"amount\":0.01060542,\"unit_price\":33002.01,\"active_order_code\":\"BkwaggTVz\",\"passive_order_code\":\"SkzhgxT4M\",\"date\":\"2018-01-17T15:50:23.247Z\"},{\"type\":\"sell\",\"amount\":0.04460498,\"unit_price\":33280,\"active_order_code\":\"BkwaggTVz\",\"passive_order_code\":\"BkMnxep4G\",\"date\":\"2018-01-17T15:50:23.223Z\"},{\"type\":\"buy\",\"amount\":0.00116185,\"unit_price\":33200,\"active_order_code\":\"BkMnxep4G\",\"passive_order_code\":\"HJ1sgx6NM\",\"date\":\"2018-01-17T15:50:01.963Z\"},{\"type\":\"sell\",\"amount\":0.00334855,\"unit_price\":33001,\"active_order_code\":\"B1yiglaNG\",\"passive_order_code\":\"rycqeea4G\",\"date\":\"2018-01-17T15:49:43.223Z\"},{\"type\":\"sell\",\"amount\":0.01818087,\"unit_price\":33001.73,\"active_order_code\":\"B1yiglaNG\",\"passive_order_code\":\"SkQ5eep4z\",\"date\":\"2018-01-17T15:49:43.220Z\"}]}}";
		DateTime dateTime => new DateTime(2017, 12, 12, 10, 10, 10);
		[
			Fact(DisplayName = "The BitcoinTrade template URL should be correct")
		]
		public void The_BitcoinTradeTemplate_Url_Should_Be_Correct()
		{
			var excpected = "https://api.bitcointrade.com.br/v1/public/{{symbol}}/trades?start_time={{startTime}}&end_time={{endTime}}&page_size={{pageSize}}&current_page={{currentPage}}";
			var actual = new BitcoinTradeDataReader().TemplateURL;
			Assert.Equal(excpected, actual);
		}

		[
			Theory(DisplayName = "The BitcoinTrade URL should be"),
			

			InlineData("BTC", CandleTimespan.OneMinute, "https://api.bitcointrade.com.br/v1/public/BTC/trades?start_time=2017-12-12T10:09:00-03:00&end_time=2017-12-12T10:00:00-03:00&page_size=200&current_page=1"),
			InlineData("BTC", CandleTimespan.FiveMinutes, "https://api.bitcointrade.com.br/v1/public/BTC/trades?start_time=2017-12-12T10:05:00-03:00&end_time=2017-12-12T10:00:00-03:00&page_size=200&current_page=1"),
			InlineData("BTC", CandleTimespan.FifteenMinutes, "https://api.bitcointrade.com.br/v1/public/BTC/trades?start_time=2017-12-12T09:45:00-03:00&end_time=2017-12T10:00:00-03:00&page_size=200&current_page=1"),
			InlineData("BTC", CandleTimespan.ThirtyMinutes, "https://api.bitcointrade.com.br/v1/public/BTC/trades?start_time=2017-12-12T09:30:00-03:00&end_time=2017-12T10:00:00-03:00&page_size=200&current_page=1"),
			InlineData("BTC", CandleTimespan.OneHour, "https://api.bitcointrade.com.br/v1/public/BTC/trades?start_time=2017-12-12T09:00:00-03:00&end_time=2017-12-12T10:00:00-03:00&page_size=200&current_page=1"),
		]
		public void The_BitcoinTrade_URL_Should_Be(string symbolName, CandleTimespan timespan, string expected)
		{//TODO: olhar o tempo para tras, ex 10 timespan 15 -> 45-00 e nao 0-15.
			var actual = new BitcoinTradeDataReader().GetUrlFrom(new Symbol() { Name = symbolName }, timespan, dateTime);
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The BitcoinTrade Open Value To Response Shoul Be"),

			InlineData(33001.73)
		]
		public void The_BitcoinTrade_Open_Value_To_Response_Should_Be(decimal expected)
		{
			var actual = new BitcoinTradeDataReader().GetCandleFrom(json);;
			Assert.Equal(expected, actual.Open);
		}

		[
			Theory(DisplayName = "The BitcoinTrade Close Value To Response Shoul Be"),

			InlineData(33800)
		]
		public void The_BitcoinTrade_Close_Value_To_Response_Should_Be(decimal expected)
		{
			var actual = new BitcoinTradeDataReader().GetCandleFrom(json);;
			Assert.Equal(expected, actual.Close);
		}

		[
			Theory(DisplayName = "The BitcoinTrade High Value To Response Shoul Be"),

			InlineData(34000)
		]
		public void The_BitcoinTrade_High_Value_To_Response_Should_Be(decimal expected)
		{
			var actual = new BitcoinTradeDataReader().GetCandleFrom(json);;
			Assert.Equal(expected, actual.High);
		}


		[
			Theory(DisplayName = "The BitcoinTrade Low Value To Response Shoul Be"),

			InlineData(33000.01)
		]
		public void The_BitcoinTrade_Low_Value_To_Response_Should_Be(decimal expected)
		{
			var actual = new BitcoinTradeDataReader().GetCandleFrom(json);;
			Assert.Equal(expected, actual.Low);
		}

		[
			Theory(DisplayName = "The BitcoinTrade Vol Value To Response Shoul Be"),

			InlineData(1.54753213)
		]
		public void The_BitcoinTrade_Vol_Value_To_Response_Should_Be(decimal expected)
		{
			var actual = new BitcoinTradeDataReader().GetCandleFrom(json);;
			Assert.Equal(expected, actual.Vol);
		}

	}
}
