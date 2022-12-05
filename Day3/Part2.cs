using System.Diagnostics;
using Common;

public class Part2 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        return inputLines.Chunk(3).Select(ProcessGroup).Sum();
    }
    
    int ProcessGroup(string[] group)
    {
        var smallestBag = group.MinBy(a => a.Length);
        char commonItem = Char.MinValue;
    
        foreach (var c in smallestBag)
        {
            if (group.All(s => s.Contains(c)))
            {
                commonItem = c;
                break;
            }
        }
    
        if (commonItem >= 'a' && commonItem <= 'z')
        {
            return commonItem - 'a' + 1;
        }
        if (commonItem >= 'A' && commonItem <= 'Z')
        {
            return commonItem - 'A' + 27;
        }

        throw new UnreachableException("Should never get here");
    }
}