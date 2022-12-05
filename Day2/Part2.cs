using Common;

public class Part2 : IDayPartJob
{
    enum Result
    {
        Lose,
        Draw,
        Win
    }
    
    public object RunPart(string[] inputLines)
    {
        int totalScore = 0;

        foreach (var line in inputLines)
        {
            var bits = line.Split(' ').Select(x => x.Trim()).ToArray();
            var theirs = bits[0] == "A" ? ObjectChosen.Rock : bits[0] == "B" ? ObjectChosen.Paper : ObjectChosen.Scissors;
            var result = bits[1] == "X" ? Result.Lose : bits[1] == "Y" ? Result.Draw : Result.Win;

            //Console.WriteLine($"{theirs} -> {mine} -> {ScoreRound(theirs, mine)}");
    
            totalScore += Part1.ScoreRound(theirs, ResultToSelection(theirs, result));
        }

        return totalScore;
    }
    
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
}