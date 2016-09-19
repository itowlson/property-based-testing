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
            CompareAssert.Assert(vector.Length).IsGreaterThanOrEqualTo(0);
        }

        [Property]
        public void VectorLengthIsAtLeastAsGreatAsAnyComponent(Vector3 vector)
        {
            CompareAssert.Assert(vector.Length).IsGreaterThanOrEqualTo(vector.X);
            CompareAssert.Assert(vector.Length).IsGreaterThanOrEqualTo(vector.Y);
            CompareAssert.Assert(vector.Length).IsGreaterThanOrEqualTo(vector.Z);
        }

        [Property]
        public void TriangleInequalityHolds(Vector3 first, Vector3 second)
        {
            CompareAssert.Assert((first + second).Length).IsLessThanOrEqualTo(first.Length + second.Length);
        }
    }
}
