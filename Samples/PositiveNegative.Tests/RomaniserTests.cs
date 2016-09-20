using FsCheck.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static PositiveNegative.Tests.Generators;

namespace PositiveNegative.Tests
{
    [Properties(Arbitrary = new[] { typeof(Generators) })]
    public class RomaniserTests
    {
        //[Property]
        //public void RepresentableValuesRoundTripCorrectly_1(int value)
        //{
        //    // Uh oh
        //    Assert.Equal(value, Romaniser.Parse(Romaniser.ToRoman(value)));
        //}

        //[Property]
        //public void RepresentableValuesRoundTripCorrectly_2(int value)
        //{
        //    // We'd probably get lucky with this but...
        //    if (value > 0 && value < 4000)
        //    {
        //        Assert.Equal(value, Romaniser.Parse(Romaniser.ToRoman(value)));
        //    }
        //}

        [Property]
        public void RepresentableValuesRoundTripCorrectly_2(RomanConvertibleInt32 genValue)
        {
            int value = genValue.Value;
            Assert.Equal(value, Romaniser.Parse(Romaniser.ToRoman(value)));
        }

        [Property]
        public void RomanNumberNeverContainsFourUnitsTogether(RomanConvertibleInt32 genValue)
        {
            int value = genValue.Value;

            foreach (var unit in new[] { 'I', 'X', 'C', 'M' })
            {
                Assert.DoesNotContain(new string(unit, 4), Romaniser.ToRoman(value));
            }
        }

        [Property]
        public void RomanNumberNeverContainsTwoFivesTogether(RomanConvertibleInt32 genValue)
        {
            int value = genValue.Value;

            foreach (var unit in new[] { 'V', 'L', 'D' })
            {
                Assert.DoesNotContain(new string(unit, 2), Romaniser.ToRoman(value));
            }
        }

        [Property(MaxTest = 1000)]
        public void InvalidRomanNumberDoesNotParse(InvalidRomanNumber roman)
        {
            var ex = Assert.Throws<ArgumentException>(() => Romaniser.Parse(roman.Value));
            Assert.Equal("roman", ex.ParamName);
        }

        [Theory]
        [InlineData(1, "I")]
        [InlineData(3, "III")]
        [InlineData(4, "IV")]
        [InlineData(5, "V")]
        [InlineData(6, "VI")]
        [InlineData(8, "VIII")]
        [InlineData(9, "IX")]
        [InlineData(10, "X")]
        [InlineData(11, "XI")]
        [InlineData(14, "XIV")]
        [InlineData(15, "XV")]
        [InlineData(19, "XIX")]
        [InlineData(20, "XX")]
        [InlineData(21, "XXI")]
        [InlineData(25, "XXV")]
        [InlineData(26, "XXVI")]
        [InlineData(48, "XLVIII")]
        [InlineData(49, "XLIX")]
        [InlineData(50, "L")]
        [InlineData(80, "LXXX")]
        [InlineData(90, "XC")]
        [InlineData(99, "XCIX")]
        [InlineData(100, "C")]
        [InlineData(299, "CCXCIX")]
        [InlineData(400, "CD")]
        [InlineData(499, "CDXCIX")]
        [InlineData(900, "CM")]
        [InlineData(999, "CMXCIX")]
        [InlineData(1000, "M")]
        [InlineData(1050, "ML")]
        [InlineData(1101, "MCI")]
        [InlineData(1499, "MCDXCIX")]
        [InlineData(1501, "MDI")]
        [InlineData(1983, "MCMLXXXIII")]
        [InlineData(1984, "MCMLXXXIV")]
        [InlineData(1999, "MCMXCIX")]
        [InlineData(2000, "MM")]
        [InlineData(3999, "MMMCMXCIX")]
        public void RomaniseMe(int value, string roman)
        {
            Assert.Equal(roman, Romaniser.ToRoman(value));
        }
    }
}
