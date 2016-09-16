using FsCheck.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Maximiser.Tests
{
    public class ReverserTests
    {
        [Property]
        public void ReversedListIsSameLengthAsOriginal(List<int> values)
        {
            var reversed = Reverser.Reverse(values);
            Assert.Equal(values.Count, reversed.Count);
        }

        [Property]
        public void TheReverseOfTheReverseIsTheOriginalList(List<int> values)
        {
            var reversedTwice = Reverser.Reverse(Reverser.Reverse(values));
            Assert.Equal(values, reversedTwice);
        }

        [Property]
        public void AListFollowedByItsReverseIsPalindromic(List<int> values)
        {
            var reversed = Reverser.Reverse(values);
            var both = values.Concat(reversed).ToList();

            Assert.Equal(both, Reverser.Reverse(both));
        }

        [Property]
        public void EachElementIsTheSameDistanceFromTheEndAsItWasFromTheStart(List<int> values)
        {
            var reversed = Reverser.Reverse(values);

            for (int i = 0; i < values.Count; ++i)
            {
                Assert.Equal(values[i], reversed[values.Count - (i + 1)]);
            }
        }

        [Property]
        public void ReversingAListDoesNotAffectTheOriginalList(List<int> values)
        {
            var original = new List<int>(values);
            Reverser.Reverse(values);
            Assert.Equal(original, values);
        }
    }
}
