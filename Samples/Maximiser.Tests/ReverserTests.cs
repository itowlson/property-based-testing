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
        // len("abc") == len("cba") == len(R("abc"))
        [Property]
        public void ReversedListIsSameLengthAsOriginal(List<int> values)
        {
            var reversed = Reverser.Reverse(values);
            Assert.Equal(values.Count, reversed.Count);
        }

        // R(R("abc")) == R("cba") == "abc"
        [Property]
        public void TheReverseOfTheReverseIsTheOriginalList(List<int> values)
        {
            var reversedTwice = Reverser.Reverse(Reverser.Reverse(values));
            Assert.Equal(values, reversedTwice);
        }

        // "abc" ++ R("abc") == "abc" ++ "cba" == "abccba", R("abccba") == itself
        [Property]
        public void AListFollowedByItsReverseIsPalindromic(List<int> values)
        {
            var reversed = Reverser.Reverse(values);
            var both = values.Concat(reversed).ToList();

            Assert.Equal(both, Reverser.Reverse(both));
        }

        // R("abc" ++ "def") == R("abcdef") == "fedcba" == "fed" ++ "cba" == R("def") ++ R("abc")
        [Property]
        public void ReversingTheConcatOfTwoListsIsEquivalentToConcattingTheReversesInTheOppositeOrder(List<int> values1, List<int> values2)
        {
            var concat = values1.Concat(values2).ToList();
            var revConcat = Reverser.Reverse(concat);

            var rev1 = Reverser.Reverse(values1);
            var rev2 = Reverser.Reverse(values2);
            var concatRevs = rev2.Concat(rev1);

            Assert.Equal(concatRevs, revConcat);
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
