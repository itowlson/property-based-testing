using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositiveNegative.Tests
{
    public static class Generators
    {
        public static Arbitrary<RomanConvertibleInt32> RomanConvertibleInt => Arb.From(RomanConvertibleInt32Gen, RomanConvertibleInt32Shrink);

        public static Arbitrary<InvalidRomanNumber> InvalidRoman => Arb.From(InvalidRomanNumberGen, InvalidRomanNumberShrink);

        private static readonly Gen<RomanConvertibleInt32> RomanConvertibleInt32Gen =
            from i in Arb.Default.Int32()
                                 .Filter(n => n > 0 && n < 4000)
                                 .Generator
            select new RomanConvertibleInt32(i);

        private static IEnumerable<RomanConvertibleInt32> RomanConvertibleInt32Shrink(RomanConvertibleInt32 value)
        {
            return Arb.Shrink(value.Value).Select(n => new RomanConvertibleInt32(n));
        }

        private static readonly char[] RomanDigits = new[] { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };

        private static readonly Gen<InvalidRomanNumber> InvalidRomanNumberGen =
            from s in Gen.OneOf(InvalidRomanNumberCharsGen(), InvalidRomanNumberFormatGen())
            select new InvalidRomanNumber(s.Get);

        private static Gen<NonEmptyString> InvalidRomanNumberCharsGen() =>
            Arb.Default.NonEmptyString()
                       .Filter(s => s.Get.Any(ch => !RomanDigits.Contains(Char.ToUpperInvariant(ch))))
                       .Generator;

        private static Gen<NonEmptyString> InvalidRomanNumberFormatGen() =>
            Arb.Default.NonEmptyString()
                       .Filter(s => s.Get.All(ch => RomanDigits.Contains(Char.ToUpperInvariant(ch))) && !IsValidFormat(s.Get))
                       .Generator;

        private static IEnumerable<InvalidRomanNumber> InvalidRomanNumberShrink(InvalidRomanNumber value)
        {
            var roman = value.Value;

            if (roman.Length == 1)
            {
                yield break;
            }

            for (int i = 0; i < roman.Length - 1; ++i)
            {
                var shrunk = roman.Substring(0, i) + roman.Substring(i + 1);

                if (shrunk.Any(ch => !RomanDigits.Contains(Char.ToUpperInvariant(ch)))
                    || !IsValidFormat(shrunk))
                {
                    yield return new InvalidRomanNumber(shrunk);
                }
            }
        }

        private static bool IsValidFormat(string s)
        {
            s = s.ToUpperInvariant();

            return HigherUnitsComeBeforeLowerUnitsExceptMaybeOne(s, 'M', 'C', out s)
                && ThereCanBeAtMostOneFive(s, 'D', 'C', out s)
                && HigherUnitsComeBeforeLowerUnitsExceptMaybeOne(s, 'C', 'X', out s)
                && ThereCanBeAtMostOneFive(s, 'L', 'X', out s)
                && HigherUnitsComeBeforeLowerUnitsExceptMaybeOne(s, 'X', 'I', out s)
                && ThereCanBeAtMostOneFive(s, 'V', 'I', out s);
        }

        private static bool HigherUnitsComeBeforeLowerUnitsExceptMaybeOne(string s, char higherPower, char lowerPower, out string rest)
        {
            var splitIndex = s.LastIndexOf(higherPower);

            if (splitIndex < 0)
            {
                rest = s;
                return true;
            }

            var ms = s.Substring(0, splitIndex + 1);
            rest = s.Substring(splitIndex + 1);

            var naughties = ms.Count(ch => ch != higherPower);

            if (naughties == 0)
            {
                return true;
            }
            if (naughties > 1)
            {
                return false;
            }
            return ms.Count(ch => ch == lowerPower) == 1 && ms.IndexOf(lowerPower) == ms.Length - 2;
        }

        private static bool ThereCanBeAtMostOneFive(string s, char five, char one, out string rest)
        {
            if (s.IndexOf(five) < 0)
            {
                rest = s;
                return true;
            }

            if (s.IndexOf(five) == 0)
            {
                rest = s.Substring(1);
                return rest.IndexOf(five) < 0;
            }

            if (s.IndexOf(five) == 1)
            {
                rest = s.Substring(2);
                return s[0] == one && rest.IndexOf(five) < 0;
            }

            rest = s;
            return false;
        }

        private static IEnumerable<int> IndexesOf(string s, char ch)
        {
            ch = Char.ToUpperInvariant(ch);

            for (int i = 0; i < s.Length; ++i)
            {
                if (Char.ToUpperInvariant(s[i]) == ch)
                {
                    yield return i;
                }
            }
        }

        public struct RomanConvertibleInt32
        {
            public RomanConvertibleInt32(int value)
            {
                Value = value;
            }

            public int Value { get; }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        public struct InvalidRomanNumber
        {
            public InvalidRomanNumber(string value)
            {
                Value = value;
            }

            public string Value { get; }

            public override string ToString()
            {
                return $"{Value.ToUpperInvariant()} ({Value})";
            }
        }
    }
}
