using System.Threading.Tasks.Dataflow;
using Common;

public class Part2 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        var grid = Part1.BuildGrid(inputLines);

        int maxX = grid.GetLength(0);
        int maxY = grid.GetLength(1);

        var scenicGrid = new int[maxX, maxY];
        int maxSoFar = 0;
        
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                scenicGrid[x, y] = CountD(grid, x, y) * CountU(grid, x, y) * CountL(grid, x, y) * CountR(grid, x, y);

                if (scenicGrid[x, y] > maxSoFar) maxSoFar = scenicGrid[x, y];
            }
        }

        return maxSoFar;
    }

    private int CountD(int[,] grid, int fromX, int fromY)
        => Count(grid, fromX, fromY, (x, y) => (x + 1, y));
    private int CountL(int[,] grid, int fromX, int fromY)
        => Count(grid, fromX, fromY, (x, y) => (x, y + 1));
    private int CountR(int[,] grid, int fromX, int fromY)
        => Count(grid, fromX, fromY, (x, y) => (x, y - 1));
    private int CountU(int[,] grid, int fromX, int fromY)
        => Count(grid, fromX, fromY, (x, y) => (x - 1, y));
    
    private int Count(int[,] grid, int fromX, int fromY, Func<int, int, (int x, int y)> direction)
    {
        int count = 0;
        var toCheck = direction(fromX, fromY);
        while (IsValid(grid, toCheck))
        {
            if (grid[toCheck.x, toCheck.y] < grid[fromX, fromY])
            {
                count++;
            }
            if (grid[toCheck.x, toCheck.y] >= grid[fromX, fromY])
            {
                count++;
                break;
            }

            toCheck = direction(toCheck.x, toCheck.y);
        }

        return count;
    }

    private bool IsValid(int[,] grid, (int x, int y) input)
    {
        return input.x >= 0 
               && input.y >= 0 
               && input.x < grid.GetLength(0) 
               && input.y < grid.GetLength(1);
    }
}