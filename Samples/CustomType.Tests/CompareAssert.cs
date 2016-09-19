using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomType.Tests
{
    internal static class CompareAssert
    {
        internal static CompareAssertImpl<T> Assert<T>(T actual)
            where T : IComparable<T>
        {
            return new CompareAssertImpl<T>(actual);
        }

        internal class CompareAssertImpl<T>
            where T : IComparable<T>
        {
            private readonly T _actual;

            public CompareAssertImpl(T actual)
            {
                _actual = actual;
            }

            public void IsLessThanOrEqualTo(T expectedBound)
            {
                if (_actual.CompareTo(expectedBound) > 0)
                {
                    throw new CompareAssertException($"Expected value <= {expectedBound}, got {_actual}");
                }
            }

            public void IsGreaterThanOrEqualTo(T expectedBound)
            {
                if (_actual.CompareTo(expectedBound) < 0)
                {
                    throw new CompareAssertException($"Expected value >= {expectedBound}, got {_actual}");
                }
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
