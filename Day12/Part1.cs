using Common;

public record Point(int X, int Y)
{
    
}

public class Part1 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        var maxX = inputLines.First().Length;
        var maxY = inputLines.Length;

        var map = new char[maxX, maxY];
        int y = 0;
        foreach (var line in inputLines)
        {
            for (int x = 0; x < line.Length; x++)
            {
                map[x, y] = line[x];
            }
            y++;
        }
    }
}