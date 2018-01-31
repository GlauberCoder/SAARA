using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Util.Extensions
{
	public static class NumberExtensions
	{

		public static decimal DecimalPart(this decimal number)
		{
			if (number.IntegerPart() - number == 0)
				return 0;

			return int.Parse(number.ToString().Split(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0])[1]);
		}
		public static string AlfabeticRepresentation(this int number)
		{
			if (number == 0)
				return "";
			number--;
			return (number / 26).AlfabeticRepresentation() + (char)('A' + number % 26);
		}
		public static decimal IntegerPart(this decimal number)
		{
			return Convert.ToDecimal(Math.Truncate(number));
		}

		public static decimal Pow(this decimal x, decimal y)
		{
			return Convert.ToDecimal(Math.Pow(Convert.ToDouble(x), Convert.ToDouble(y)));
		}

		public static long NumeralsCount(this int number)
		{
			return number.ToString().ReplaceAllNumberSeparators().Length;
		}

		public static long NumeralsCount(this long number)
		{
			return number.ToString().ReplaceAllNumberSeparators().Length;
		}

		public static long NumeralsCount(this decimal number)
		{
			return number.ToString().ReplaceAllNumberSeparators().Length;
		}

		public static string ReplaceAllNumberSeparators(this string text)
		{
			var format = CultureInfo.CurrentCulture.NumberFormat;
			foreach (var separador in new List<string> { format.CurrencyDecimalSeparator, format.CurrencyGroupSeparator, format.NumberDecimalSeparator, format.NumberGroupSeparator, format.PercentDecimalSeparator, format.PercentGroupSeparator })
				text = text.Replace(separador, string.Empty);

			return text;
		}

		public static string ToString(this decimal? number, string format)
		{
			return number.HasValue ? number.Value.ToString(format) : string.Empty;
		}

		public static decimal ToPrecision(this decimal number, int precision)
		{
			return Math.Round(number, precision);
		}

		public static double ToPrecision(this double number, int precision)
		{
			return Math.Round(number, precision);
		}

		public static string GetCurrency()
		{
			return "R$";
		}

		public static string ToMonetary(this decimal? number)
		{
			return number.HasValue ? number.Value.ToMonetary() : "-";
		}

		public static string ToMonetary(this decimal number)
		{
			return $"{GetCurrency()} {number.MonetaryFormat()}";
		}
		public static string ToPercent(this decimal? number, int precision = 0)
		{
			return number.HasValue ? number.Value.ToPercent(precision) : string.Empty;
		}

		public static string ToPercent(this decimal number, int precision = 0)
		{
			return $"{ number.Round(precision: precision).ToString("N" + precision) }%";
		}


		public static decimal RoundUp(this decimal number, int precision = 2)
		{
			var multiplier = 10m.Pow(precision);
			return Math.Ceiling(number * multiplier) / multiplier;
		}

		public static decimal? RoundUp(this decimal? number, int precision = 2)
		{
			return number.HasValue ? (decimal?)number.Value.RoundUp(precision) : null;
		}

		public static decimal RoundDown(this decimal number, int precision = 2)
		{
			var multiplier = 10m.Pow(precision);
			return Math.Floor(number * multiplier) / multiplier;
		}

		public static decimal? RoundDown(this decimal? number, int precision = 2)
		{
			return number.HasValue ? (decimal?)number.Value.RoundDown(precision) : null;
		}

		public static decimal Round(this decimal number, int precision = 2)
		{
			return Math.Round(number, precision);
		}

		public static decimal? Round(this decimal? number, int precision = 2)
		{
			return number.HasValue ? (decimal?)number.Value.Round(precision: precision) : (decimal?)null;
		}

		public static decimal Monetary(this decimal number)
		{
			return number.ToPrecision(2);
		}

		public static decimal Abs(this decimal number)
		{
			return Math.Abs(number);
		}

		public static string MonetaryFormat(this decimal? number)
		{
			return MonetaryFormat(number ?? 0);
		}

		public static string MonetaryFormat(this decimal number)
		{
			return number.ToPrecision(2).ToString("N2");
		}

		/// <summary>
		/// Calculates the percentage of the diference of this number for another
		/// </summary>
		/// <param name="inThis">Percentage variation of a number in another</param>
		/// <returns>Percentage variation of the 'number' for 'inThis'</returns>
		public static decimal PercentageOfChange(this decimal number, decimal inThis)
		{
			return (inThis - number).Percentage(inThis);
		}

		/// <summary>
		/// Calculates the percentagem of this number in another
		/// </summary>
		/// <param name="inThis">percentual do number em relacao à este parametro</param>
		/// <returns></returns>
		public static decimal Percentage(this decimal number, decimal inThis)
		{
			return inThis > 0 ? (number * 100) / inThis : 0;
		}

		/// <summary>
		/// Calcula o valor para o percentual informado
		/// </summary>
		/// <param name="porcentagem">Percentual que se deseja obter</param>
		/// <returns></returns>
		public static decimal Percent(this decimal number, decimal percent)
		{
			return number * (percent / 100);
		}

		/// <summary>
		/// Reduz o number no valor percentual
		/// </summary>
		/// <param name="porcentagem">Percentual que se deseja reduzir</param>
		/// <returns></returns>
		public static decimal Reduce(this decimal number, decimal percent)
		{
			return number - number.Percent(percent);
		}

		/// <summary>
		/// Aumenta o number no valor percentual
		/// </summary>
		/// <param name="porcentagem">Percentual que se deseja aumentar</param>
		/// <returns></returns>
		public static decimal Rise(this decimal number, decimal percent)
		{
			return number + number.Percent(percent);
		}

		/// <summary>
		/// Transforma um decimal em inteiro
		/// </summary>
		/// <returns></returns>
		public static int ToInt(this decimal number)
		{
			return int.Parse(number.ToPrecision(0).ToString());
		}

		public static string ToEmptyIfZero(this decimal number)
		{
			return number == 0 ? string.Empty : number.ToString();
		}

		/// <summary>
		/// Transforma um decimal em inteiro
		/// </summary>
		/// <returns></returns>
		public static int ToInt(this double number)
		{
			return int.Parse(number.ToPrecision(0).ToString());
		}

		/// <summary>
		/// Transforma um decimal em inteiro
		/// </summary>
		/// <returns></returns>
		public static decimal ToDecimal(this int number)
		{
			return decimal.Parse(number.ToString());
		}

		/// <summary>
		/// Calcula o percentual do number em outro
		/// </summary>
		/// <param name="em">percentual do number em relacao à este parametro</param>
		/// <returns></returns>
		public static int Percentage(this int number, int inThis)
		{
			return number.ToDecimal().Percentage(inThis).ToInt();
		}

		/// <summary>
		/// Calcula o valor para o percentual informado
		/// </summary>
		/// <param name="porcentagem">Percentual que se deseja obter</param>
		/// <returns></returns>
		public static int Percent(this int number, int percentage)
		{
			return number.ToDecimal().Percent(percentage).ToInt();
		}

		/// <summary>
		/// Reduz o number no valor percentual
		/// </summary>
		/// <param name="porcentagem">Percentual que se deseja reduzir</param>
		/// <returns></returns>
		public static int Reduce(this int number, int percentage)
		{
			return number - number.Reduce(percentage);
		}

		/// <summary>
		/// Aumenta o number no valor percentual
		/// </summary>
		/// <param name="porcentagem">Percentual que se deseja aumentar</param>
		/// <returns></returns>
		public static int Rise(this int number, int percentage)
		{
			return number + number.Rise(percentage);
		}
	}
}
