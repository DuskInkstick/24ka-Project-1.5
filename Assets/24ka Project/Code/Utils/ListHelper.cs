using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Utils
{
    internal static class ListHelper
    {
        private static Random _random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count() - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);

                var temp = list[j];
                list[j] = list[i];
                list[i] = temp;
            }
        }
    }
}
