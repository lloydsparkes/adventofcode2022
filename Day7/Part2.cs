using Common;

public class Part2 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        var treeRoot = Part1.BuildTree(inputLines);
        var folderSizes = new Dictionary<Folder, int>();
        
        Part1.ProcessFolder(treeRoot, folderSizes);

        var currentFreeSpace = 70000000 - treeRoot.GetFolderSize();
        var requiredFreeSpace = 30000000;

        var differenceToFind = requiredFreeSpace - currentFreeSpace;

        return folderSizes.Values.Where(v => v >= differenceToFind).Min();
    }
}