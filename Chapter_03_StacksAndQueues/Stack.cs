using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter3StacksAndQueues
{
    public class Stack
    {
        private StackNode top;

        public int Peek()
        {
            if (top == null) throw new Exception("Empty Stack");

            return top.Data;
        }
        public void Push(int data)
        {
            StackNode n = new StackNode(data);
            n.Next = top;
            top = n;
        }

        public int Pop()
        {
            if (top == null)
                throw new Exception("Empty Stack");

            int result = top.Data;
            top = top.Next;

            return result;
        }

        public bool IsEmpty()
        {
            if (top == null)
                return true;

            return false;
        }
    }

    public class StackNode
    {
        public int Data { get; set; }
        public StackNode Next { get; set; }

        public StackNode(int data)
        {
            Data = data;
        }
    }
}
