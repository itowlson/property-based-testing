using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomType.Tests
{
    public static class Generators
    {
        public static Arbitrary<Vector3> Vector3 => Arb.From(Vector3Gen, Vector3Shrink);

        private static Gen<double> NormalDouble()
            => from nf in Arb.Default.NormalFloat().Generator select nf.Get;

        private static readonly Gen<Vector3> Vector3Gen =
            from x in NormalDouble()
            from y in NormalDouble()
            from z in NormalDouble()
            select new Vector3(x, y, z);

        private static IEnumerable<Vector3> Vector3Shrink(Vector3 vector)
        {
            // Not needed if we protect ourselves by using NormalFloat() instead of Float()
            //if (Double.IsNaN(vector.X) || Double.IsNaN(vector.Y) || Double.IsNaN(vector.Z))
            //{
            //    yield break;
            //}
            //if (Double.IsInfinity(vector.X) || Double.IsInfinity(vector.Y) || Double.IsInfinity(vector.Z))
            //{
            //    yield break;
            //}

            if (vector.X != 0)
            {
                yield return new Vector3(ShrinkProportional(vector.X), vector.Y, vector.Z);
                yield return new Vector3(ShrinkLinear(vector.X), vector.Y, vector.Z);
            }
            if (vector.Y != 0)
            {
                yield return new Vector3(vector.X, ShrinkProportional(vector.Y), vector.Z);
                yield return new Vector3(vector.X, ShrinkLinear(vector.Y), vector.Z);
            }
            if (vector.Z != 0)
            {
                yield return new Vector3(vector.X, vector.Y, ShrinkProportional(vector.Z));
                yield return new Vector3(vector.X, vector.Y, ShrinkLinear(vector.Z));
            }
        }

        private static double ShrinkProportional(double x)
        {
            return x / 3;
        }

        private static double ShrinkLinear(double x)
        {
            if (x < -10)
            {
                return x + 10;
            }
            if (x > 10)
            {
                return x - 10;
            }
            return 0;
        }
    }
}
