using Common;

public class Part1 : IDayPartJob
{
    public int RunPart(string[] inputLines)
    {
        int currentHighest = 0, currentCount = 0;

        foreach (var line in inputLines)
        {
            if (line == String.Empty)
            {
                if (currentCount > currentHighest)
                {
                    currentHighest = currentCount;
                }

                currentCount = 0;
            }
            else
            {
                currentCount += int.Parse(line);
            }
        }

        return currentHighest;
    }
}