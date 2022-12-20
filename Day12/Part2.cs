using Common;

public class Part2 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        (var map, var start, var goal) = Part1.ReadMap(inputLines);
        var routeCostMap = new int[map.GetLength(0), map.GetLength(1)];

        Queue<Point> toCheck = new Queue<Point>(new[] { goal });
        while (toCheck.Any())
        {
            var testPoint = toCheck.Dequeue();

            foreach (var neightbour in Part1.GetValidNextPoints(map, testPoint))
            {
                if(routeCostMap[neightbour.X, neightbour.Y] > 0) continue;
                routeCostMap[neightbour.X, neightbour.Y] = routeCostMap[testPoint.X, testPoint.Y] + 1;
                toCheck.Enqueue(neightbour);
            }
        }

        List<int> routeCosts = new();
        for(int x = 0; x < map.GetLength(0); x++)
        for (int y = 0; y < map.GetLength(1); y++)
        {
            if (map[x, y] == 'a' && routeCostMap[x,y] > 0)
            {
                routeCosts.Add(routeCostMap[x,y]);
            }
        }

        return routeCosts.Min();
    }
}