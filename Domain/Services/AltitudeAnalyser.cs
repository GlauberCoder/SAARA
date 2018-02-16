using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using System.Collections.Generic;
using System.Linq;
using Util.Extensions;

namespace Domain.Services
{
	public class AltitudeAnalyser<T> : IAltitudeAnalyser<T>, IAltitudeAnalyserConfigured<T>
		where T : ICanBeClassifiedByAltitude
	{
		public decimal MinTopIdentifier { get; set; }
		public decimal MinBottomIdentifier { get; set; }
		AltitudeAnalyserMode Mode { get; set; }
		
		public virtual IAltitudeAnalyserConfigured<T> ByLength(int minTopLength, int minBottomLength)
		{
			Mode = AltitudeAnalyserMode.Length;
			MinTopIdentifier = minTopLength;
			MinBottomIdentifier = minBottomLength;

			return this;
		}
		public virtual IAltitudeAnalyserConfigured<T> ByVariation(decimal minTopVariation, decimal minBottomVariation)
		{
			Mode = AltitudeAnalyserMode.Variation;
			MinTopIdentifier = minTopVariation;
			MinBottomIdentifier = minBottomVariation;

			return this;
		}
		public virtual IAltitudeAnalyserConfigured<T> Configure(IAltitudeAnalyserConfig config)
		{
			return config.Mode == AltitudeAnalyserMode.Length ? ByLength((int)config.MinTop, (int)config.MinBottom) : ByVariation(config.MinTop, config.MinBottom);
		}
		
		public virtual IList<T> Identify(IList<T> values)
		{
			return Mode == AltitudeAnalyserMode.Variation ? IdentifyByVariation(values, MinTopIdentifier, MinBottomIdentifier) : IdentifyByLength(values, (int)MinTopIdentifier, (int)MinBottomIdentifier);
		}
		private IList<T> IdentifyByVariation(IList<T> values, decimal minTopVariation, decimal minBottomVariation)
		{
			var lastValue = values.FirstOrDefault();
			lastValue?.ClassifyByAltitude(Altitude.Top);
			foreach (var value in values)
			{
				var lastAltitude = lastValue.Altitude;
				var actualValue = value.ValueForAltitude() * (int)lastAltitude;
				var previousValue = lastValue.ValueForAltitude() * (int)lastAltitude;
				var (tolerance, inverseAltitude) = lastAltitude == Altitude.Top ? (minTopVariation, Altitude.Bottom) : (minBottomVariation, Altitude.Top); 
				if (actualValue > previousValue)
				{
					lastValue.ClassifyByAltitude(Altitude.Neutral);
					value.ClassifyByAltitude(lastAltitude);
					lastValue = value;
				}
				else
				{
					var variation = lastValue.ValueForAltitude().AbsolutePercentageOfChange(value.ValueForAltitude());
					if (actualValue < previousValue && variation >= tolerance)
					{
						value.ClassifyByAltitude(inverseAltitude);
						lastValue = value;
					}
				}
			}
			return values;
		}
		private IList<T> IdentifyByLength(IList<T> values, int minTopLength, int minBottomLength)
		{
			var altitude = Altitude.Top;
			var minLength = minTopLength;

			for (var i = 0; i < values.Count; )
			{
				var index = RelativeIndexFrom(values, altitude, i, minLength);
				if (index == i)
				{
					values[i].ClassifyByAltitude(altitude);
					(altitude, minLength) = SwitchAltitude(altitude, minLength, minTopLength, minBottomLength);
					i++;
				}
				else
					i = index;
			}
			return values;
		}
		public virtual int RelativeIndexFrom(IList<T> values, Altitude altitude, int index, int length)
		{
			var nextValues = values.SkipAndTake(index + 1, length);
			var reference = values[index].ValueForAltitude();
			foreach (var value in nextValues)
				if (FitsAltitude(value.ValueForAltitude(), reference, altitude))
					return (index + nextValues.IndexOf(value) + 1);
			return index;
		}
		public virtual bool FitsAltitude(decimal value, decimal reference, Altitude altitude )
		{
			return (altitude == Altitude.Top && value > reference) || (altitude == Altitude.Bottom && value < reference);
		}
		public virtual (Altitude altitude, D minLength) SwitchAltitude<D>(Altitude altitude, D lastMinLength, D minTopLength, D minBottomLength)
		{
			if (altitude != Altitude.Neutral)
				return (altitude == Altitude.Top) ? (altitude: Altitude.Bottom, minLength: minBottomLength) : (altitude: Altitude.Top, minLength: minTopLength);
			return (altitude : Altitude.Neutral, minLength : lastMinLength);
		}
		public IList<int> IndexesFrom(IList<Altitude> values, Altitude altitude)
		{
			var indexes = new List<int>();
			for (int i = 0; i < values.Count; i++)
				if (values[i] == altitude)
					indexes.Add(i);
			return indexes;
		}
	}
}
