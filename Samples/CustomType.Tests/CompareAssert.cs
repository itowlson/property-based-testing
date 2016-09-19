using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomType.Tests
{
    internal static class CompareAssert
    {
        internal static void LessThanOrEqual<T>(T expectedBound, T actual)
            where T : IComparable<T>
        {
            if (actual.CompareTo(expectedBound) > 0)
            {
                throw new CompareAssertException($"Expected value <= {expectedBound}, got {actual}");
            }
        }

        internal static void GreaterThanOrEqual<T>(T expectedBound, T actual)
            where T : IComparable<T>
        {
            if (actual.CompareTo(expectedBound) < 0)
            {
                throw new CompareAssertException($"Expected value >= {expectedBound}, got {actual}");
            }
        }

        [Serializable]
        public class CompareAssertException : Exception
        {
            public CompareAssertException() { }
            public CompareAssertException(string message) : base(message) { }
            public CompareAssertException(string message, Exception inner) : base(message, inner) { }
            protected CompareAssertException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}
