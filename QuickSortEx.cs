using System;
using System.Linq;
using System.Collections.Generic;

namespace Algo
{
    public class QuickSortExTest
    {
        QuickSortEx qs = new QuickSortEx();

        public void TestQuickSort()
        {
            var sortedArray = qs.QuickSort(new int[] {5,4,3,2,1}, 0, 4);
            foreach(var e in sortedArray)
            {
                Console.Write(e + " ");
            }
        }

        public void TestKthLargestElement()
        {
            var element = qs.KthLargestElement(new int[] {3,2,3,1,2,4,5,5,6}, 4);
            Console.Write(element);
        }

        public void TestMergeIntervals()
        {
            Utils.Print2dArray(qs.MergeInterval(new int[][]{new int[]{4,8},new int[]{3,5},new int[]{1,7},new int[]{9,11}}));
        }
    }

    ///<summary>
    /// Implement quick sort
    ///</summary>
    public class QuickSortEx
    {
        public int[] QuickSort(int[] array, int start, int end)
        {
            if (start < end)
            {
                var p = findPivot(array, start, end);
                QuickSort(array, start, p - 1);
                QuickSort(array, p + 1, end);
            }
            return array;
        }

        ///<summary>
        /// 找到无序数组的第k大元素（用快速排序思想）
        ///</summary>
        public int KthLargestElement(int[] array, int k)
        {
            return FindKthLargest(array, k, 0, array.Length - 1);
        }

        ///<summary>
        /// Leetcode 56
        ///</summary>
        public int[][] MergeInterval(int[][] intervals)
        {
            intervals = intervals.OrderBy(x => x[0]).ToArray();

            var result = new List<int[]>();
            foreach (var array in intervals)
            {
                var lastArray = result.LastOrDefault();
                if (lastArray == null)
                {
                    result.Add(new int[] {array[0], array[1]});
                    continue;
                }
                if (array[0] <= lastArray[1])
                {
                    if (array[1] > lastArray[1])
                    {
                        lastArray[1] = array[1];
                    }
                }
                else
                {
                    result.Add(new int[]{array[0], array[1]});
                }
            }

            return result.ToArray();
        }

        private int FindKthLargest(int[] array, int k, int start, int end)
        {
            if (start < end)
            {
                var p = findPivotEx(array, start, end);
                if (k == p + 1)
                {
                    return array[p];
                }
                else if (k < p + 1)
                {
                    return FindKthLargest(array, k, start, p - 1);
                }
                else
                {
                    return FindKthLargest(array, k, p + 1, end);
                }
            }
            return array[start];
        }

        private int findPivotEx(int[] array, int start, int end)
        {
            var p = end;
            var pivot = array[p];
            int i = start, j = start;

            while (j < end)
            {
                if (array[j] > pivot) //descending
                {
                    if (i != j)
                    {
                        swap(array, i, j);
                    }
                    i ++;
                }
                j++;
            }
            swap(array, i, p);
            return i;
        }

        private int findPivot(int[] array, int start, int end)
        {
            //use the last element as pivot
            var p = end;
            var pivot = array[p];
            int i = start,j = start;

            while (j < end)
            {
                if (array[j] < pivot) //ascending
                {
                    if (i != j)
                    {
                        swap(array, i, j);
                    }
                    i++;
                }
                j++;
            }

            swap(array, i, end);
            return i;
        }

        private void swap(int[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}