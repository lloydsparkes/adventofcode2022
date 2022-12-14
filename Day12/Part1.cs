using Common;

public record Point(int X, int Y);

public class Part1 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        (var map, var startPoint) = ReadMap(inputLines);

        return FindShortestRoute(map, startPoint, 0, new List<Point>());
    }

    public int FindShortestRoute(char[,] map, Point sp, int routeLength, List<Point> visited)
    {
        if (map[sp.X, sp.Y] == 'E') return routeLength;
        
        var pointsToCheck = GetValidPoints(map, sp, visited);
        if (!pointsToCheck.Any()) return int.MaxValue;

        var newVisited = new List<Point>(visited);
        newVisited.Add(sp);
        return pointsToCheck.Select(p => FindShortestRoute(map, p, routeLength + 1, newVisited)).Min();
    }

    public IEnumerable<Point> GetValidPoints(char[,] map, Point sp, List<Point> visited)
    {
        var silbingPoints = new[]
        {
            new Point(sp.X, sp.Y + 1),
            new Point(sp.X, sp.Y - 1),
            new Point(sp.X + 1, sp.Y),
            new Point(sp.X - 1, sp.Y),
        };
        char[] charsAllowed;

        if (map[sp.X, sp.Y] == 'S')
        {
            charsAllowed = new []{'E', 'a'};
        }
        else
        {
            charsAllowed = new[] { 'E', map[sp.X, sp.Y], (char)(map[sp.X, sp.Y] + 1) };
        }

        return silbingPoints.Where(p => InBounds(map, p) && !visited.Contains(p) && charsAllowed.Contains(map[p.X, p.Y]));
    }

    public bool InBounds(char[,] map, Point sp)
    {
        return (sp.X >= 0 && sp.X < map.GetLength(0))
               && (sp.Y >= 0 && sp.Y < map.GetLength(1));
    }

    public (char[,] map, Point start) ReadMap(string[] inputLines)
    {
        var maxX = inputLines.First().Length;
        var maxY = inputLines.Length;
        var startPoint = new Point(0, 0);
        
        var map = new char[maxX, maxY];
        int y = 0;
        foreach (var line in inputLines)
        {
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == 'S') startPoint = new Point(x, y);
                map[x, y] = line[x];
            }
            y++;
        }

        return (map, startPoint);
    }
}