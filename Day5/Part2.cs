using Common;

public class Part2 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        var cargoLines = inputLines.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        var instructions = inputLines.Where(x => x.StartsWith("move")).Select(Instruction.ReadInstruction).ToList();

        var cargoHold = CargoHold.ReadInitialStacks(cargoLines);
        
        foreach (var inst in instructions)
        {
            cargoHold.ProcessInstruction2(inst);
        }

        return new string(cargoHold.Stacks.Values.Select(s => s.Peek()).ToArray());
    }
}