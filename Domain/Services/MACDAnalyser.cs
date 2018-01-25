using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Services;
using Domain.Extensions;
using System.Collections.Generic;
using System.Linq;
using Util.Extensions;

namespace Domain.Services
{
	public class MACDAnalyser : IMACDAnalyser
	{
		public virtual decimal LongEMA { get; set; }
		public virtual decimal ShortEMA { get; set; }
		public virtual decimal MACD { get; set; }
		public virtual decimal Signal { get; set; }
		public virtual decimal Histogram { get; set; }

		public MACDAnalyser()
		{

		}
		public MACDAnalyser(IMACDConfig config, ICandleAnalyser analysis) : this()
		{
			Calculate(config, analysis);
		}
		public MACDAnalyser(IMACDConfig config, ICandleAnalyser analysis, ICandle candle) : this()
		{
			Calculate(config, analysis, candle);
		}
		public virtual IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis)
		{
			return Calculate(config, analysis, analysis.Main);
		}
		public virtual IMACDAnalyser Calculate(IMACDConfig config, ICandleAnalyser analysis, ICandle candle)
		{
			calculateMACD(config, analysis);

			calculateSignal(config, analysis, candle);

			Histogram = MACD - Signal;

			return this;
		}

		private IMACDAnalyser calculateMACD(IMACDConfig config, ICandleAnalyser analysis)
		{
			LongEMA = analysis.EMA(config.LongEMA);
			ShortEMA = analysis.EMA(config.ShortEMA);
			MACD = ShortEMA - LongEMA;

			return this;
		}

		private IMACDAnalyser calculateSignal(IMACDConfig config, ICandleAnalyser analysis, ICandle candle)
		{
			var MACDsList = new List<decimal>();

			var candles = analysis.Previous;

			var length = candles.Count - config.LongEMA;
			var previous = candles.TakePrevious(candle, length);
			previous.Add(candle);


			foreach (ICandle c in previous)
			{
				var candleAnalyser = new CandleAnalyser { Previous = candles.TakeAllPrevious(c) };
				MACDsList.Add( new MACDAnalyser().calculateMACD(config, candleAnalyser).MACD );
			}

			Signal = MACDsList.EMA(config.SignalEMA);

			return this;
		}

	}
}
