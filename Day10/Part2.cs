using Common;

public class CpuCrt
{
    public int X { get; set; } = 1;

    public int CycleCount { get; set; } = 1;

    public char[,] Screen { get; set; } = new char[6, 40];

    public (int cycle, int x) Cycle(int? addX = null)
    {
        DrawPixel();
        CycleCount++;
        if (addX.HasValue) X += addX.Value;
        return (CycleCount, X);
    }

    public void DrawPixel()
    {
        var cc = CycleCount - 1;
        var row = cc / 40;
        var col = (cc - (row * 40));

        while (row >= 6)
        {
            row -= 6;
        }
        
        if (col == X || col == X - 1 || col == X + 1)
        {
            Screen[row, col] = '#';
        }
        else
        {
            Screen[row, col] = '.';
        }
    }

    public void DrawScreen()
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 40; y++)
            {
                Console.Write(Screen[x, y]);
            }
            Console.WriteLine();
        }
    }

    public IEnumerable<(int cycle, int x)> ProcessInstruction(string line)
    {
        if (line == "noop") yield return Cycle();
        else
        {
            var count = int.Parse(line.Split(" ")[1]);
            yield return Cycle();
            yield return Cycle(count);
        }
    }
}

public class Part2 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        var cpu = new CpuCrt();
        foreach (var l in inputLines)
        {
            cpu.ProcessInstruction(l).ToList();
        }

        cpu.DrawScreen();
        return null;
    }
}