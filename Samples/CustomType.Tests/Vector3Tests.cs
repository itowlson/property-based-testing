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
            CompareAssert.GreaterThanOrEqual(0, vector.Length);
        }

        [Property]
        public void VectorLengthIsAtLeastAsGreatAsAnyComponent(Vector3 vector)
        {
            CompareAssert.GreaterThanOrEqual(vector.X, vector.Length);
            CompareAssert.GreaterThanOrEqual(vector.Y, vector.Length);
            CompareAssert.GreaterThanOrEqual(vector.Z, vector.Length);
        }

        [Property]
        public void TriangleInequalityHolds(Vector3 first, Vector3 second)
        {
            CompareAssert.LessThanOrEqual(first.Length + second.Length, (first + second).Length);
        }
    }
}
