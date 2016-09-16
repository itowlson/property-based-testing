using FsCheck.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Maximiser.Tests
{
    public class MaximumTests
    {
        // Value-based tests

        [Fact]
        public void MaxValueReturnsTheGreatestValue()
        {
            Assert.Equal(3, Maximum.MaxValue(new[] { 3, 0, 0 }));
        }

        [Fact]
        public void MaxOfEmptyCollectionThrows()
        {
            Assert.Throws<ArgumentException>(() => Maximum.MaxValue(new int[0]));
        }

        [Fact]
        public void MaxOfNullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => Maximum.MaxValue(null));
        }

        // Property-based tests

        [Property]
        public void AllValuesAreLessThanOrEqualToTheMax(int[] values)
        {
            if (values.Length == 0)
            {
                return;  // This is a bit suspicious and we'll come back to it later
            }

            var max = Maximum.MaxValue(values);

            foreach (var value in values)
            {
                Assert.True(value <= max);
            }
        }

        [Property]
        public void TheMaxIsOneOfTheOriginalValues(int[] values)
        {
            if (values.Length == 0)
            {
                return;
            }

            var max = Maximum.MaxValue(values);
            Assert.Contains(max, values);
        }
    }
}
