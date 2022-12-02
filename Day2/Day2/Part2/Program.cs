// A = Rock B = Paper C = Scissors
// X = Lose Y = Draw Z = Win

var inputLines = File.ReadAllLines("input.txt");
int totalScore = 0;

foreach (var line in inputLines)
{
    var bits = line.Split(' ').Select(x => x.Trim()).ToArray();
    var theirs = bits[0] == "A" ? ObjectChosen.Rock : bits[0] == "B" ? ObjectChosen.Paper : ObjectChosen.Scissors;
    var result = bits[1] == "X" ? Result.Lose : bits[1] == "Y" ? Result.Draw : Result.Win;

    //Console.WriteLine($"{theirs} -> {mine} -> {ScoreRound(theirs, mine)}");
    
    totalScore += ScoreRound(theirs, ResultToSelection(theirs, result));
}

Console.WriteLine("Total Score following Strategy: " + totalScore);

ObjectChosen ResultToSelection(ObjectChosen theirs, Result required)
{
    switch (required)
    {
        case Result.Draw:
            return theirs;
        case Result.Win:
            switch (theirs)
            {
                case ObjectChosen.Paper:
                    return ObjectChosen.Scissors;
                case ObjectChosen.Rock:
                    return ObjectChosen.Paper;
                case ObjectChosen.Scissors:
                    return ObjectChosen.Rock;
            }
            break;
        case Result.Lose:
            switch (theirs)
            {
                case ObjectChosen.Paper:
                    return ObjectChosen.Rock;
                case ObjectChosen.Rock:
                    return ObjectChosen.Scissors;
                case ObjectChosen.Scissors:
                    return ObjectChosen.Paper;
            }
            break;
    }

    throw new InvalidOperationException("Should never get here");
}

int ScoreRound(ObjectChosen theirs, ObjectChosen mine)
{
    if (mine == theirs)
    {
        return 3 + (int)mine;
    }

    if ((mine == ObjectChosen.Rock && theirs == ObjectChosen.Scissors) ||
        (mine == ObjectChosen.Scissors && theirs == ObjectChosen.Paper) ||
        (mine == ObjectChosen.Paper && theirs == ObjectChosen.Rock))
    {
        return 6 + (int)mine;
    }

    return (int)mine;
}

enum ObjectChosen
{
    Rock = 1,
    Paper = 2, 
    Scissors = 3
}

enum Result
{
    Lose,
    Draw,
    Win
}