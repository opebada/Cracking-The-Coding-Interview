using System;
using System.Collections.Generic;
using System.Text;

namespace Chapter2LinkedLists
{
    public class LinkedListsModule
    {
        /// <summary>
        /// Returns the node at the beginning of a loop in a circular linked list
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Node DetectLoop(Node node)
        {
            if (node == null) return null;

            Node faster = node;
            Node slower = node;

            while (slower != null && faster.next != null)
            {
                slower = slower.next;
                faster = faster.next.next;

                if (slower == faster)
                    return null;
            }

            if (faster == null || faster.next == null)
                return null;

            faster = node;

            while (slower != faster)
            {
                faster = faster.next;
                slower = slower.next;
            }

            return faster;
        }
        /// <summary>
        /// Determines if two linked lists intersect and returns the Node where they do
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static Node Intersection(Node one, Node two)
        {
            if (one == null || two == null) return null;

            TailResult result1 = GetLengthAndTail(one);
            TailResult result2 = GetLengthAndTail(two);

            if (result1.Tail != result2.Tail)
                return null;

            Node longer = result1.Length > result2.Length ? one : two;
            Node shorter = result1.Length < result2.Length ? one : two;

            longer = GetKthNode(longer, Math.Abs(result1.Length - result2.Length));

            while (longer != shorter)
            {
                longer = longer.next;
                shorter = shorter.next;
            }

            return longer;
        }

        private class TailResult {
            public Node Tail { get; set; }
            public int Length { get; set; }

            public TailResult(int length, Node tail)
            {
                Tail = tail;
                Length = length;
            }
        }

        private static TailResult GetLengthAndTail(Node node)
        {
            if (node == null) return null;

            int length = 0;
            Node tail = null;

            while (node != null)
            {
                length++;
                node = node.next;

                if (node.next == null)
                    tail = node;
            }

            return new TailResult(length, tail);
        }

        private static Node GetKthNode(Node node, int k)
        {
            if (node == null) return null;

            while (node != null && k != 0)
            {
                node = node.next;
                k--;
            }

            return node;
        }

        /// <summary>
        /// Checks if a linked list is a palindrome
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool IsPalindrome(Node node)
        {
            if (node == null) return false;

            Node reverseReplica = ReverseAndClone(node);

            while (node != null && reverseReplica != null)
            {
                if (node.data != reverseReplica.data)
                    return false;

                node = node.next;
                reverseReplica = reverseReplica.next;
            }

            return true;
        }

        static Node ReverseAndClone(Node node)
        {
            if (node == null) return null;

            Node head = null;

            while (node != null)
            {
                Node n = new Node(node.data);

                n.next = head;
                head = n;

                node = node.next;
            }

            return head;
        }

        /// <summary>
        /// Returns the sum of two given linked lists
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static Node SumLists1(Node l1, Node l2)
        {
            if (l1 == null || l2 == null) return null;

            int length1 = GetLength(l1);
            int length2 = GetLength(l2);

            if (length1 > length2)
                PadList(l2, length1 - length2);
            else if (length1 < length2)
                PadList(l1, length2 - length1);

            Result result = Sum(l1, l2);

            return result.Node;
        }

        private static Result Sum(Node l1, Node l2)
        {
            if (l1 == null || l2 == null) return null;

            Result result = Sum(l1.next, l2.next);

            int sum = (result != null) ? result.Carry : 0;
            sum += l1.data + l2.data;

            int carry = 0;
            if (sum > 9)
            {
                carry = 1;
                sum = sum % 10;
            }
            else
            {
                carry = 0;
            }

            Result newResult = new Result(sum, carry);
            newResult.Node.next = (result != null) ? result.Node : null;

            return newResult;
        }

        private class Result
        {
            public Node Node { get; set; }
            public int Carry { get; set; }

            public Result(int sum, int carry)
            {
                Node = new Node(sum);
                Carry = carry;
            }
        }
        private static void PadList(Node node, int diff)
        {
            for (int i = 0; i < diff; i++)
            {
                Node n = new Node(0);
                n.next = node;
                node = n;
            }
        }

        private static int GetLength(Node node)
        {
            if (node == null) return -1;

            int count = 0;
            Node n = node;

            while (n != null)
            {
                count++;
                n = n.next;
            }

            return count;
        }
        /// <summary>
        /// Returns the sum of two given linked lists in reverse order
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static Node SumLists(Node l1, Node l2)
        {
            if (l1 == null || l2 == null) return null;

            int carry = 0;

            Node result = null;

            while (l1 != null || l2 != null)
            {
                int l1Value = (l1 == null) ? 0 : l1.data;
                int l2Value = (l2 == null) ? 0 : l2.data;

                int sum = l1Value + l2Value + carry;
                if (sum > 9)
                {
                    carry = 1;
                    sum = sum % 10;
                } else
                {
                    carry = 0;
                }
                Node r = new Node(sum);

                if (result == null)
                {
                    result = r;
                }
                else
                {
                    Node n = result;
                    while (n.next != null)
                    {
                        n = n.next;
                    }
                    n.next = r;
                }

                l1 = (l1 == null) ? null : l1.next;
                l2 = (l2 == null) ? null : l2.next;
            }

            return result;
        }

        /// <summary>
        /// Partitions elements of a linked list so that values 
        /// less than the pivot are on the left of it
        /// </summary>
        /// <param name="node"></param>
        /// <param name="pivot"></param>
        /// <returns></returns>
        public static Node Partition(Node node, int pivot)
        {
            if (node == null) return null;

            Node head = node;
            Node tail = node;

            while (node != null)
            {
                Node next = node.next;
                if (node.data < pivot)
                {
                    node.next = head;
                    head = node;
                } else
                {
                    tail.next = node;
                    tail = node;
                }
                node = next;
            }

            tail.next = null;

            return head;
        }

        /// <summary>
        /// Delete a node in the middle of a linkedlist given only a reference to that node
        /// </summary>
        /// <param name="node"></param>
        public static void DeleteMiddleNode(Node node)
        {
            if (node == null || node.next == null) return;

            node.data = node.next.data;

            node.next = node.next.next;
        }

        /// <summary>
        /// Returns the Kth to last element of a linked list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int KthToLast(LinkedList<int> list, int k)
        {
            if (list == null || list.Count == 0) return -1;

            LinkedListNode<int> p1 = list.First;
            LinkedListNode<int> p2 = p1;

            // separated p1 and p2 by k elements
            while (p2 != null && k > 0)
            {
                p2 = p2.Next;
                k--;
            }

            while (p2 != null)
            {
                p1 = p1.Next;
                p2 = p2.Next;
            }

            return p1.Value;
        }
        /// <summary>
        /// Removes duplicates from a linked list
        /// </summary>
        /// <param name="list"></param>
        public static void RemoveDups(LinkedList<int> list)
        {
            if (list == null || list.Count == 0) return;

            LinkedListNode<int> p1 = list.First;

            while (p1 != null)
            {
                LinkedListNode<int> p2 = p1.Next;

                while (p2 != null)
                {
                    LinkedListNode<int> next = p2.Next;

                    if (p1.Value == p2.Value)
                        list.Remove(p2);

                    p2 = next;
                }

                p1 = p1.Next;
            }
        }

        /// <summary>
        /// Removes duplicates from a linked list
        /// </summary>
        /// <param name="list"></param>
        public static void RemoveDuplicates(LinkedList<int> list)
        {
            if (list == null || list.Count == 0) return;

            HashSet<int> map = new HashSet<int>();
            
            LinkedListNode<int> current = list.First;

            while (current != null)
            {
                LinkedListNode<int> next = current.Next;

                if (map.Contains(current.Value))
                {
                    list.Remove(current);
                } else
                {
                    map.Add(current.Value);
                }

                current = next;
            }
        }
    }
}
