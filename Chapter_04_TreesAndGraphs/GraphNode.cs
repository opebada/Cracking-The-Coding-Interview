using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4_TreesAndGraphs
{
    public class GraphNode <T>
    {
        public GraphNode() { }
        public GraphNode(T data)
        {
            Data = data;
            Parents = new List<GraphNode<T>>();
            Children = new List<GraphNode<T>>();
        }
        public T Data { get; set; }
        public List<GraphNode<T>> Parents { get; set; }
        public List<GraphNode<T>> Children { get; set; }
        public bool Visited { get; set; }
        public bool Marked { get; set; }
        public override string ToString()
        {
            return Data.ToString();
        }
    }

    public class Edge
    {
        public GraphNode<string> Out { get; set; }
        public GraphNode<string> In { get; set; }
        public override string ToString()
        {
            return Out.ToString() + "->" + In.ToString();
        }
    }
}
