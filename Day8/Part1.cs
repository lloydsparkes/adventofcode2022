using Common;

public record TV(int MaxHeightSoFar, bool IsThisTreeVisible);

public class Part1 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        var grid = BuildGrid(inputLines);

        int maxX = grid.GetLength(0);
        int maxY = grid.GetLength(1);

        var visibilityGrid = new TV[maxX, maxY];
        
        // Need to run each pass independantly

        // Top Down
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                //The Edges are always visible
                if (x == 0 || y == 0 || x == maxX - 1 || y == maxY - 1)
                {
                    visibilityGrid[x, y] = new TV(grid[x, y], true);
                    continue;
                }

                var top = visibilityGrid[x - 1, y];
                var isVisible = grid[x, y] > top.MaxHeightSoFar;
                visibilityGrid[x, y] = new TV(Math.Max(grid[x,y], top.MaxHeightSoFar), isVisible);
            }
        }
        
        // Left to Right
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                //The Edges are always visible
                if (x == 0 || y == 0 || x == maxX - 1 || y == maxY - 1)
                {
                    visibilityGrid[x, y] = new TV(grid[x, y], true);
                    continue;
                }

                var left = visibilityGrid[x, y - 1];
                var isVisible = grid[x, y] > left.MaxHeightSoFar || visibilityGrid[x, y].IsThisTreeVisible;;
                visibilityGrid[x, y] = new TV(Math.Max(grid[x,y], left.MaxHeightSoFar), isVisible);
            }
        }

        // Bottom Up
        for (int x = maxX-1; x >= 0; x--)
        {
            for (int y = maxY-1; y >= 0; y--)
            {
                if (x == 0 || y == 0 || x == maxX - 1 || y == maxY - 1)
                {
                    // The edges are already done
                    continue;
                }

                var bottom = visibilityGrid[x + 1, y];
                var isVisible = grid[x, y] > bottom.MaxHeightSoFar || visibilityGrid[x, y].IsThisTreeVisible;
                visibilityGrid[x, y] = new TV(Math.Max(grid[x,y], bottom.MaxHeightSoFar), isVisible);
            }
        }
        
        // Right to Left
        for (int x = maxX-1; x >= 0; x--)
        {
            for (int y = maxY-1; y >= 0; y--)
            {
                if (x == 0 || y == 0 || x == maxX - 1 || y == maxY - 1)
                {
                    // The edges are already done
                    continue;
                }

                var right = visibilityGrid[x, y + 1];
                var isVisible = grid[x, y] > right.MaxHeightSoFar || visibilityGrid[x, y].IsThisTreeVisible;
                visibilityGrid[x, y] = new TV(Math.Max(grid[x,y], right.MaxHeightSoFar), isVisible);
            }
        }

        int count = 0;
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                if (visibilityGrid[x, y].IsThisTreeVisible)
                {
                    Console.Write('V');
                    count++;
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.WriteLine();
        }

        return count;
    }

    public static int[,] BuildGrid(string[] inputLines)
    {
        var grid = new int[inputLines.Length, inputLines.First().Length];

        for (int x = 0; x < inputLines.Length; x++)
        {
            for (int y = 0; y < inputLines[x].Length; y++)
            {
                grid[x, y] = int.Parse(inputLines[x][y].ToString());
            }
        }

        return grid;
    }
}