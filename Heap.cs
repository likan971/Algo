using System.Collections.Generic;
using System;

namespace Algo
{
    public class HeapTest
    {
        private MaxHeap maxHeap = new MaxHeap();
        private MinHeap minHeap = new MinHeap();

        public void TestMaxHeapAdd()
        {
            maxHeap.Offer(10);
            maxHeap.Offer(12);
            maxHeap.Offer(13);
            maxHeap.Offer(9);
            maxHeap.Offer(5);
            maxHeap.Offer(15);
            Console.WriteLine("current top is: " + maxHeap.Peek());
        }

        public void TestMinHeapAdd()
        {
            minHeap.Offer(10);
            minHeap.Offer(12);
            minHeap.Offer(13);
            minHeap.Offer(9);
            minHeap.Offer(5);
            minHeap.Offer(15);
            Console.WriteLine("current top is: " + minHeap.Peek());
        }

        public void TestMaxHeapRemoveTop()
        {
            TestMaxHeapAdd();
            for (int i = 0; i < 3; i ++)
            {
                Console.WriteLine(maxHeap.Poll() + " was removed");
                Console.WriteLine("next top is: " + maxHeap.Peek());
            }
        }

        public void TestMinHeapRemoveTop()
        {
            TestMinHeapAdd();
            for (int i = 0; i < 4; i ++)
            {
                Console.WriteLine(minHeap.Poll() + " was removed");
                Console.WriteLine("next top is: " + minHeap.Peek());
            }
        }
    }

    public abstract class Heap<T> : IComparer<T>
    {
        private List<T> list;

        public abstract int Compare(T x, T y);

        protected Heap(int initCapacity = 10)
        {
            list = new List<T>(initCapacity);
            list.Add(default(T)); // binary tree is better start with index == 1, so the 0th position is filled up at the beginning
        }

        public void Offer(T element)
        {
            list.Add(element);
            int index = list.Count - 1; // index start with 1
            int parentIndex = index / 2;

            while (parentIndex > 0 && Compare(element, list[parentIndex]) > 0)
            {
                swap(list, index, parentIndex);
                index = parentIndex;
                parentIndex = index / 2;
            }
        }

        public T Poll()
        {
            if (TryPeek(out T topElement))
            {
                if (Size() == 1) // only one element exist
                {
                    list.RemoveAt(1);
                    return topElement;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("heap is empty");
            }

            var index = 1;
            list[index] = list[list.Count - 1]; // replace the top element with the last one
            list.RemoveAt(list.Count - 1);      // remove the last element

            while (index < list.Count - 1)
            {
                var maxChildIndex = index;
                var leftChildIndex = index * 2;
                var rightChildIndex = index * 2 + 1;
                if (leftChildIndex < list.Count && Compare(list[leftChildIndex], list[index]) > 0)
                {
                    maxChildIndex = leftChildIndex;
                }
                if (rightChildIndex < list.Count && Compare(list[rightChildIndex], list[maxChildIndex]) > 0)
                {
                    maxChildIndex = rightChildIndex;
                }

                if (maxChildIndex > index)
                {
                    swap(list, index, maxChildIndex);
                    index = maxChildIndex;
                }
                else
                {
                    break;
                }
            }

            return topElement;
        }

        public bool TryPoll(out T element)
        {
            try
            {
                element = Poll();
                return true;
            }
            catch
            {
                element = default(T);
                return false;
            }
        }

        public T Peek()
        {
            if (list.Count > 1)
            {
                return list[1];
            }
            throw new ArgumentOutOfRangeException("Heap is empty");
        }

        public bool TryPeek(out T element)
        {
            if (list.Count > 1)
            {
                element = list[1];
                return true;
            }

            element = default(T);
            return false;
        }

        public void Clear()
        {
            list.RemoveRange(1, list.Count - 1);
        }

        public bool Contains(T element)
        {
            return list.Contains(element);
        }

        public int Size()
        {
            return list.Count - 1;
        }

        public bool IsEmpty()
        {
            return Size() <= 0;
        }

        private void swap(List<T> list, int indexA, int indexB)
        {
            var temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }
    }

    public class MaxHeap : Heap<int>
    {
        public MaxHeap(int initCapacity = 10) : base(initCapacity)
        {

        }

        public override int Compare(int x, int y)
        {
            if (x > y) return 1;
            return -1;
        }
    }

    public class MinHeap : Heap<int>
    {
        public MinHeap(int initCapacity = 10) : base(initCapacity)
        {

        }

        public override int Compare(int x, int y)
        {
            if (x < y) return 1;
            return -1;
        }
    }
}