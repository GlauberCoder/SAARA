﻿using Domain.Abstractions.Enums;
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
		AltitudeAnalyserTechnic Technic { get; set; }
		
		public virtual IAltitudeAnalyser ByLength(int minTopLength, int minBottomLength)
		{
			Technic = AltitudeAnalyserTechnic.Length;
			minTopIdentifier = minTopLength;
			minBottomIdentifier = minBottomLength;

			return this;
		}
		public virtual IAltitudeAnalyser ByVariation(decimal minTopVariation, decimal minBottomVariation)
		{
			Technic = AltitudeAnalyserTechnic.Variation;
			minTopIdentifier = minTopVariation;
			minBottomIdentifier = minBottomVariation;

			return this;
		}
		public virtual IList<Altitude> Identify(IList<decimal> values)
		{
			return Technic == AltitudeAnalyserTechnic.Variation ? IdentifyByVariation(values, minTopIdentifier, minBottomIdentifier) : IdentifyByLength(values, (int)minTopIdentifier, (int)minBottomIdentifier);
		}
		private IList<Altitude> IdentifyByVariation(IList<decimal> values, decimal minTopVariation, decimal minBottomVariation)
		{
			var results = values.Select(v => Altitude.Neutral).ToList();
			var indexReference = 0;
			var indexCandidate = 0;
			var altitude = Altitude.Top;

			for (var i = 1; i < values.Count; i++)
			{
				var j = i;
				while (FitsAltitude(values[i], values[i - 1], altitude) && i < values.Count)
					i++;
				indexCandidate = i - 1;
				results[i - 1] = altitude;

				while(i < values.Count - 1)
				{
					if (FitsAltitude(values[i], values[indexCandidate], altitude))
					{
						results[indexCandidate] = Altitude.Neutral;
						i--;
						break;
					}
					else if(AltitudeFrom(values[i], values[indexReference], minTopVariation, minBottomVariation) == OppositeOf(altitude))
					{
						altitude = OppositeOf(altitude);
						indexReference = i;
						i--;
						break;
					}
					i++;
				}
			}
			return results;
		}
		public Altitude OppositeOf(Altitude altitude)
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
			var altitude = Altitude.Top;
			var minLength = minTopLength;

			for (var i = 0; i < values.Count - minLength; )
			{
				var index = RelativeIndexFrom(values, altitude, i, minLength);
				if (index == i)
				{
					results[i] = altitude;
					(altitude, minLength) = SwitchAltitude(altitude, minLength, minTopLength, minBottomLength);
					i++;
				}
				else
					i = index;
			}
			return results;
		}
		public int RelativeIndexFrom(IList<decimal> values, Altitude altitude, int index, int length)
		{
			var nextValues = values.SkipAndTake(index + 1, length);
			var reference = values[index];
			foreach (var value in nextValues)
				if (FitsAltitude(value, reference, altitude))
					return (index + nextValues.IndexOf(value) + 1);
			return index;
		}
		public bool FitsAltitude(decimal value, decimal reference, Altitude altitude )
		{
			return (altitude == Altitude.Top && value > reference) || (altitude == Altitude.Bottom && value < reference);
		}
		public (Altitude altitude, T minLength) SwitchAltitude<T>(Altitude altitude, T lastMinLength, T minTopLength, T minBottomLength)
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
		public decimal? GetLast(IList<decimal> values, IList<Altitude> altitudes, Altitude altitude)
		{
			if (values.Count != altitudes.Count)
				return null;
			for (int i = altitudes.Count - 1; i >= 0; i--)
				if (altitudes[i] == altitude)
					return values[i];
			return null;
		}
		public decimal? GetFirst(IList<decimal> values, IList<Altitude> altitudes, Altitude altitude)
		{
			if (values.Count != altitudes.Count)
				return null;
			for (int i = 0; i < altitudes.Count; i++)
				if (altitudes[i] == altitude)
					return values[i];
			return null;
		}
		public int? GetIndexOfHighest(IList<decimal> values, IList<Altitude> altitudes, Altitude altitude)
		{
			var indexes = IndexesFrom(altitudes, altitude);

			if (values.Count != altitudes.Count || indexes.Count == 0)
				return null;

			var higherIndex = indexes.First();
			foreach (var i in indexes)
				if (values[i] > values[higherIndex])
					higherIndex = i;
			return higherIndex;
		}
		public int? GetIndexOfLowest(IList<decimal> values, IList<Altitude> altitudes, Altitude altitude)
		{
			var indexes = IndexesFrom(altitudes, altitude);

			if (values.Count != altitudes.Count || indexes.Count == 0)
				return null;

			var lowerIndex = indexes.First();
			foreach(var i in indexes)
				if (values[i] < values[lowerIndex])
					lowerIndex = i;
			return lowerIndex;
		}
	}
}
