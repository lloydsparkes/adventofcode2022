using System.ComponentModel;
using Common;

public record Point(int X, int Y);

public class State
{
    public Point Head { 
        get => Rope[0];
        set { Rope[0] = value; }
    }
    public Point[] Rope { get; set; }

    public State(int length = 2)
    {
        Rope = new Point[length];
        for (int i = 0; i < length; i++)
        {
            Rope[i] = new Point(0, 0);
        }
    }

    public HashSet<Point> TailVisitedPositions 
        = new HashSet<Point>() { new Point(0, 0) };

    public void ProcessInstruction(string line)
    {
        var bits = line.Split(" ");
        var movementCount = int.Parse(bits[1]);

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

            for (var t = 1; t < Rope.Length; t++)
            {
                var dX = Rope[t-1].X - Rope[t].X;
                var dY = Rope[t-1].Y - Rope[t].Y;

                if (Math.Abs(dX) > 1 || Math.Abs(dY) > 1)
                {
                    var mX = Math.Sign(dX);
                    var mY = Math.Sign(dY);

                    Rope[t] = new Point(Rope[t].X + mX, Rope[t].Y + mY);

                    if (t == Rope.Length - 1)
                    {
                        TailVisitedPositions.Add(Rope[t]);
                    }
                }
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