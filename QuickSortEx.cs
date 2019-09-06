using System;

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

    public int KthLargestElement(int[] array, int k)
    {
        return FindKthLargest(array, k, 0, array.Length - 1);
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