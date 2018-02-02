using Domain.Abstractions.Enums;
using Domain.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

			for (int i = 0; i < values.Count;)
			{
				results[i] = altitude;
				var actualValue = values[i];

				for (int j = i + 1; j < i + minLength; j++)
				{
					var value = values[j];
					if ((altitude == Altitude.Top && actualValue < value) || (altitude == Altitude.Bottom && actualValue > value))
					{
						results[j] = altitude;
						results[i] = Altitude.Neutral;
						i = j;
						break;
					}
					else
					{
						if (j + 1 == i + minLength)
						{
							if (altitude == Altitude.Top)
							{
								altitude = Altitude.Bottom;
								minLength = minBottomLength;
							}
							else
							{
								altitude = Altitude.Top;
								minLength = minTopLength;
							}
							i++;
						}
					}
				}
			}
			return results;
		}

	}
}
