using Common;

public class Part1 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        return string.Join(",", inputLines.Select(ProcessLine));
    }

    public int ProcessLine(string line)
    {
        for (int i = 0; i < line.Length - 4; i++)
        {
            var subString = line.Substring(i, 4).ToArray();
            if (subString.Distinct().Count() == 4)
            {
                return i + 4;
            }
        }
        return -1;
    }
}