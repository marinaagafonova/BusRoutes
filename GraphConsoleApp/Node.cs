using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphConsoleApp
{
    public class Node
    {
        public int number;
        public List<Node> children;
        public int mark;
        public List<int> buses;
        public bool unavailable;

        public Node(int number)
        {
            children = new List<Node>();
            this.number = number;
            unavailable = false;
            buses = new List<int>();
        }
    }
}
