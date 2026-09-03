namespace SubwayRouteFinder
{
    public class PathResult
    {
        public bool Success;
        public List<string> Route = new List<string>();
        public int TotalSeconds;

        public string GetFormattedTime()
        {
            int minutes = TotalSeconds / 60;
            int seconds = TotalSeconds % 60;
            return $"{minutes}분 {seconds}초";
        }

        public string GetRouteString() => string.Join("->", Route);
    }

    public static class SubwayPathFinder
    {
        public static PathResult FindShortestPath(SubwayGraph graph, string start, string end)
        {
            var dist = new Dictionary<string, int> { [start] = 0 };
            var prev = new Dictionary<string, string>();
            var visited = new HashSet<string>();
            var frontier = new List<string> { start };

            while (frontier.Count > 0)
            {
                string current = frontier[0];
                int currentIndex = 0;
                for (int i = 1; i < frontier.Count; i++)
                {
                    if (dist[frontier[i]] < dist[current])
                    {
                        current = frontier[i];
                        currentIndex = i;
                    }
                }
                frontier.RemoveAt(currentIndex);

                if (visited.Contains(current)) continue;
                visited.Add(current);

                foreach (var edge in graph.GetEdges(current))
                {
                    if (visited.Contains(edge.Station)) continue;

                    int newDist = dist[current] + edge.Time;
                    if (!dist.TryGetValue(edge.Station, out int oldDist) || newDist < oldDist)
                    {
                        dist[edge.Station] = newDist;
                        prev[edge.Station] = current;
                        frontier.Add(edge.Station);
                    }
                }
            }

            var result = new PathResult();
            if (!dist.ContainsKey(end))
            {
                result.Success = false;
                return result;
            }

            var route = new List<string>();
            string? cursor = end;
            while (cursor != null)
            {
                route.Add(cursor);
                prev.TryGetValue(cursor, out cursor);
            }
            route.Reverse();

            result.Success = true;
            result.Route = route;
            result.TotalSeconds = dist[end];
            return result;
        }
    }
}
