using System;
using System.Collections.Generic;

namespace Algo
{
    public class RecursionTest
    {
        Recursion r = new Recursion();

        public void TestLongestUnivaluePath()
        {
            var root = new TreeNode(1);
            root.left = new TreeNode(1);
            root.right = new TreeNode(1);
            root.left.left = new TreeNode(1);
            root.left.right = new TreeNode(1);
            // root.right.left = new TreeNode(5);
            // root.right.right = new TreeNode(4);
            // root.left.left.left = new TreeNode(5);
            // root.left.left.right = new TreeNode(4);
            // root.left.right.left = new TreeNode(5);

            Console.Write(r.LongestUnivaluePath(root));
        }

        public void TestCountSmaller()
        {
            var result = r.CountSmaller(new int[] {3,2,1,2,3});
            var array = new int[result.Count];
            result.CopyTo(array, 0);
            Utils.PrintArray(array);
        }
    }

    public class Recursion
    {
        private int max = 0;
        private int[] result315;

        ///<summary>
        /// LeetCode 687
        ///</summary>
        public int LongestUnivaluePath(TreeNode root)
        {
            LeetCode687(root);
            return max;
        }

        private int LeetCode687(TreeNode root)
        {
            if (root == null) return 0;

            var left = LeetCode687(root.left);
            var right = LeetCode687(root.right);
            int leftLength = 0, rightLength = 0;
            if (root.left != null && root.val == root.left.val)
            {
                leftLength += left + 1;
            }
            if (root.right != null && root.val == root.right.val)
            {
                rightLength += right + 1;
            }

            max = Math.Max(max, leftLength + rightLength);
            return Math.Max(leftLength, rightLength);
        }

        ///<summary>
        /// 315. Count of Smaller Numbers After Self
        ///</summary>
        public IList<int> CountSmaller(int[] nums)
        {
            result315 = new int[nums.Length];
            var numIndexes = new List<(int, int)>(nums.Length);
            for (int i = 0; i < nums.Length; i ++)
            {
                numIndexes.Add((nums[i], i));
            }
            MergeSort(numIndexes, 0, numIndexes.Count - 1);
            return result315;
        }

        private void MergeSort(List<(int,int)> numIndexes, int low, int high)
        {
            if (low < high)
            {
                var mid = (low + high) / 2;
                MergeSort(numIndexes, low, mid);
                MergeSort(numIndexes, mid + 1, high);
                Merge(numIndexes, low, mid, high);
            }
        }

        private void Merge(List<(int,int)> numIndexes, int low, int mid, int high)
        {
            var temp = new List<(int,int)>(high - low + 1);
            int left = low;
            int right = mid + 1;
            int count = 0;

            while (left <= mid && right <= high)
            {
                if (numIndexes[left].Item1 <= numIndexes[right].Item1)
                {
                    if (count > 0)
                    {
                        result315[numIndexes[left].Item2] += count;
                    }
                    temp.Add((numIndexes[left].Item1, numIndexes[left].Item2));
                    left ++;
                }
                else
                {
                    count ++;
                    temp.Add((numIndexes[right].Item1, numIndexes[right].Item2));
                    right ++;
                }
            }

            while (left <= mid)
            {
                if (count > 0)
                {
                    result315[numIndexes[left].Item2] += count;
                }
                temp.Add((numIndexes[left].Item1, numIndexes[left].Item2));
                left ++;
            }
            while (right <= high)
            {
                temp.Add((numIndexes[right].Item1, numIndexes[right].Item2));
                right ++;
            }

            for (int j = 0; j < temp.Count; j++)
            {
                numIndexes[low + j] = (temp[j].Item1, temp[j].Item2);
            }
        }
    }
}