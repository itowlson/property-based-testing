using FsCheck.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomType.Tests
{
    [Properties(Arbitrary = new[] { typeof(Generators) })]
    public class Vector3Tests
    {
        [Property]
        public void VectorLengthIsNonNegative(Vector3 vector)
        {
            Assert.True(vector.Length >= 0);
        }

        [Property]
        public void VectorLengthIsAtLeastAsGreatAsAnyComponent(Vector3 vector)
        {
            Assert.True(vector.Length >= vector.X);
            Assert.True(vector.Length >= vector.Y);
            Assert.True(vector.Length >= vector.Z);
        }

        [Property]
        public void TriangleInequalityHolds(Vector3 first, Vector3 second)
        {
            var sum = new Vector3(first.X + second.X, first.Y + second.Y, first.Z + second.Z);

            Assert.True(sum.Length <= first.Length + second.Length);
        }
    }
}
