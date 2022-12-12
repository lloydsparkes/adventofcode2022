using Common;

public class Part2 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        var s = new State(10);
        foreach (var l in inputLines)
        {
            s.ProcessInstruction(l);
        }

        return s.TailVisitedPositions.Count;
    }
}