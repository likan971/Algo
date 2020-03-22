using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algo
{
    public class LeetCodeTest
    {
        public LeetCode leetCode = new LeetCode();

        public void TestTwoSumEx()
        {
            var result = leetCode.TwoSumEx(new int[] {-1, -4, 0, -5, -2, -2, -3}, -5);
            foreach (var r in result)
            {
                Console.WriteLine($"{r[0]}, {r[1]}");
            }
        }

        public void TestThreeSum()
        {
            var result = leetCode.ThreeSum(new int[] { -1, 0, 1, 2, -1, -4 });
            foreach (var r in result)
            {
                Console.WriteLine($"{r[0]}, {r[1]}, {r[2]}");
            }
        }

        public void TestAddTwoNumbers()
        {
            var l1 = Utils.CreateLinkedList(3);
            var l2 = Utils.CreateLinkedList(3);

            Utils.PrintLinkedList(leetCode.AddTwoNumbers(l1, l2));
        }

        public void TestLengthOfLongestSubstring()
        {
            Console.WriteLine(leetCode.LengthOfLongestSubstring("tmmzuxt"));
        }

        public void TestLongestPalindrome()
        {
            Console.WriteLine(leetCode.LongestPalindrome("ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg"));
        }

        public void TestSearchInRotatedSortedArray()
        {
            var array = new int[] {1,2,4,5,6,7,0 };
            Console.WriteLine(leetCode.SearchInRotatedSortedArray(array, 2));
        }

        public void TestFindMedianSortedArrays()
        {
            Console.WriteLine(leetCode.FindMedianSortedArrays(new int[] {7,2}, new int[] {3,6,1,9}));
        }

        public void TestLevelOrder()
        {
            var root = new TreeNode(new int[] {1,2,3,4,5,6,7,8,9});
            var result = leetCode.LevelOrder(root);
            Utils.Print2dArray(result as int[][]);
        }
    }

    public class LeetCode
    {
        /// <summary>
        /// 1. Two Sum for 3Sum
        /// Instead of returing indices of one result elements, this method returns multiple result elements themselves
        /// </summary>
        public IList<List<int>> TwoSumEx(int[] nums, int target, int excludeIndex = - 1)
        {
            var result = new List<List<int>>();
            var hashTable = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i ++)
            {
                if (excludeIndex >= 0 && i == excludeIndex)
                {
                    continue;
                }
                var num1 = nums[i];
                var num2 = target - num1;
                if (hashTable.ContainsKey(num2))
                {
                    result.Add(num1 < num2 ? new List<int> { num1, num2 } : new List<int> { num2, num1 });
                }
                else if (!hashTable.ContainsKey(num1))
                {
                    hashTable.Add(num1, i);
                }
            }
            return result;
        }

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
        /// 4. Median of Two Sorted Arrays
        /// <summary>
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var totalLength = nums1.Length + nums2.Length;
            if (totalLength == 1)
            {
                return nums1.Length > 0 ? nums1[0] : nums2[0];
            }

            var sizeDiff = totalLength % 2;
            var minHeap = new MinHeap(totalLength + 1);
            var maxHeap = new MaxHeap(totalLength + 1);

            foreach (var n in nums1)
            {
                BuildHeap(minHeap, maxHeap, n);
            }
            foreach (var n in nums2)
            {
                BuildHeap(minHeap, maxHeap, n);
            }
            while (maxHeap.Size() != minHeap.Size() + sizeDiff)
            {
                if (maxHeap.Size() > minHeap.Size() + sizeDiff)
                {
                    minHeap.Offer(maxHeap.Poll());
                }
                else
                {
                    maxHeap.Offer(minHeap.Poll());
                }
            }

            return sizeDiff == 0 ? (minHeap.Peek() + maxHeap.Peek()) / 2.0 : maxHeap.Peek();
        }

        private void BuildHeap(MinHeap minHeap, MaxHeap maxHeap, int n)
        {
            if (maxHeap.IsEmpty() || n <= maxHeap.Peek())
            {
                maxHeap.Offer(n);
            }
            else
            {
                minHeap.Offer(n);
            }
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

        /// <summary>
        /// 15. 3Sum
        /// </summary>
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            var result = new Dictionary<string, IList<int>>();
            var hashTable = new Dictionary<int, int>();

            if (nums.Length < 3)
            {
                return result.Values.ToList();
            }
            for (int i = 0; i < nums.Length; i ++)
            {
                if (hashTable.ContainsKey(nums[i]))
                {
                    continue;
                }
                var twoSum = TwoSumEx(nums, nums[i] * -1, i);
                hashTable.Add(nums[i], i);

                foreach (var twoSumItem in twoSum)
                {
                    if (nums[i] < twoSumItem[0])
                    {
                        twoSumItem.Insert(0, nums[i]);
                    }
                    else if (nums[i] > twoSumItem[1])
                    {
                        twoSumItem.Add(nums[i]);
                    }
                    else
                    {
                        twoSumItem.Insert(1, nums[i]);
                    }
                    result.TryAdd($"{twoSumItem[0]}{twoSumItem[1]}{twoSumItem[2]}", twoSumItem);
                }
            }

            return result.Values.ToList();
        }

        /// <summary>
        /// 33. Search in Rotated Sorted Array
        /// <summary>
        public int SearchInRotatedSortedArray(int[] nums, int target)
        {
            if (nums.Length == 0)
            {
                return -1;
            }

            var low = 0;
            var high = nums.Length - 1;

            // find the smallest element (start) in the array
            while (low < high)
            {
                var mid = low + (high - low)/2;
                if (nums[mid] > nums[high])
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid;
                }
            }

            // nums[low] is the start of this rotated ascendingly sorted array;
            if (target <= nums[nums.Length - 1])
            {
                high = nums.Length - 1;  // target is located at the right side of the array 
            }
            else
            {
                low = 0;  // target is located at the left side of the array
            }

            // normal binary search
            while (low <= high)
            {
                var mid = low + (high - low)/2;
                if (target == nums[mid])
                {
                    return mid;
                }
                else if (target > nums[mid])
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            return -1;
        }

        /// <summary>
        /// 102. Binary Tree Level Order Traversal
        /// </summary>
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            if (root == null)
            {
                return new List<int[]>().ToArray();
            }

            var q = new Queue<TreeNode>();
            var result = new List<int[]>();

            q.Enqueue(root);
            TreeNode node = root;
            while (q.TryPeek(out TreeNode element))
            {
                var list = new List<int>();
                var nodeList = new List<TreeNode>();
                while (q.TryDequeue(out node))
                {
                    list.Add(node.val);
                    nodeList.Add(node);
                }
                if (list.Count > 0)
                {
                    //result.Add(list.ToArray());
                    result.Insert(0, list.ToArray());
                }
                foreach(var levelNode in nodeList)
                {
                    if (levelNode.left != null)
                    {
                        q.Enqueue(levelNode.left);
                    }
                    if (levelNode.right != null)
                    {
                        q.Enqueue(levelNode.right);
                    }
                }
            }

            return result.ToArray();
        }
    }
}