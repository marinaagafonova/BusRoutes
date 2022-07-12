// See https://aka.ms/new-console-template for more information

using GraphConsoleApp;

Console.WriteLine("Enter path to file");
string path = Console.ReadLine();

Console.WriteLine("start point:");
int startNodeN = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("end point:");
int endNodeN = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("start time");
TimeOnly startTime = TimeOnly.Parse(Console.ReadLine());

Graph graph = new Graph();

var parser = new ParseFile();
graph = parser.Parse(path);
var buses = parser.buses;
graph.MarkUnavailableNodes(buses, startTime);
var result = graph.FindMinTimeRoad(startNodeN, endNodeN);
var (cost, route) = graph.FindCheapestRoad(startNodeN, endNodeN, buses, startTime);

Console.WriteLine();
Console.WriteLine(result);
Console.WriteLine(cost);
Console.WriteLine(route);