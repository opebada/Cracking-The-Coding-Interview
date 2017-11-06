using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4_TreesAndGraphs
{
    public class TreeNode<T>
    {
        public T Data { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
        public TreeNode<T> Parent { get; set; }
        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
