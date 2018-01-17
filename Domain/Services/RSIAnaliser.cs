using Domain.Abstractions;
using Domain.Abstractions.Entitys;
using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using Domain.Entitys;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
	public class RSIAnaliser : IRSIAnaliser
	{
		public virtual decimal RSI { get; private set; }
		public virtual PriceSignal PriceSignal => throw new NotImplementedException();
		public virtual Trend RSIRange => throw new NotImplementedException();
		public virtual Trend Divergence => throw new NotImplementedException();
		public virtual Trend FailSwing => throw new NotImplementedException();
		public virtual Trend PositiveNegativeReversal => throw new NotImplementedException();

		public virtual IRSIAnaliser Calculate(IRSIConfig config, ICandleAnalyser analysis)
		{
			RSI = CalculateRSI(config, analysis.Previous);
			//TODO: Calcular PriceSignal
			//TODO: Calcular RSIRange
			//TODO: Calcular Divergence
			//TODO: Calcular FailSwing
			//TODO: Calcular PositiveNegativeReversal

			return this;
		}

		private decimal CalculateRSI(IRSIConfig config, IList<ICandle> previousCandles)
		{
			var differences = CalculateDifferences(previousCandles, config.Length);
			var gainAvg = CalculateAVG(differences, d => d > 0, config.Length);
			var lossAvg = CalculateAVG(differences, d => d < 0, config.Length);

			return lossAvg == 0 ? 100 : CalculateRSI(gainAvg, lossAvg);

		}

		private decimal CalculateRSI(decimal gainAvg, decimal lossAvg)
		{
			var rs = gainAvg / lossAvg;
			return 100 - (100 / (1 + rs));
		}

		private IList<decimal> CalculateDifferences(IList<ICandle> previousCandles, int length)
		{
			var numberOfCandles = (length * 2) + 1;
			var workCandles = previousCandles.TakeLast(numberOfCandles);
			var lastValue = previousCandles.First();
			var differences = new List<decimal>();

			foreach (var value in previousCandles.Skip(1))
				differences.Add(value.Close - lastValue.Close);

			return differences;
		}



		private decimal CalculateAVG(IList<decimal> values, Func<decimal, bool> filter, int length)
		{
			var workValues = values.Skip(length);
			var fistAvarage = values.Take(length).Where(filter).Average();
			var lastAvg = CalculateAVG(fistAvarage, workValues.First(), length);

			foreach (var value in workValues.Skip(1))
				lastAvg = CalculateAVG(lastAvg, filter(value) ? value : 0, length);

			return Math.Abs(lastAvg);

		}

		private decimal CalculateAVG(decimal previousAvg, decimal currentValue, int length)
		{
			return ((previousAvg * (length - 1)) + currentValue) / length;
		}

	}
}
