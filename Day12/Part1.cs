using Common;

public record Point(int X, int Y);

public class Part1 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        (var map, var start, var goal) = ReadMap(inputLines);
        var routeCostMap = new int[map.GetLength(0), map.GetLength(1)];

        Queue<Point> toCheck = new Queue<Point>(new[] { goal });
        while (toCheck.Any())
        {
            var testPoint = toCheck.Dequeue();

            foreach (var neightbour in GetValidNextPoints(map, testPoint))
            {
                if(routeCostMap[neightbour.X, neightbour.Y] > 0) continue;
                routeCostMap[neightbour.X, neightbour.Y] = routeCostMap[testPoint.X, testPoint.Y] + 1;
                toCheck.Enqueue(neightbour);
            }
        }

        return routeCostMap[start.X, start.Y];
    }
    
    public static IEnumerable<Point> GetValidNextPoints(char[,] map, Point sp)
    {
        var silbingPoints = new[]
        {
            new Point(sp.X, sp.Y + 1),
            new Point(sp.X, sp.Y - 1),
            new Point(sp.X + 1, sp.Y),
            new Point(sp.X - 1, sp.Y),
        };

        return silbingPoints.Where(p => 
            InBounds(map, p)
            && (map[sp.X, sp.Y]-map[p.X,p.Y]) <= 1);
    }

    public static bool InBounds(char[,] map, Point sp)
    {
        return (sp.X >= 0 && sp.X < map.GetLength(0))
               && (sp.Y >= 0 && sp.Y < map.GetLength(1));
    }

    public static (char[,] map, Point start, Point goal) ReadMap(string[] inputLines)
    {
        var maxX = inputLines.First().Length;
        var maxY = inputLines.Length;
        var startPoint = new Point(0, 0);
        var goalPoint = new Point(0, 0);
        
        var map = new char[maxX, maxY];
        int y = 0;
        foreach (var line in inputLines)
        {
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == 'S')
                {
                    startPoint = new Point(x, y);
                    map[x, y] = 'a';
                }
                else if (line[x] == 'E')
                {
                    goalPoint = new Point(x, y);
                    map[x, y] = 'z';
                }
                else
                {
                    map[x, y] = line[x];
                }
            }
            y++;
        }

        return (map, startPoint, goalPoint);
    }
}