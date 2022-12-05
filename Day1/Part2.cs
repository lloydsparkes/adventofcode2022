using Common;

public class Part2 : IDayPartJob
{
    public int RunPart(string[] inputLines)
    {
        int currentCount = 0;
        var elfSupplies = new List<int>();

        foreach (var line in inputLines)
        {
            if (line == String.Empty)
            {
                elfSupplies.Add(currentCount);
                currentCount = 0;
            }
            else
            {
                currentCount += int.Parse(line);
            }
        }

        var topThree = elfSupplies.OrderDescending().Take(3);
        return topThree.Sum();
    }
}