namespace SubwayRouteFinder
{
    public readonly struct Edge
    {
        public readonly string Station;
        public readonly int Time;

        public Edge(string station, int time)
        {
            Station = station;
            Time = time;
        }
    }
    public class SubwayGraph
    {
        private readonly Dictionary<string, List<Edge>> _adjacency = new Dictionary<string, List<Edge>>();

        public SubwayGraph()
        {
            foreach (var segment in SubwayData.Segments)
            {
                AddEdge(segment.From, segment.To, segment.Time);
                AddEdge(segment.To, segment.From, segment.Time);
            }
        }

        private void AddEdge(string from, string to, int time)
        {
            if (!_adjacency.TryGetValue(from, out var edges))
            {
                edges = new List<Edge>();
                _adjacency[from] = edges;
            }
            edges.Add(new Edge(to, time));
        }

        public bool StationExists(string station) => _adjacency.ContainsKey(station);

        public IReadOnlyList<Edge> GetEdges(string station)
        {
            return _adjacency.TryGetValue(station, out var edges) ? edges : Array.Empty<Edge>();
        }

        public List<string> GetAllStationNames()
        {
            return _adjacency.Keys.OrderBy(s => s, StringComparer.Ordinal).ToList();
        }
    }
}
