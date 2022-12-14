using Common;

public record Monkey(long Id, Func<long, long> WorryOperation, long TestDivisor, long TestTrueMonkey, long TestFalseMonkey)
{
    public Queue<long> Items { get; } = new Queue<long>();
    
    public long ItemsInspected { get; set; }

    public void Process(Monkey[] others)
    {
        while (Items.TryDequeue(out long item))
        {
            ItemsInspected++;
            var newWorry = WorryOperation(item) / 3;

            if ((newWorry % TestDivisor) == 0)
            {
                others[TestTrueMonkey].Items.Enqueue(newWorry);
            }
            else
            {
                others[TestFalseMonkey].Items.Enqueue(newWorry);
            }
        }
    }
}

public class Part1 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        var monkeys = ProcessLines(inputLines);
        long roundCount = 20;

        for (long i = 0; i < roundCount; i++)
        {
            foreach (var m in monkeys)
            {
                m.Process(monkeys);
            }
        }

        long result = 0;
        foreach (var m in monkeys.Select(m => m.ItemsInspected).OrderDescending().Take(2))
        {
            if (result == 0) result = m;
            else result *= m;
        }

        return result;
    }

    private Monkey[] ProcessLines(string[] inputLines)
    {
        var monkeys = new List<Monkey>();

        long id = -1, testDivisor = -1, testTrue = -1, testFalse = -1;
        long[] items = null;
        Func<long, long> op = null;

        foreach (var l in inputLines)
        {
            if (l.StartsWith("Monkey"))
            {
                id = long.Parse(l[7].ToString());
            }

            if (l.Contains("Starting items:"))
            {
                items = l.Split(":")[1].Split(",").Select(s => s.Trim()).Select(s => long.Parse(s)).ToArray();
            }

            if (l.Contains("Operation:"))
            {
                var log = l.Split("=")[1].Split(" ").Select(s => s.Trim()).Skip(1).ToArray();

                if (long.TryParse(log[2], out long constant))
                {
                    switch (log[1][0])
                    {
                        case '-':
                            op = i => i - constant;
                            break;
                        case '+': 
                            op = i => i + constant;
                            break;
                        case '*':
                            op = i => i * constant;
                            break;
                        case '/':
                            op = i => i / constant;
                            break;
                    }                    
                }
                else
                {
                    switch (log[1][0])
                    {
                        case '-':
                            op = i => i - i;
                            break;
                        case '+': 
                            op = i => i + i;
                            break;
                        case '*':
                            op = i => i * i;
                            break;
                        case '/':
                            op = i => i / i;
                            break;
                    }
                }
                
            }

            if (l.Contains("Test:"))
            {
                testDivisor = long.Parse(l.Split(" ").Last());
            }
            
            if (l.Contains("If true:"))
            {
                testTrue = long.Parse(l.Split(" ").Last());
            }
            
            if (l.Contains("If false:"))
            {
                testFalse = long.Parse(l.Split(" ").Last());
            }

            if (string.IsNullOrEmpty(l))
            {
                monkeys.Add(new Monkey(id, op, testDivisor, testTrue, testFalse));

                foreach (var item in items)
                {
                    monkeys.Last().Items.Enqueue(item);
                }
            }
        }
        
        monkeys.Add(new Monkey(id, op, testDivisor, testTrue, testFalse));

        foreach (var item in items)
        {
            monkeys.Last().Items.Enqueue(item);
        }
        
        return monkeys.ToArray();
    }
}