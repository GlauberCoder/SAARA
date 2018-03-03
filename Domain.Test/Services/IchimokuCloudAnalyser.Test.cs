using Domain.Abstractions.Entitys;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Entitys;
using Domain.Entitys.AnalisysConfig;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Domain.Test.Services
{
	public class IchimokuCloudAnalyserTest
	{
		decimal[] CloseCandleValues => new decimal[] { 22.27m, 22.19m, 22.08m, 22.17m, 22.18m, 22.13m, 22.23m, 22.43m, 22.24m, 22.29m, 22.15m, 22.39m, 22.38m, 22.61m, 23.36m, 24.05m, 23.75m, 23.83m, 23.95m, 23.63m, 23.82m, 23.87m, 23.65m, 23.19m, 23.10m, 23.33m, 22.68m, 23.10m, 22.40m, 22.17m };

		private decimal[] GetCloseCandleValues(int length)
		{
			return CloseCandleValues.Take(length).ToArray();
		}

		private IchimokuConfig GetConfig(int conversionLine = 0, int baseLine = 0, int leadingSpanB = 0, int lagggingSpan = 0)
		{
			return new IchimokuConfig { ConversionLine = conversionLine, BaseLine = baseLine, LeadingSpanB = leadingSpanB, LaggingSpan = lagggingSpan };
		}
		private CandleAnalyser GetCandleAnalyser(decimal[] closeValueCandle)
		{
			var i = 1;
			var previousLength = closeValueCandle.Count() - 1;
			var previousCandles = closeValueCandle.Take(previousLength).Select(a => new Candle() { ID = i++, Close = a }).ToList<ICandle>();
			return new CandleAnalyser { Previous = previousCandles, Main = new Candle() { Close = closeValueCandle.Last() } };
		}
		private IchimokuAnalyser generateCandleAnalyser(decimal[] closeValueCandle, int conversionLine = 0, int baseLine = 0, int leadingSpanB = 0, int lagggingSpan = 0)
		{
			var config = GetConfig(conversionLine, baseLine, leadingSpanB, lagggingSpan);
			var analyser = GetCandleAnalyser(closeValueCandle);

			return new IchimokuAnalyser(config, analyser);
		}
		private IchimokuAnalyser generateIchimokuCrossover(decimal conversionLine, decimal baseLine, decimal leadingSpanA, decimal leadingSpanB)
		{
			var previous = new List<IIchimokuAnalyser>();
			previous.Add(new IchimokuAnalyser { ConversionLine = baseLine, BaseLine = conversionLine });
			var refenrence = new IchimokuAnalyser { LeadingSpanA = leadingSpanA, LeadingSpanB = leadingSpanB };
			return new IchimokuAnalyser { ConversionLine = conversionLine, BaseLine = baseLine, Previous = previous, Reference = refenrence };
		}

		[
			Theory(DisplayName = "The Conversion Line from Ichimoku Cloud after Calculate method should be"),
			InlineData(22.175, 7, 6, 9, 18, 9),
			InlineData(22.155, 8, 6, 9, 18, 9),
			InlineData(22.255, 9, 6, 9, 18, 9),
			InlineData(22.28, 10, 6, 9, 18, 9),
			InlineData(22.28, 11, 6, 9, 18, 9),
			InlineData(22.28, 12, 6, 9, 18, 9),
			InlineData(22.29, 13, 6, 9, 18, 9),
			InlineData(22.29, 14, 6, 9, 18, 9),
			InlineData(22.38, 15, 6, 9, 18, 9),
			InlineData(22.755, 16, 6, 9, 18, 9),
			InlineData(23.1, 17, 6, 9, 18, 9),
			InlineData(23.215, 18, 6, 9, 18, 9),
			InlineData(23.215, 19, 6, 9, 18, 9),
			InlineData(23.33, 20, 6, 9, 18, 9),
			InlineData(23.705, 21, 6, 9, 18, 9),
			InlineData(23.84, 22, 6, 9, 18, 9),
			InlineData(23.79, 23, 6, 9, 18, 9),
			InlineData(23.79, 24, 6, 9, 18, 9),
			InlineData(23.57, 25, 6, 9, 18, 9),
			InlineData(23.485, 26, 6, 9, 18, 9),
			InlineData(23.485, 27, 6, 9, 18, 9),
			InlineData(23.275, 28, 6, 9, 18, 9),
			InlineData(23.165, 29, 6, 9, 18, 9),
			InlineData(22.865, 30, 6, 9, 18, 9),
		]
		public void The_Conversion_Line_from_Ichimoku_Cloud_after_Calculate_method_should_be(decimal expected, int candleCount, int conversionLine, int baseLine, int leadingSpanB, int lagggingSpan)
		{
			var actual = generateCandleAnalyser(GetCloseCandleValues(candleCount), conversionLine, baseLine, leadingSpanB, lagggingSpan).ConversionLine;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The Base Line from Ichimoku Cloud after Calculate method should be"),
			InlineData(22.255, 10, 6, 9, 18, 9),
			InlineData(22.255, 11, 6, 9, 18, 9),
			InlineData(22.255, 12, 6, 9, 18, 9),
			InlineData(22.28, 13, 6, 9, 18, 9),
			InlineData(22.28, 14, 6, 9, 18, 9),
			InlineData(22.37, 15, 6, 9, 18, 9),
			InlineData(22.755, 16, 6, 9, 18, 9),
			InlineData(23.1, 17, 6, 9, 18, 9),
			InlineData(23.1, 18, 6, 9, 18, 9),
			InlineData(23.1, 19, 6, 9, 18, 9),
			InlineData(23.1, 20, 6, 9, 18, 9),
			InlineData(23.215, 21, 6, 9, 18, 9),
			InlineData(23.215, 22, 6, 9, 18, 9),
			InlineData(23.33, 23, 6, 9, 18, 9),
			InlineData(23.705, 24, 6, 9, 18, 9),
			InlineData(23.62, 25, 6, 9, 18, 9),
			InlineData(23.525, 26, 6, 9, 18, 9),
			InlineData(23.525, 27, 6, 9, 18, 9),
			InlineData(23.315, 28, 6, 9, 18, 9),
			InlineData(23.275, 29, 6, 9, 18, 9),
			InlineData(23.135, 30, 6, 9, 18, 9),
		]
		public void The_Base_Line_from_Ichimoku_Cloud_after_Calculate_method_should_be(decimal expected, int candleCount, int conversionLine, int baseLine, int leadingSpanB, int lagggingSpan)
		{
			var actual = generateCandleAnalyser(GetCloseCandleValues(candleCount), conversionLine, baseLine, leadingSpanB, lagggingSpan).BaseLine;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The Leading Span A from Ichimoku Cloud after Calculate method should be"),
			InlineData(22.2675, 10, 6, 9, 18, 9),
			InlineData(22.2675, 11, 6, 9, 18, 9),
			InlineData(22.2675, 12, 6, 9, 18, 9),
			InlineData(22.285, 13, 6, 9, 18, 9),
			InlineData(22.285, 14, 6, 9, 18, 9),
			InlineData(22.375, 15, 6, 9, 18, 9),
			InlineData(22.755, 16, 6, 9, 18, 9),
			InlineData(23.1, 17, 6, 9, 18, 9),
			InlineData(23.1575, 18, 6, 9, 18, 9),
			InlineData(23.1575, 19, 6, 9, 18, 9),
			InlineData(23.215, 20, 6, 9, 18, 9),
			InlineData(23.46, 21, 6, 9, 18, 9),
			InlineData(23.5275, 22, 6, 9, 18, 9),
			InlineData(23.56, 23, 6, 9, 18, 9),
			InlineData(23.7475, 24, 6, 9, 18, 9),
			InlineData(23.595, 25, 6, 9, 18, 9),
			InlineData(23.505, 26, 6, 9, 18, 9),
			InlineData(23.505, 27, 6, 9, 18, 9),
			InlineData(23.295, 28, 6, 9, 18, 9),
			InlineData(23.22, 29, 6, 9, 18, 9),
			InlineData(23, 30, 6, 9, 18, 9),
		]
		public void The_Leading_Span_A_from_Ichimoku_Cloud_after_Calculate_method_should_be(decimal expected, int candleCount, int conversionLine, int baseLine, int leadingSpanB, int lagggingSpan)
		{
			var actual = generateCandleAnalyser(GetCloseCandleValues(candleCount), conversionLine, baseLine, leadingSpanB, lagggingSpan).LeadingSpanA;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The Leading Span B from Ichimoku Cloud after Calculate method should be"),
			InlineData(23.065, 19, 6, 9, 18, 9),
			InlineData(23.065, 20, 6, 9, 18, 9),
			InlineData(23.065, 21, 6, 9, 18, 9),
			InlineData(23.09, 22, 6, 9, 18, 9),
			InlineData(23.09, 23, 6, 9, 18, 9),
			InlineData(23.09, 24, 6, 9, 18, 9),
			InlineData(23.1, 25, 6, 9, 18, 9),
			InlineData(23.1, 26, 6, 9, 18, 9),
			InlineData(23.1, 27, 6, 9, 18, 9),
			InlineData(23.1, 28, 6, 9, 18, 9),
			InlineData(23.1, 29, 6, 9, 18, 9),
			InlineData(23.215, 30, 6, 9, 18, 9),
		]
		public void The_Leading_Span_B_from_Ichimoku_Cloud_after_Calculate_method_should_be(decimal expected, int candleCount, int conversionLine, int baseLine, int leadingSpanB, int lagggingSpan)
		{
			var actual = generateCandleAnalyser(GetCloseCandleValues(candleCount), conversionLine, baseLine, leadingSpanB, lagggingSpan).LeadingSpanB;
			Assert.Equal(expected, actual);
		}


		[
			Theory(DisplayName = "The Conversion Base Crossover from Ichimoku Cloud should return the correct trade signal"),
			InlineData(TradeSignal.StrongLong, 19, 17, 9, 16),
			InlineData(TradeSignal.WeakLong, 19, 17, 20, 24),
			InlineData(TradeSignal.Hold, 19, 17, 15, 20),
			InlineData(TradeSignal.WeakShort, 17, 19, 9, 16),
			InlineData(TradeSignal.StrongShort, 17, 19, 20, 24),
			InlineData(TradeSignal.Hold, 17, 19, 15, 20),
		]
		public void The_Conversion_Base_Crossover_from_Ichimoku_Cloud_should_return_the_correct_trade_signal(TradeSignal expected, decimal conversionLine, decimal baseLine, decimal leadingSpanA, decimal leadingSpanB)
		{
			var actual = generateIchimokuCrossover(conversionLine, baseLine, leadingSpanA, leadingSpanB).CalculateConversionBaseCrossover();
			Assert.Equal(expected, actual);
		}
	}
}
