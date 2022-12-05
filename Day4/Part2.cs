using Common;

public class Part2 : IDayPartJob
{
    public int RunPart(string[] inputLines)
    {
        return inputLines.Select(ProcessLine).Sum();
    }
    
    int ProcessLine(string line)
    {
        var bits = line.Split(',');
        var rangeA = Range.ProcessText(bits[0]);
        var rangeB = Range.ProcessText(bits[1]);

        return rangeA.AnyOverlap(rangeB) ? 1 : 0;
    }
}