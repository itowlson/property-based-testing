using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomType
{
    public struct Vector3
    {
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public double Length =>
            X + Y + Z;                                   // incorrect!
            // Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);  // taxicab metric
            // Math.Sqrt(X * X + Y * Y + Z * Z);         // Euclidean metric

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}
