using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphConsoleApp
{
    public class Edge
    {
        public Node n1;
        public Node n2;
        public int val;
        public string name;
        public bool ok;
        public int mark;

        public Edge(Node n1, Node n2, int val)
        {
            mark = 0;
            ok = false;
            this.n1 = n1;
            this.n2 = n2;
            this.val = val;
            if (n1 != null && n2 != null)
            {
                name = n1.number.ToString() + " and " + n2.number.ToString() + ":     " + val.ToString();
            }
        }

    }
}
