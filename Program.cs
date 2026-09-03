using System;

namespace SubwayRouteFinder
{
    public class Program
    {
        public static void Main()
        {
            var graph = new SubwayGraph();
            void TestPath(string start, string end)
            {
                var result = SubwayPathFinder.FindShortestPath(graph, start, end);
                Console.WriteLine($"{result.GetRouteString()} | {result.GetFormattedTime()}");
            }

            TestPath("홍대입구", "경복궁");
            TestPath("망원", "마장");
            TestPath("용산", "청량리");
        }
    }
}