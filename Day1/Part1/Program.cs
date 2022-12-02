// See https://aka.ms/new-console-template for more information

var inputLines = File.ReadAllLines("input.txt");

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

Console.WriteLine("Highest Supply Count: " + currentHighest);