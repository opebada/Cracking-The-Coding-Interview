using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter3StacksAndQueues
{
    public class StacksAndQueues
    {
        public static Stack SortStack(Stack stack)
        {
            if (stack == null || stack.IsEmpty()) return null;

            Stack temp = new Stack();

            while (!stack.IsEmpty())
            {
                int data = stack.Pop();

                while (!temp.IsEmpty() && data > temp.Peek())
                {
                    stack.Push(temp.Pop());
                }

                temp.Push(data);
            }

            return temp;
        }
    }
}
