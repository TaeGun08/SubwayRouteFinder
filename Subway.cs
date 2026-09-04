namespace SubwayRouteFinder
{
    public class Subway
    {
        private readonly SubwayGraph _graph = new();

        public void Run()
        {
            string start;
            string end;

            while (true)
            {
                start = ReadStation("출발");
                end = ReadStation("도착");

                if (IsValid(start, end)) break;

                Console.WriteLine();
            }

            var result = SubwayPathFinder.FindShortestPath(_graph, start, end);

            Console.WriteLine();
            Console.WriteLine($"[탐색 결과] {start} -> {end}");
            Console.WriteLine($"이동 경로 : {result.GetRouteString()}");
            Console.WriteLine($"총 소요 시간 : {result.GetFormattedTime()}");
        }

        private string ReadStation(string label)
        {
            Console.Write($"{label} 역을 입력해주세요 : ");
            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        private bool IsValid(string start, string end)
        {
            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
            {
                Console.WriteLine("출발역과 도착역을 모두 입력해주세요.");
                return false;
            }

            if (!_graph.StationExists(start))
            {
                Console.WriteLine($"'{start}'은(는) 존재하지 않는 역입니다. 다시 입력해주세요.");
                return false;
            }

            if (!_graph.StationExists(end))
            {
                Console.WriteLine($"'{end}'은(는) 존재하지 않는 역입니다. 다시 입력해주세요.");
                return false;
            }

            if (start == end)
            {
                Console.WriteLine("출발역과 도착역이 같습니다. 다시 입력해주세요.");
                return false;
            }

            return true;
        }
    }
}
