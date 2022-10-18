using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace _Scripts.Extensions
{
    public static class ListExtention
    {
        private static Random rng = new Random();  

        public static IList<T> Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                (list[k], list[n]) = (list[n], list[k]);
            }

            return list;
        }
    }
}

/*
 * Шафлить можно проще, вот так - 
 * Random rnd = new Random();
 * list = list.OrderBy(x => rnd.Next()).ToArray(); 
 */