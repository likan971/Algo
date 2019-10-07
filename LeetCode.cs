using System;
using System.Collections.Generic;
using System.Text;

namespace Algo
{
    public class LeetCodeTest
    {
        public LeetCode leetCode = new LeetCode();

        public void TestAddTwoNumbers()
        {
            var listTest = new LinkedListTest();
            var l1 = listTest.CreateLinkedList(3);
            var l2 = listTest.CreateLinkedList(3);

            listTest.PrintLinkedList(leetCode.AddTwoNumbers(l1, l2));
        }

        public void TestLengthOfLongestSubstring()
        {
            Console.WriteLine(leetCode.LengthOfLongestSubstring("tmmzuxt"));
        }

        public void TestLongestPalindrome()
        {
            Console.WriteLine(leetCode.LongestPalindrome("ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg"));
        }
    }

    public class LeetCode
    {
        /// <summary>
        /// 2. Add Two Numbers
        /// </summary>
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var result = new ListNode(0);
            var p = result;
            var carry = 0;
            while (l1 != null || l2 != null)
            {
                var sum = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;
                carry = sum >= 10 ? 1 : 0;
                sum = sum % 10;
                p.next = new ListNode(sum);
                p = p.next;

                l1 = l1?.next;
                l2 = l2?.next;
            }
            if (carry == 1)
            {
                p.next = new ListNode(1);
            }

            return result.next;
        }

        /// <summary>
        /// 3. Longest Substring Without Repeating Characters
        /// <summary>
        public int LengthOfLongestSubstring(string s)
        {
            var dic = new Dictionary<char, int>();
            var maxLength = 0;

            for (int i = 0, j = 0; j < s.Length; j ++) //sliding window
            {
                if (dic.ContainsKey(s[j]))
                {
                    i = Math.Max(i, dic[s[j]]);
                    dic[s[j]] = j + 1;
                }
                else
                {
                    dic.Add(s[j], j + 1);
                }
                if (j - i + 1 > maxLength)
                {
                    maxLength = j - i + 1;
                }
            }
            return maxLength;
        }
    
        /// <summary>
        /// 5. Longest Palindromic Substring
        /// <summary>
        public string LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            var dic = new Dictionary<char, List<int>>();
            var subString = "";
            var result = "";

            for (int j = 0; j < s.Length; j++)
            {
                if (dic.ContainsKey(s[j]))
                {
                    foreach (var i in dic[s[j]])
                    {
                        subString = s.Substring(i, j - i + 1);
                        if (isPalindrome(subString))
                        {
                            result = subString.Length > result.Length ? subString : result;
                        }
                    }
                    dic[s[j]].Add(j);
                }
                else
                {
                    dic.Add(s[j], new List<int>{ j });
                }
            }
            return result == "" ? s[0].ToString() : result;
        }

        private bool isPalindrome(string s)
        {
            for (int i = 0, j = s.Length - 1; i <= j; i++, j--)
            {
                if (s[i] != s[j])
                {
                    return false;
                }
            }
            return true;
        }
    }
}