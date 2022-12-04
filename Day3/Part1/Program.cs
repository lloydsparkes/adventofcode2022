using System.Diagnostics;

var inputLines = File.ReadAllLines("input.txt");

Console.WriteLine("Result: " + inputLines.Select(ProcessBackpack).Sum());

int ProcessBackpack(string backpack)
{
    var compartment1 = backpack.Substring(0, backpack.Length / 2);
    var compartment2 = backpack.Substring(backpack.Length / 2);

    char commonItem = Char.MinValue;
    foreach (var c in compartment1)
    {
        if (compartment2.Contains(c))
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