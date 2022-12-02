// See https://aka.ms/new-console-template for more information

var inputLines = File.ReadAllLines("input.txt");

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

Console.WriteLine("Top 3 Supply Sum: " + topThree.Sum());