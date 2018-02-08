using Domain.Abstractions.Entitys.AnalisysConfig;
using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Extensions;

namespace Domain.Services
{
	public class AltitudeAnalyser<T> : IAltitudeAnalyser<T>, IAltitudeAnalyserConfigured<T>
		where T : ICanBeClassifiedByAltitude
	{
		decimal minTopIdentifier { get; set; }
		decimal minBottomIdentifier { get; set; }
		AltitudeAnalyserMode Mode { get; set; }
		
		public virtual IAltitudeAnalyserConfigured<T> ByLength(int minTopLength, int minBottomLength)
		{
			Mode = AltitudeAnalyserMode.Length;
			minTopIdentifier = minTopLength;
			minBottomIdentifier = minBottomLength;

			return this;
		}
		public virtual IAltitudeAnalyserConfigured<T> ByVariation(decimal minTopVariation, decimal minBottomVariation)
		{
			Mode = AltitudeAnalyserMode.Variation;
			minTopIdentifier = minTopVariation;
			minBottomIdentifier = minBottomVariation;

			return this;
		}
		public virtual IAltitudeAnalyserConfigured<T> Configure(IAltitudeAnalyserConfig config)
		{//TODO: testar cast int -> decimal
			return config.Mode == AltitudeAnalyserMode.Length ? ByLength((int)config.MinTop, (int)config.MinBottom) : ByVariation(config.MinTop, config.MinBottom);
		}
		
		public virtual IList<T> Identify(IList<T> values)
		{
			return Mode == AltitudeAnalyserMode.Variation ? IdentifyByVariation(values, minTopIdentifier, minBottomIdentifier) : IdentifyByLength(values, (int)minTopIdentifier, (int)minBottomIdentifier);
		}
		private IList<T> IdentifyByVariation(IList<T> values, decimal minTopVariation, decimal minBottomVariation)
		{
			var indexReference = 0;
			var indexCandidate = 0;
			var altitude = Altitude.Top;

			for (var i = 1; i < values.Count; i++)
			{
				var j = i;
				while (FitsAltitude(values[i].ValueForAltitude(), values[i - 1].ValueForAltitude(), altitude) && i < values.Count)
					i++;
				indexCandidate = i - 1;
				values[i - 1].ClassifyByAltitude(altitude);

				while(i < values.Count - 1)
				{
					if (FitsAltitude(values[i].ValueForAltitude(), values[indexCandidate].ValueForAltitude(), altitude))
					{
						values[indexCandidate].ClassifyByAltitude(Altitude.Neutral);
						i--;
						break;
					}
					else if(AltitudeFrom(values[i].ValueForAltitude(), values[indexReference].ValueForAltitude(), minTopVariation, minBottomVariation) == OppositeOf(altitude))
					{
						altitude = OppositeOf(altitude);
						indexReference = i;
						i--;
						break;
					}
					i++;
				}
			}
			return values;
		}
		public virtual Altitude OppositeOf(Altitude altitude)
		{
			switch (altitude)	
			{
				case Altitude.Top:
					return Altitude.Bottom;
				case Altitude.Bottom:
					return Altitude.Top;
				default:
					return Altitude.Neutral;
			}
		}
		public virtual Altitude AltitudeFrom(decimal value, decimal reference, decimal minTopVariation, decimal minBottomVariation)
		{
			var topReference = (1 + minTopVariation) * reference;
			var bottomReference = (1 - minBottomVariation) * reference;

			if (value >= topReference)
				return Altitude.Top;

			if (value <= bottomReference)
				return Altitude.Bottom;

			return Altitude.Neutral;
		}
		private IList<T> IdentifyByLength(IList<T> values, int minTopLength, int minBottomLength)
		{
			var altitude = Altitude.Top;
			var minLength = minTopLength;

			for (var i = 0; i < values.Count - minLength; )
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
