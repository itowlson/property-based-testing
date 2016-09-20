using FsCheck.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomType.Tests
{
    // No generator needed... sorta kinda
    public class Vector3RTests
    {
        [Property]
        public void VectorLengthIsNonNegative(Vector3R vector)
        {
            CompareAssert.Assert(vector.Length).IsGreaterThanOrEqualTo(0);
        }

        [Property]
        public void VectorLengthIsAtLeastAsGreatAsAnyComponent(Vector3R vector)
        {
            CompareAssert.Assert(vector.Length).IsGreaterThanOrEqualTo(vector.X);
            CompareAssert.Assert(vector.Length).IsGreaterThanOrEqualTo(vector.Y);
            CompareAssert.Assert(vector.Length).IsGreaterThanOrEqualTo(vector.Z);
        }

        [Property]
        public void TriangleInequalityHolds(Vector3R first, Vector3R second)
        {
            CompareAssert.Assert((first + second).Length).IsLessThanOrEqualTo(first.Length + second.Length);
        }
    }
}
