using System;
using System.Collections.Generic;
using System.Text;

namespace Chapter2LinkedLists
{
    public class Node
    {
        public int data { get; set; }
        public Node next { get; set; }

        public Node(int d)
        {
            data = d;
        }

        public Node(List<int> list)
        {
            AppendToTail(list);
        }

        public void AppendToTail(int d)
        {
            Node newNode = new Node(d);
            Node n = this;

            while (n.next != null)
            {
                n = n.next;
            }

            n.next = newNode;
        }

        public void AppendToTail(List<int> list)
        {
            Node n = this;
            data = list[0];

            while (n.next != null)
            {
                n = n.next;
            }

            for (int item = 1; item < list.Count; item++)
            {
                Node newNode = new Node(list[item]);
                n.next = newNode;
                n = n.next;
            }
        }

        public Node Find(int num)
        {
            Node n = this;
            while (n != null)
            {
                if (n.data == num)
                    return n;

                n = n.next;
            }

            return null;
        }
    }
}
