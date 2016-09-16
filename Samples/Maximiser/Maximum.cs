using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maximiser
{
    public static class Maximum
    {
        public static int MaxValue(IEnumerable<int> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            if (!values.Any())
            {
                throw new ArgumentException($"{nameof(values)} must not be empty", nameof(values));
            }

            // Bad implementation 1
            //return values.First();

            // Bad implementation 2
            return values.Sum(v => Math.Abs(v));
        }
    }
}
