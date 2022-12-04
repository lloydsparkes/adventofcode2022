using System.Diagnostics;
using System.Linq;

var inputLines = File.ReadAllLines("input.txt");
var groups = inputLines.Chunk(3);

Console.WriteLine("Result: " + groups.Select(ProcessGroup).Sum());

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

