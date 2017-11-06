using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter3StacksAndQueues
{
    public class QueueViaStacks
    {
        private Stack stackIn;
        private Stack stackOut;

        public QueueViaStacks()
        {
            stackIn = new Stack();
            stackOut = new Stack();
        }

        public void Add(int data)
        {
            StackNode node = new StackNode(data);

            stackIn.Push(data);
        }

        public int Pop()
        {
            if (stackOut.IsEmpty())
            {
                TurnOver();
            }

            return stackOut.Pop();
        }

        public int Peek()
        {
            if (stackOut.IsEmpty())
            {
                TurnOver();
            }

            return stackOut.Peek();
        }

        private void TurnOver()
        {
            while (!stackIn.IsEmpty())
            {
                stackOut.Push(stackIn.Pop());
            }
        }

        public bool IsEmpty()
        {
            return (stackIn.IsEmpty() && stackOut.IsEmpty()); 
        }
    }
}
