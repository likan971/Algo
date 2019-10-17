using System;
using System.Linq;

namespace Algo
{
    public abstract class Utils
    {
        public static void PrintArray(int[] array)
        {
            foreach (int a in array)
            {
                Console.Write(a.ToString() + " ");
            }
        }

        public static void Print2dArray(int[][] arrays)
        {
            Console.Write("[ ");
            for (int i = 0; i < arrays.Length; i ++)
            {
                Console.Write("[");
                for (int j = 0; j < arrays[i].Length; j ++)
                {
                    Console.Write(arrays[i][j]);
                    if (j < arrays[i].Length - 1)
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("]");
                if (i < arrays.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.Write(" ]");
        }

        public static void PrintLinkedList(ListNode head)
        {
            while (head != null)
            {
                System.Console.Write($"{head.val} --> ");
                head = head.next;
            }
            System.Console.Write("NULL");
            System.Console.WriteLine();
        }

        public static ListNode CreateLinkedList(int nodeCount, bool cycled = false)
        {
            if (nodeCount <= 0)
            {
                return null;
            }

            var head = new ListNode(1);
            var last = head;
            for (int i = 1; i < nodeCount; i ++)
            {
                var node = new ListNode(i+1);
                last.next = node;
                last = node;
            }
            if (cycled)
            {
                last.next = head;
            }
            return head;
        }

    }
}