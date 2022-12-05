namespace Common;

public class Runner<T1, T2> 
    where T1: IDayPartJob, new()
    where T2: IDayPartJob, new()
{
    public void Run()
    {
        var testLines = File.ReadAllLines("test_input.txt");
        var lines = File.ReadAllLines("input.txt");
        
        Console.WriteLine("Day Part 1 - Test - Result: " + new T1().RunPart(testLines));
        Console.WriteLine("Day Part 1 - Result: " + new T1().RunPart(lines));
        
        Console.WriteLine("Day Part 2 - Test - Result: " + new T2().RunPart(testLines));
        Console.WriteLine("Day Part 2 - Result: " + new T2().RunPart(lines));
    }
}