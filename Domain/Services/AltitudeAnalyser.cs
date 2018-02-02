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
		public virtual IAltitudeAnalyser ByVariation(decimal topMinVariation, decimal bottomMinVariation)
		{
			byVariation = true;
			minTopIdentifier = topMinVariation;
			minBottomIdentifier = bottomMinVariation;

			return this;
		}
		public virtual IList<Altitude> Identify(IList<decimal> values)
		{
			return byVariation ? IdentifyByVariation(values, minTopIdentifier, minBottomIdentifier) : IdentifyByLength(values, (int)minTopIdentifier, (int)minBottomIdentifier);
		}
		private IList<Altitude> IdentifyByVariation(IList<decimal> values, decimal topMinVariation, decimal bottomMinVariation)
		{
			throw new NotImplementedException();
		}

		private IList<Altitude> IdentifyByLength(IList<decimal> values, int minTopLength, int minBottomLength)
		{
			var results = values.Select(v => Altitude.Neutral).ToList();
			var altitude = Altitude.Top;
			var minLength = minTopLength;

			for (var i = 0; i < values.Count - minLength; )
			{
				results[i] = altitude;

				minLength = (altitude == Altitude.Top) ? minTopLength : minBottomLength;
				var index = i + RelativeIndexFrom( values.TakeFrom(i, minLength + 1), altitude );

				if (index == i)
				{
					altitude = (altitude == Altitude.Top) ? Altitude.Bottom : Altitude.Top;
					i++;
				}
				else
				{
					while (i < index)
					{
						results[i] = Altitude.Neutral;
						i++;
					}
				}
					
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
	}
}
