using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter3StacksAndQueues
{
    public class StackWithMin : Stack
    {
        Stack mins;

        public new void Push(int value)
        {
            if (value < Min())
                mins.Push(value);

            base.Push(value);
        }

        public new int Pop()
        {
            int value = base.Pop();

            if (value == Min())
                mins.Pop();

            return value;
        }

        public int Min()
        {
            if (!mins.IsEmpty())
                return Int32.MaxValue;

            return mins.Peek();
        }
    }
}
