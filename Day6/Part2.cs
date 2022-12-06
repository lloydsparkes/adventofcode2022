using Common;

public class Part2 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        return string.Join(",", inputLines.Select(ProcessLine));
    }

    public int ProcessLine(string line)
    {
        for (int i = 0; i < line.Length - 14; i++)
        {
            var subString = line.Substring(i, 14).ToArray();
            if (subString.Distinct().Count() == 14)
            {
                return i + 14;
            }
        }
        return -1;
    }
}