using Common;

public record Range(int Start, int End)
{

    public bool FullyContains(Range other)
    {
        return Start <= other.Start && End >= other.End;
    }
    
    public bool AnyOverlap(Range other)
    {
        return (Start <= other.Start && End >= other.Start)
               || (Start <= other.End && End >= other.End)
               || (Start > other.Start && End <= other.End);
    }
    
    public static Range ProcessText(string input)
    {
        var bits = input.Split('-');
        return new Range(int.Parse(bits[0]), int.Parse(bits[1]));
    }
}

public class Part1 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        return inputLines.Select(ProcessLine).Sum();
    }
    
    int ProcessLine(string line)
    {
        var bits = line.Split(',');
        var rangeA = Range.ProcessText(bits[0]);
        var rangeB = Range.ProcessText(bits[1]);

        return rangeA.FullyContains(rangeB) || rangeB.FullyContains(rangeA) ? 1 : 0;
    }
}