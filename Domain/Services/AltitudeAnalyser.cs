using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Extensions;

namespace Domain.Services
{
	public class AltitudeAnalyser : IAltitudeAnalyser
	{
		decimal minTopIdentifier { get; set; }
		decimal minBottomIdentifier { get; set; }
		bool byVariation { get; set; }
		
		public virtual IAltitudeAnalyser ByLength(int minTopLength, int minBottomLength)
		{
			byVariation = false;
			minTopIdentifier = minTopLength;
			minBottomIdentifier = minBottomLength;

			return this;
		}
		public virtual IAltitudeAnalyser ByVariation(decimal minTopVariation, decimal minBottomVariation)
		{
			byVariation = true;
			minTopIdentifier = minTopVariation;
			minBottomIdentifier = minBottomVariation;

			return this;
		}
		public virtual IList<Altitude> Identify(IList<decimal> values)
		{
			return byVariation ? IdentifyByVariation(values, minTopIdentifier, minBottomIdentifier) : IdentifyByLength(values, (int)minTopIdentifier, (int)minBottomIdentifier);
		}
		private IList<Altitude> IdentifyByVariation(IList<decimal> values, decimal minTopVariation, decimal minBottomVariation)
		{
			var results = values.Select(v => Altitude.Neutral).ToList();
			var reference = values.First();
			var (altitude, minVariation) = (Altitude.Top, minTopVariation);
			var previousAltitude = Altitude.Neutral;

			for (var i = 1; i < values.Count; i++)
			{
				altitude = AltitudeFrom(values[i], reference, minTopVariation, minBottomVariation);
				results[i - 1] = altitude != previousAltitude ? previousAltitude : Altitude.Neutral;
				previousAltitude = altitude;
			}
			return results;
		}
		public Altitude AltitudeFrom(decimal value, decimal reference, decimal minTopVariation, decimal minBottomVariation)
		{
			var topReference = (1 + minTopVariation) * reference;
			var bottomReference = (1 - minBottomVariation) * reference;

			if (value >= topReference)
				return Altitude.Top;

			if (value <= bottomReference)
				return Altitude.Bottom;

			return Altitude.Neutral;
		}
		private IList<Altitude> IdentifyByLength(IList<decimal> values, int minTopLength, int minBottomLength)
		{
			var results = values.Select(v => Altitude.Neutral).ToList();
			var (altitude, minLength) = (Altitude.Top, minTopLength);

			for (var i = 0; i < values.Count - minLength; )
			{
				var index = i + RelativeIndexFrom( values.TakeFrom(i, minLength + 1), altitude );
				if (index == i)
				{
					results[i] = altitude;
					(altitude, minLength) = SwitchAltitude(altitude, minTopLength, minBottomLength);
					i++;
				}
				else
					i = index;
			}
			return results;
		}
		public int RelativeIndexFrom(IList<decimal> values, Altitude altitude)
		{
			var reference = values.First();
			foreach (var value in values.Skip(1))
				if ((altitude == Altitude.Top && value > reference) || (altitude == Altitude.Bottom && value < reference))
					return values.IndexOf(value);
			return 0;
		}
		public (Altitude altitude, T minLength) SwitchAltitude<T>(Altitude altitude, T minTopLength, T minBottomLength)
		{
			if (altitude != Altitude.Neutral)
				return (altitude == Altitude.Top) ? (altitude: Altitude.Bottom, minLength: minBottomLength) : (altitude: Altitude.Top, minLength: minTopLength);
			return (altitude : Altitude.Neutral, minLength : default(T));
		}
		public IList<Altitude> Congruence(IList<Altitude> values, IList<Altitude> otherValues)
		{
			var altitudeCongruence = new List<Altitude>();
			PrependToEqualizeLength(ref values, ref otherValues, Altitude.Neutral);

			for (int i = 0; i < values.Count; i++)
				altitudeCongruence.Add((values[i] == otherValues[i]) ? values[i] : Altitude.Neutral);

			return altitudeCongruence;
		}
		public void PrependToEqualizeLength(ref IList<Altitude> values, ref IList<Altitude> otherValues, Altitude altitude)
		{
			var difference = Math.Abs(values.Count - otherValues.Count);

			if (values.Count > otherValues.Count)
				otherValues = PrependAltitudeValues(otherValues, altitude, difference);
			if (values.Count < otherValues.Count)
				values = PrependAltitudeValues(values, altitude, difference);
		}
		public IList<Altitude> PrependAltitudeValues(IList<Altitude> values, Altitude altitude, int count)
		{
			var altitudes = values.ToList<Altitude>();
			for (int i = 0; i < count; i++)
				altitudes.Insert(0, altitude);
			return altitudes;
		}
		public IList<int> IndexesFrom(IList<Altitude> congruentValues, Altitude altitude)
		{
			var indexes = new List<int>();
			for (int i = 0; i < congruentValues.Count; i++)
				if (congruentValues[i] == altitude)
					indexes.Add(i);
			return indexes;
		}
	}
}
