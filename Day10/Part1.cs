using Common;

public class Cpu
{
    public int X { get; set; } = 1;

    public int CycleCount { get; set; } = 1;

    public (int cycle, int x) Cycle(int? addX = null)
    {
        CycleCount++;
        if (addX.HasValue) X += addX.Value;
        return (CycleCount, X);
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

public class Part1 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        int nextCheckPoint = 20;
        int checkPointAddition = 40;
        int result = 0;

        var cpu = new Cpu();
        foreach (var l in inputLines)
        {
            foreach (var (cycle, x) in cpu.ProcessInstruction(l))
            {
                if (cpu.CycleCount == nextCheckPoint)
                {
                    result += (nextCheckPoint * cpu.X);
                    nextCheckPoint += checkPointAddition;
                }

                if (nextCheckPoint > 220)
                {
                    break;
                }
            }
        }
        return result;
    }
}