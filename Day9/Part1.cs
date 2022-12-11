using System.ComponentModel;
using Common;

public record Point(int X, int Y)
{
    public bool IsTouching()
    {
        return (Math.Abs(X) == 1 || X == 0) && (Math.Abs(Y) == 1 || Y == 0);
    }
}

public class State
{
    public Point Head { get; set; } = new Point(0, 0);
    public Point Tail { get; set; } = new Point(0, 0);

    public HashSet<Point> TailVisitedPositions 
        = new HashSet<Point>() { new Point(0, 0) };

    public void ProcessInstruction(string line)
    {
        var bits = line.Split(" ");
        var movementCount = int.Parse(bits[1]) * -1;

        for (int i = 0; i < movementCount; i++)
        {
            switch (bits[0])
            {
                case "R":
                    Head = Head with { Y = Head.Y + 1 };
                    break;
                case "U":
                    Head = Head with { X = Head.X + 1 };
                    break;
                case "L":
                    Head = Head with { Y = Head.Y - 1 };
                    break;
                case "D":
                    Head = Head with { X = Head.X - 1 };
                    break;
            }

            var distance = new Point(Math.Abs(Head.X) - Math.Abs(Tail.X), Math.Abs(Head.Y) - Math.Abs(Tail.Y));
            var direction = new Point(Head.X - Tail.X, Head.Y - Tail.Y);

            if (!distance.IsTouching())
            {
                //need to move the tail
                if (Head.X == Tail.X)
                {
                    var d = direction.Y < 0 ? direction.Y + 1 : direction.Y - 1;
                    Tail = Tail with { Y = Tail.Y + d };
                }
                else if (Head.Y == Tail.Y)
                {
                    var d = direction.X < 0 ? direction.X + 1 : direction.X - 1;
                    Tail = Tail with { Y = Tail.Y + d };
                }
                else //Diagnoal
                {
                    //One distance will be 2, one will be 1;
                    var dX = Math.Abs(direction.X) == 1 ? direction.X : direction.X < 0 ? -1 : 1;
                    var dY = Math.Abs(direction.Y) == 1 ? direction.Y : direction.Y < 0 ? -1 : 1;

                    Tail = new Point(Tail.X + dX, Tail.Y + dY);
                }
                TailVisitedPositions.Add(Tail);
            }
        }
    }
}

public class Part1 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        var s = new State();
        foreach (var l in inputLines)
        {
            s.ProcessInstruction(l);
        }

        return s.TailVisitedPositions.Count;
    }
}