using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo
{
    public class Program
    {
        static void Main(string[] args)
        {
            LeetCodeTest test = new LeetCodeTest();
            test.TestLongestPalindrome();
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

    public class Model
    {
        public int Key { get; set; }

        public string Value { get; set; }
    }
}
