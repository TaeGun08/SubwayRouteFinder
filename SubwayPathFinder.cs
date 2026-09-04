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
                if (i > 0) sb.Append(" -> ");

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

            if (startNodes.Count == 0 || !endExists) return result;


            var dist = new Dictionary<StationNode, int>();
            var prev = new Dictionary<StationNode, StationNode?>();
            var arrivedViaTransfer = new Dictionary<StationNode, bool>();
            var visited = new HashSet<StationNode>();
            var nodeQueue = new PriorityQueue<StationNode, int>();

            foreach (var node in startNodes)
            {
                dist[node] = 0;
                prev[node] = null;
                nodeQueue.Enqueue(node, 0);
            }

            while (nodeQueue.Count > 0)
            {
                var current = nodeQueue.Dequeue();

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
                        arrivedViaTransfer[edge.To] = edge.IsTransfer;
                        nodeQueue.Enqueue(edge.To, newDist);
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

            if (bestEnd == null) return result;

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
                bool cameByTransfer = arrivedViaTransfer.TryGetValue(node, out bool wasTransfer) && wasTransfer;

                if (steps.Count > 0 && cameByTransfer)
                {
                    steps[steps.Count - 1].IsTransferHere = true;
                    transferCount++;
                    continue;
                }

                steps.Add(new RouteStep(node.Station, false));
            }

            result.Steps = steps;
            result.TotalSeconds = bestDist;
            result.TransferCount = transferCount;

            return result;
        }
    }
}
