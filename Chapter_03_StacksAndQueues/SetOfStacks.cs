using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter3StacksAndQueues
{
    public class SetOfStacks
    {
        private List<Stack> stacks;
        private List<int> stackSizes;
        private int threshold;

        public SetOfStacks(int threshold)
        {
            this.threshold = threshold;
            stacks = new List<Stack>();
            stackSizes = new List<int>();
        }

        private void AddNewStack()
        {
            stacks.Add(new Stack());
            stackSizes.Add(0);
        }

        public void Push(int data)
        {
            if (IsEmpty() || stackSizes[stackSizes.Count - 1] == threshold)
            {
                AddNewStack();
            }

            // push data to last stack
            stacks[stacks.Count - 1].Push(data);
            stackSizes[stackSizes.Count - 1] += 1;
        }

        public int Pop()
        {
            Stack last = stacks[stacks.Count - 1];
            int result = last.Pop();
            stackSizes[stackSizes.Count - 1] -= 1;

            if (last.IsEmpty())
            {
                stacks.Remove(last);
            }

            return result;
        }

        public int PopAt(int index)
        {
            if (index > stacks.Count - 1) throw new IndexOutOfRangeException();

            Stack stack = stacks[index];
            int result = stack.Pop();

            if (stack.IsEmpty())
                stacks.Remove(stack);

            return result;
        }

        public int Peek()
        {
            int result = stacks[stacks.Count - 1].Peek();

            return result;
        }

        public bool IsEmpty()
        {
            if (stacks.Count == 0)
                return true;

            return false;
        }
    }
}
