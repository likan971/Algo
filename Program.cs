using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Algo
{
    public class Program
    {
        static void Main(string[] args)
        {
            // LeetCodeTest test = new LeetCodeTest();
            // test.TestFindMedianSortedArrays();

            RecursionTest rt = new RecursionTest();
            rt.TestCountSmaller();
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
    }
}
