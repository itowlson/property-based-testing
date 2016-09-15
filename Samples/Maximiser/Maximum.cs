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
            // Bad implementation 1
            //return values.First();

            // Bad implementation 2
            return values.Sum(v => Math.Abs(v));
        }
    }
}
