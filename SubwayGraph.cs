namespace SubwayRouteFinder
{
    public readonly struct StationNode
    {
        public readonly string Station;
        public readonly int Line;

        public StationNode(string station, int line)
        {
            Station = station;
            Line = line;
        }

        public override bool Equals(object? obj)
        {
            return obj is StationNode other
                && Station == other.Station
                && Line == other.Line;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Station, Line);
        }
    }

    public readonly struct Edge
    {
        public readonly StationNode To;
        public readonly int Time;
        public readonly bool IsTransfer;

        public Edge(StationNode to, int time, bool isTransfer)
        {
            To = to;
            Time = time;
            IsTransfer = isTransfer;
        }
    }

    public class SubwayGraph
    {
        private readonly Dictionary<StationNode, List<Edge>> _adjacency = new();
        private readonly Dictionary<string, HashSet<int>> _stationLines = new();

        public SubwayGraph()
        {
            foreach (var segment in SubwayData.Segments)
            {
                var a = new StationNode(segment.From, segment.Line);
                var b = new StationNode(segment.To, segment.Line);

                AddEdge(a, b, segment.Time, isTransfer: false);
                AddEdge(b, a, segment.Time, isTransfer: false);

                RegisterLine(segment.From, segment.Line);
                RegisterLine(segment.To, segment.Line);
            }

            foreach (var pair in _stationLines)
            {
                var lines = pair.Value.ToList();
                
                if (lines.Count < 2) continue;

                for (int i = 0; i < lines.Count; i++)
                {
                    for (int j = i + 1; j < lines.Count; j++)
                    {
                        var nodeA = new StationNode(pair.Key, lines[i]);
                        var nodeB = new StationNode(pair.Key, lines[j]);

                        AddEdge(nodeA, nodeB, SubwayData.TransferPenaltySeconds, isTransfer: true);
                        AddEdge(nodeB, nodeA, SubwayData.TransferPenaltySeconds, isTransfer: true);
                    }
                }
            }
        }

        private void AddEdge(StationNode from, StationNode to, int time, bool isTransfer)
        {
            if (!_adjacency.TryGetValue(from, out var edges))
            {
                edges = new List<Edge>();
                _adjacency[from] = edges;
            }

            edges.Add(new Edge(to, time, isTransfer));
        }

        private void RegisterLine(string station, int line)
        {
            if (!_stationLines.TryGetValue(station, out var lines))
            {
                lines = new HashSet<int>();
                _stationLines[station] = lines;
            }

            lines.Add(line);
        }

        public bool StationExists(string station) => _stationLines.ContainsKey(station);

        public IReadOnlyList<Edge> GetEdges(StationNode node)
        {
            return _adjacency.TryGetValue(node, out var edges) ? edges : Array.Empty<Edge>();
        }

        public List<int> GetLines(string station)
        {
            return _stationLines.TryGetValue(station, out var lines) ? lines.OrderBy(l => l).ToList() : new();
        }

        public List<string> GetAllStationNames()
        {
            return _stationLines.Keys.OrderBy(s => s, StringComparer.Ordinal).ToList();
        }
    }
}
