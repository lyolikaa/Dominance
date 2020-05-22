using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Dominance
{
    class Program
    {
        static void Main(string[] args)
        {
            //samples to test the function
            int[] hasDomArray = new[] { 3, 4, 3, 2, 3, -1, 3, 3 };
            int[] noDomArray = new[] { 2, 5, 6, 7, 2, 2 };
            //sample array for performance test
            var count = 10_000;
            Random rand = new Random();
            int[] arr = new int[count];
            for (int i = 0; i < count; i++) {
                arr[i] = rand.Next(-10, 10);
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var d1 = Solution.solution(arr);
            sw.Stop();
            Console.WriteLine($"solution linq:{sw.Elapsed}");
            sw.Start();
            var d2 = Solution.sol2(arr);
            sw.Stop();
            Console.WriteLine($"solution dict:{sw.Elapsed}");
            d1 = Solution.solution(hasDomArray);
            d2 = Solution.solution(noDomArray);
            //my benchmark conclusion - 'solution method' is little bit faster
        }


    }

    public class Solution
    {
        /// <summary>
        /// Short LINQ based solution
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static int solution(int[] A)
        {
            var getDominant = A.ToList().GroupBy(e => e).FirstOrDefault(e => e.Count() > A.Length / 2);
            return getDominant != null ? A.ToList().IndexOf(getDominant.Key) : -1;
        }

        /// <summary>
        /// Dictinary with optimized exit solution
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static int sol2(int[] A)
        {
            var dictionary = new Dictionary<int, int>();
            KeyValuePair<int, int> max;
            for(int ii = 0; ii < A.Count(); ii++) 
            {
                if (dictionary.Any())
                {
                    max = getMax(dictionary);
                    //if we sure the rest of array won't giva us dominannce
                    if (max.Value + A.Length - ii < A.Length / 2)
                        return -1;
                    //if we got dominance
                    if (max.Value > A.Length / 2)
                        return A.ToList().IndexOf(max.Key);
                }
                if (dictionary.ContainsKey(A[ii]))
                    dictionary[A[ii]]++;
                else
                    dictionary.Add(A[ii], 1);
            }
            return -1;
        }

        private static KeyValuePair<int,int> getMax(Dictionary<int, int> dict)
        {
            return dict.OrderByDescending(d => d.Value).First();
        }
    }

}
