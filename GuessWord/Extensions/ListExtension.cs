using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.Extensions
{
    public static class ListExtensions
    {
        public static T PickRandom<T>(this List<T> items)
        {
            var random = new Random();

            return items[random.Next(items.Count)];
        }
    }
}
