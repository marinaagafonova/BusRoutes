using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraphConsoleApp
{
    public class ParseFile
    {
        int NBuses;
        public List<Bus> buses;
        public Graph Parse(string path)
        {
            string[] lines = File.ReadAllLines(path);
            NBuses = Convert.ToInt32(lines[0]);
            var times = lines[2].Split(' ');
            var prices = lines[3].Split(' ');
            buses = new List<Bus>();
            var graph = new Graph();

            for (int i=0; i<NBuses; i++)
            {
                var stops = lines[4 + i * 2].Split(' ').ToList().Select(int.Parse).ToList();
                stops = stops.Skip(1).ToList();
                var timesStops = lines[5 + i * 2].Split(' ').ToList().Select(int.Parse).ToList();
                for (int j = 1; j < stops.Count; j++)
                {
                    var nodeIndex = graph.nodes.FindIndex(node => node.number == stops[j - 1]);
                    if (nodeIndex < 0)
                    {
                        var node = new Node(stops[j - 1]);
                        node.buses.Add(i);
                        var child = new Node(stops[j]);
                        node.children.Add(child);
                        graph.nodes.Add(node);
                        graph.edges.Add(new Edge(node, child, timesStops[j - 1]));
                    }
                    else
                    {
                        graph.nodes[nodeIndex].buses.Add(i);
                        if (graph.nodes.FindIndex(node => node.number == stops[j]) < 0)
                        {
                            var child = new Node(stops[j]);
                            graph.nodes[nodeIndex].children.Add(child);
                            graph.edges.Add(new Edge(graph.nodes[nodeIndex], child, timesStops[j - 1]));
                        }
                    }                
                }
                var lastnode = new Node(stops[stops.Count - 1]);
                lastnode.buses.Add(i);
                lastnode.children.Add(graph.nodes[0]);
                graph.nodes.Add(lastnode);
                graph.edges.Add(new Edge(lastnode, graph.nodes[0], timesStops[stops.Count - 1]));

                buses.Add(new Bus()
                {
                    startTime = TimeOnly.Parse(times[i]),
                    price = Convert.ToInt32(prices[i]),
                    Stops = stops,
                    Time = timesStops,
                });
            }
            return graph;
        }
    }
}
