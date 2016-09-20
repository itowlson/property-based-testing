using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositiveNegative
{
    public static class Romaniser
    {
        // Incorrect!
        public static int Parse(string roman)
        {
            if (roman == null)
            {
                throw new ArgumentNullException(nameof(roman));
            }
            if (roman.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(roman));
            }

            var mangled = roman.ToUpperInvariant();

            for (int power = 0; power < One.Length - 1; ++power)
            {
                mangled = mangled.Replace(One[power] + One[power + 1], Repeat(One[power], 9));
            }
            for (int power = 0; power < Five.Length; ++power)
            {
                mangled = mangled.Replace(One[power] + Five[power], Repeat(One[power], 4));
                mangled = mangled.Replace(Five[power], Repeat(One[power], 5));
            }

            int value = 0;
            int? last = null;

            foreach (var ch in mangled)
            {
                var power = Array.IndexOf(One, ch.ToString());
                if (power < 0)
                {
                    throw new ArgumentException($"{roman} is not a valid Roman number", nameof(roman));
                }
                if (last.HasValue)
                {
                    if (power > last.Value)
                    {
                        throw new ArgumentException($"{roman} is not a valid Roman number", nameof(roman));
                    }
                }
                last = power;
                value += (int)(Math.Pow(10, power));
            }

            return value;
        }

        public static string ToRoman(int value)
        {
            if (value <= 0 || value >= 4000)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            var digits = Digitise(value);
            var romanBits = digits.Select((d, i) => Romanise(d, i)).Reverse();
            return String.Concat(romanBits);
        }

        private static IEnumerable<int> Digitise(int value)
        {
            if (value == 0)
            {
                yield break;
            }

            yield return value % 10;

            foreach (var d in Digitise(value / 10))
            {
                yield return d;
            }
        }

        private static string Romanise(int digit, int power)
        {
            if (digit == 9)
            {
                return One[power] + One[power + 1];
            }
            if (digit >= 5)
            {
                return Five[power] + Romanise(digit - 5, power);
            }
            if (digit == 4)
            {
                return One[power] + Five[power];
            }
            return Repeat(One[power], digit);
        }

        private static string Repeat(string s, int repeatCount)
        {
            if (repeatCount == 0)
            {
                return String.Empty;
            }
            return s + Repeat(s, repeatCount - 1);
        }

        private static readonly string[] Five = { "V", "L", "D" };
        private static readonly string[] One = { "I", "X", "C", "M" };
    }
}
