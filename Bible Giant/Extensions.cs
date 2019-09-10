using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Bible_Giant
{
    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random local = new Random();
        public static Random ThisThreadsRandom
        {
            get
            {
                return local;
            }
        }
    }
    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;

            }
        }
    }
}
