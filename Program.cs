using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo
{
    public class Program
    {
        static void Main(string[] args)
        {
            // LeetCodeTest test = new LeetCodeTest();
            // test.TestFindMedianSortedArrays();

            // RecursionTest rt = new RecursionTest();
            // rt.TestCountSmaller();

            DynamicTest dt = new DynamicTest();
            dt.TestKnapsack();
        }

        // Get memoery address of an int variable
        static string GetIntAddress(int i)
        {
            unsafe
            {
                int* ptr = &i;
                IntPtr addr = (IntPtr)ptr;
                return addr.ToString("x");
            }
        }

        static IEnumerable<int> GenerateWithYield()
        { 
            var i = 0;
            while (i<5)
                yield return ++i; 
        }
    }
}
