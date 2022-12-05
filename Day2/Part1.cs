using Common;

public enum ObjectChosen
{
    Rock = 1,
    Paper = 2, 
    Scissors = 3
}
public class Part1 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        int totalScore = 0;

        foreach (var line in inputLines)
        {
            var bits = line.Split(' ').Select(x => x.Trim()).ToArray();
            var theirs = bits[0] == "A" ? ObjectChosen.Rock : bits[0] == "B" ? ObjectChosen.Paper : ObjectChosen.Scissors;
            var mine = bits[1] == "X" ? ObjectChosen.Rock : bits[1] == "Y" ? ObjectChosen.Paper : ObjectChosen.Scissors;

            totalScore += ScoreRound(theirs, mine);
        }

        return totalScore;
    }
    
    public static int ScoreRound(ObjectChosen theirs, ObjectChosen mine)
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

}