using System.Collections.Generic;

namespace Algo
{
    public class ListNode
    {
      public int val;
      public ListNode next;
      public ListNode(int x) { val = x; }
    }

    public class LinkedListTest
    {
        private readonly LinkedList list = new LinkedList();

        public ListNode CreateLinkedList(int nodeCount, bool cycled = false)
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

        public void PrintLinkedList(ListNode head)
        {
            while (head != null)
            {
                System.Console.Write($"{head.val} --> ");
                head = head.next;
            }
            System.Console.Write("NULL");
            System.Console.WriteLine();
        }


        #region Reverse Singly Linked List Test
        public void ReverseListTest()
        {
            var list1 = CreateLinkedList(1);
            var list2 = CreateLinkedList(2);
            var list3 = CreateLinkedList(3);
            var list4 = CreateLinkedList(4);
            var list5 = CreateLinkedList(5);
            var list6 = CreateLinkedList(6);
            PrintLinkedList(list.ReverseList(list1));
            PrintLinkedList(list.ReverseList(list2));
            PrintLinkedList(list.ReverseList(list3));
            PrintLinkedList(list.ReverseList(list4));
            PrintLinkedList(list.ReverseList(list5));
            PrintLinkedList(list.ReverseList(list6));
        }

        public void ReverseListExTest()
        {
            var list1 = CreateLinkedList(1);
            var list2 = CreateLinkedList(2);
            var list3 = CreateLinkedList(3);
            var list4 = CreateLinkedList(4);
            var list5 = CreateLinkedList(5);
            var list6 = CreateLinkedList(6);
            PrintLinkedList(list.ReverseListEx(list1));
            PrintLinkedList(list.ReverseListEx(list2));
            PrintLinkedList(list.ReverseListEx(list3));
            PrintLinkedList(list.ReverseListEx(list4));
            PrintLinkedList(list.ReverseListEx(list5));
            PrintLinkedList(list.ReverseListEx(list6));
        }

        public void ReverseListReTest()
        {
            var list1 = CreateLinkedList(1);
            var list2 = CreateLinkedList(2);
            var list3 = CreateLinkedList(3);
            var list4 = CreateLinkedList(4);
            var list5 = CreateLinkedList(5);
            var list6 = CreateLinkedList(6);
            PrintLinkedList(list.ReverseListRe(list1));
            PrintLinkedList(list.ReverseListRe(list2));
            PrintLinkedList(list.ReverseListRe(list3));
            PrintLinkedList(list.ReverseListRe(list4));
            PrintLinkedList(list.ReverseListRe(list5));
            PrintLinkedList(list.ReverseListRe(list6));
        }
        #endregion

        public void CycledLinkedListTest()
        {
            var isCycled = list.CycledLinkedList(CreateLinkedList(3, true));
            System.Console.WriteLine(isCycled);
        }

        public void MergeTwoListsTest()
        {
            var l1 = CreateLinkedList(5);
            var l2 = CreateLinkedList(3);
            var result = list.MergeTwoLists(l1, l2);
            PrintLinkedList(result);
        }
    }

    public class LinkedList
    {
        #region Reverse Singly Linked List
        /// <summary>
        /// use three pointers (head, mid, last) to iterate through the list
        /// </summary>
        public ListNode ReverseList(ListNode head)
        {
            if (head == null || head.next == null) //0 or 1 node
            {
                return head;
            }
            if (head.next.next == null) //2 nodes
            {
                var newHead = head.next;
                head.next = null;
                newHead.next = head;
                return newHead;
            }

            var mid = head.next;
            var last = mid.next;
            head.next = null;
            while (true)
            {
                mid.next = head;
                head = last.next;
                last.next = mid;
                if (head == null)
                {
                    return last;
                }
                mid = head.next;
                head.next = last;
                if (mid == null)
                {
                    return head;
                }
                last = mid.next;
                if (last == null)
                {
                    mid.next = head;
                    return mid;
                }
            }
        }

        /// <summary>
        /// use two pointers + a temp pointer to iterate through the list
        /// </summary>
        public ListNode ReverseListEx(ListNode head)
        {
            if (head == null || head.next == null) //0 or 1 node
            {
                return head;
            }

            var p = head.next;
            var temp = p;
            head.next = null;
            while (p != null)
            {
                temp = p.next;
                p.next = head;
                head = p;
                p = temp;
            }
            return head;
        }

        /// <summary>
        /// the recursive way of ReverseListEx
        /// </summary>
        public ListNode ReverseListRe(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            var last = ReverseListRe(head.next); //move to the last node
            // head is now the previous node of the last node
            head.next.next = head;
            head.next = null;
            return last;
        }
        #endregion

        public bool CycledLinkedList(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return false;
            }

            var set = new HashSet<ListNode>();
            while (head != null)
            {
                if (!set.Add(head))
                {
                    return true;
                }
                head = head.next;
            }
            return false;
        }

        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            var sentinel = new ListNode(0);
            var current = sentinel;
            while (l1 != null && l2 != null)
            {
                ListNode node;
                if (l1.val >= l2.val)
                {
                    node = new ListNode(l2.val);
                    l2 = l2.next;
                }
                else
                {
                    node = new ListNode(l1.val);
                    l1 = l1.next;
                }
                current.next = node;
                current = node;
            }
            if (l1 != null)
            {
                current.next = l1;
            }
            if (l2 != null)
            {
                current.next = l2;
            }

            return sentinel.next;
        }

        /// <summary>
        /// use two pointers (p1,p2), let p2 be Nth nodes ahead of p1, and forward them one step each towards the end
        /// </summary>
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            if (head == null || head.next == null)
            {
                return null;
            }

            var p1 = head;
            var p2 = head;
            for (int i = 0; i < n; i++)
            {
                p2 = p2.next;
            }

            while (p2 != null && p2.next != null)
            {
                p1 = p1.next;
                p2 = p2.next;
            }

            // remove head
            if (p1 == head && p2 == null)
            {
                return head.next;
            }

            // p1 is in the (n-1)th node
            p1.next = p1.next.next;
            return head;
        }

        /// <summary>
        /// fast and slow pointer
        /// <summary>
        public ListNode MiddleNode(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            var p1 = head;
            var p2 = head;
            while (p2 != null && p2.next != null)
            {
                p1 = p1.next;
                p2 = p2.next.next;
            }

            return p1;
        }
    }
}