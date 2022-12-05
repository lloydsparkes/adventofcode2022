using Common;

public class CargoHold
{
    public Dictionary<int, Stack<char>> Stacks { get; set; } = new();

    public void ProcessInstruction(Instruction inst)
    {
        for (int c = 0; c < inst.Count; c++)
        {
            Stacks[inst.To].Push(Stacks[inst.From].Pop());
        }
    }
    
    public void ProcessInstruction2(Instruction inst)
    {
        if (inst.Count == 1)
        {
            ProcessInstruction(inst);
            return;
        }
        
        var intermediatry = new Stack<char>();
        for (int c = 0; c < inst.Count; c++)
        {
            intermediatry.Push(Stacks[inst.From].Pop());
        }

        while (intermediatry.TryPop(out char c))
        {
            Stacks[inst.To].Push(c);
        }
    }

    public static CargoHold ReadInitialStacks(string[] inputLines)
    {
        var newCargoHold = new CargoHold();
        
        var indexLine = inputLines.First(s => s.StartsWith(" 1"));
        var cargoLines = inputLines.Where(x => x.Contains("[")).Reverse().ToList();
        for (int i = 0; i < indexLine.Length; i++)
        {
            if (indexLine[i] != ' ')
            {
                var stackName = int.Parse(indexLine[i].ToString());
                var stack = new Stack<char>();
                foreach (var line in cargoLines)
                {
                    if (line[i] != ' ')
                    {
                        stack.Push(line[i]);
                    }
                }
                newCargoHold.Stacks.Add(stackName, stack);
            }
        }

        return newCargoHold;
    }
}

public record Instruction(int Count, int From, int To)
{
    public static Instruction ReadInstruction(string input)
    {
        var bits = input.Split(' ');
        return new Instruction(int.Parse(bits[1]), int.Parse(bits[3]), int.Parse(bits[5]));
    }
}

public class Part1 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        var cargoLines = inputLines.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        var instructions = inputLines.Where(x => x.StartsWith("move")).Select(Instruction.ReadInstruction).ToList();

        var cargoHold = CargoHold.ReadInitialStacks(cargoLines);
        
        foreach (var inst in instructions)
        {
            cargoHold.ProcessInstruction(inst);
        }

        return new string(cargoHold.Stacks.Values.Select(s => s.Peek()).ToArray());
    }
}