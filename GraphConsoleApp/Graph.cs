using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphConsoleApp
{
    public class Graph
    {
        public List<Node> nodes;
        public List<Edge> edges;
        public List<Edge> sorted;
        private List<Edge> potential;
        private Node tempNode;
        private Edge tempEdge;

        public Graph()
        {
            nodes = new List<Node>();
            tempNode = new Node(-1);
            tempEdge = new Edge(null, null, -1);
            potential = new List<Edge>();
            nodes = new List<Node>();
            edges = new List<Edge>();
            sorted = new List<Edge>();
            sorted = edges;
            BubbleSort(sorted);
        }

        private void BubbleSort(List<Edge> arr)
        {
            int n = arr.Count;
            for (int i = 1; i <= n - 1; i++)
            {
                for (int j = n - 1; j >= i; j--)
                {
                    if (arr[j - 1].val > arr[j].val)
                    {
                        Edge t = arr[j - 1];
                        arr[j - 1] = arr[j];
                        arr[j] = t;
                    }
                }
            }
        }

        private List<Edge> Prim(int startN, int endN)
        {
            var start = nodes[nodes.FindIndex(n => n.number == startN)];
            var end = nodes[nodes.FindIndex(n => n.number == endN)];
            var track = new List<Edge>();
            tempNode = start;
            tempNode.mark = 1;
            foreach (Edge v in edges)
            {
                if (v.n1 == tempNode)
                {
                    potential.Add(v);
                }
            }
            BubbleSort(potential);

            while (potential.Count != 0)
            {
                tempEdge = potential[0];
                if ((tempEdge.n1.mark == 0 || tempEdge.n2.mark == 0) && (!tempEdge.n1.unavailable && !tempEdge.n2.unavailable))
                {
                    tempEdge.ok = true;
                    if (tempEdge.n1.mark != 1)
                    {
                        tempEdge.n1.mark = 1;
                        tempNode = tempEdge.n1;
                    }
                    if (tempEdge.n2.mark != 1)
                    {
                        tempEdge.n2.mark = 1;
                        tempNode = tempEdge.n2;
                    }
                    track.Add(tempEdge);
                    if (tempEdge.n2.number == end.number)
                        break;
                    potential.Remove(tempEdge);
                    foreach (Edge edge in edges)
                    {
                        if (edge.n1.number == tempNode.number && !edge.ok && !potential.Contains(edge))
                        {
                            potential.Add(edge);
                        }
                    }
                    BubbleSort(potential);
                }
                else
                {
                    potential.Remove(tempEdge);
                }
            }
            return track;
        }

        public string FindMinTimeRoad(int startN, int endN)
        {
            var track = Prim(startN, endN);
            string output = "Fastest track: ";
            foreach (Edge edge in track)
                output += edge.n1.number.ToString() + "-";
            output += track.Last().n2.number.ToString();
            return output;
        }

        public void MarkUnavailableNodes(List<Bus> buses, TimeOnly startTime)
        {
            foreach (Node node in nodes)
            {
                bool unavailable = true;
                
                foreach (int busId in node.buses)
                {
                    if (buses[busId].startTime < startTime)
                        unavailable = unavailable && false;
                }
                node.unavailable = unavailable;
            }
        }

        public (string, string) FindCheapestRoad(int startN, int endN, List<Bus> buses)
        {
            string output = "Cheapest track: ";

            var start = nodes[nodes.FindIndex(n => n.number == startN)];
            var end = nodes[nodes.FindIndex(n => n.number == endN)];

            int sum = int.MaxValue;
            int busId = 0;

            for (int i = 0; i < start.buses.Count; i++)
            {
                for (int j = 0; j < end.buses.Count; j++)
                {
                    if (start.buses[i] == end.buses[j])
                    {
                        if (sum > buses[i].price)
                        {
                            sum = buses[i].price;
                            busId = i;
                        }
                    }
                }
            }

            var index1 = buses[busId].Stops.IndexOf(startN);
            var index2 = buses[busId].Stops.IndexOf(endN);

            for (int i = index1; i <= index2; i++)
                output += i != index2 ? buses[busId].Stops[i].ToString() + "-" : buses[busId].Stops[i].ToString();

            return ("Cheapest route cost: " + sum.ToString(), output);
        }
    }
}
