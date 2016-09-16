using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maximiser
{
    public static class Reverser
    {
        public static List<T> Reverse<T>(List<T> source)
        {
            // absolutely rotten implementation
            return source;


            // better implementation... but are we sure it's right?
            //var reversed = new List<T>(source.Count);

            //for (int i = source.Count - 1; i >= 0; --i)
            //{
            //    reversed.Add(source[i]);
            //}

            //return reversed;
        }
    }
}
