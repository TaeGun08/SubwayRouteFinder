using System.Text;

namespace SubwayRouteFinder
{
    public class RouteStep
    {
        public string Station;
        public bool IsTransferHere;

        public RouteStep(string station, bool isTransferHere)
        {
            Station = station;
            IsTransferHere = isTransferHere;
        }
    }

    public class PathResult
    {
        public bool Success;
        public List<RouteStep> Steps = new();
        public int TotalSeconds;
        public int TransferCount;

        public string GetFormattedTime()
        {
            int minutes = TotalSeconds / 60;
            int seconds = TotalSeconds % 60;

            return $"{minutes}분 {seconds}초";
        }

        public string GetRouteString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Steps.Count; i++)
            {
                if (i > 0) sb.Append("->");

                sb.Append(Steps[i].Station);

                if (Steps[i].IsTransferHere) sb.Append("(환승)");
            }

            return sb.ToString();
        }
    }

    public static class SubwayPathFinder
    {
        public static PathResult FindShortestPath(SubwayGraph graph, string start, string end)
        {
            var result = new PathResult();

            var startNodes = graph.GetLines(start).Select(line => new StationNode(start, line)).ToList();
            bool endExists = graph.GetLines(end).Count > 0;

            if (startNodes.Count == 0 || !endExists)
            {
                result.Success = false;
                return result;
            }

            var dist = new Dictionary<StationNode, int>();
            var prev = new Dictionary<StationNode, StationNode?>();
            var visited = new HashSet<StationNode>();
            var frontier = new List<StationNode>();

            foreach (var node in startNodes)
            {
                dist[node] = 0;
                prev[node] = null;
                frontier.Add(node);
            }

            while (frontier.Count > 0)
            {
                int bestIndex = 0;
                var current = frontier[0];

                for (int i = 1; i < frontier.Count; i++)
                {
                    if (dist[frontier[i]] < dist[current])
                    {
                        current = frontier[i];
                        bestIndex = i;
                    }
                }

                frontier.RemoveAt(bestIndex);

                if (visited.Contains(current)) continue;
                visited.Add(current);

                foreach (var edge in graph.GetEdges(current))
                {
                    if (visited.Contains(edge.To)) continue;

                    int newDist = dist[current] + edge.Time;

                    if (!dist.TryGetValue(edge.To, out int oldDist) || newDist < oldDist)
                    {
                        dist[edge.To] = newDist;
                        prev[edge.To] = current;
                        frontier.Add(edge.To);
                    }
                }
            }

            StationNode? bestEnd = null;
            int bestDist = int.MaxValue;

            foreach (var kv in dist)
            {
                if (kv.Key.Station == end && kv.Value < bestDist)
                {
                    bestDist = kv.Value;
                    bestEnd = kv.Key;
                }
            }

            if (bestEnd == null)
            {
                result.Success = false;
                return result;
            }

            var nodePath = new List<StationNode>();
            StationNode? currentNode = bestEnd;

            while (currentNode != null)
            {
                nodePath.Add(currentNode.Value);
                currentNode = prev[currentNode.Value];
            }

            nodePath.Reverse();

            var steps = new List<RouteStep>();
            int transferCount = 0;

            foreach (var node in nodePath)
            {
                if (steps.Count > 0 && steps[steps.Count - 1].Station == node.Station)
                {
                    steps[steps.Count - 1].IsTransferHere = true;
                    transferCount++;
                    continue;
                }
                steps.Add(new RouteStep(node.Station, false));
            }

            result.Success = true;
            result.Steps = steps;
            result.TotalSeconds = bestDist;
            result.TransferCount = transferCount;

            return result;
        }
    }
}
