using System;

namespace SubwayRouteFinder
{
    public class Program
    {
        public static void Main()
        {
            //그래프 테스트 -> 이웃역 까지
            var graph = new SubwayGraph();
            Console.WriteLine(graph.GetAllStationNames().Count);
            foreach (var e in graph.GetEdges("시청"))
                Console.WriteLine($"{e.Station} {e.Time}");
        }
    }
}