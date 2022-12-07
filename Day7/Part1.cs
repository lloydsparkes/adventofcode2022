using Common;

public class Folder
{
    public string Name { get; set; }
    
    public Folder Parent { get; set; }
    public List<Folder> SubFolders { get; set; } = new();
    public List<File> Files { get; set; } = new();

    public int GetFolderSize()
    {
        return SubFolders.Sum(f => f.GetFolderSize()) + Files.Sum(f => f.Size);
    }
}

public record File(int Size, string Name); 

public class Part1 : IDayPartJob
{
    public object RunPart(string[] inputLines)
    {
        var treeRoot = BuildTree(inputLines);
        var folderSizes = new Dictionary<Folder, int>();
        
        ProcessFolder(treeRoot, folderSizes);

        return folderSizes.Values.Sum(s => s <= 100000 ? s : 0);
    }

    public static void ProcessFolder(Folder folder, Dictionary<Folder, int> sizes)
    {
        foreach (var f in folder.SubFolders)
        {
            sizes.Add(f, f.GetFolderSize());
            ProcessFolder(f, sizes);
        }
    }
    
    public static Folder BuildTree(string[] inputLines)
    {
        Folder root = new Folder { Name = "/" };
        Folder current = root;
        
        foreach (var line in inputLines)
        {
            var bits = line.Split(' ');
            if (bits[0] == "$")
            {
                // Command
                if (bits[1] == "cd")
                {
                    if (bits[2] == "/")
                    {
                        current = root;
                    }
                    else if (bits[2] == ".." && current.Parent != null)
                    {
                        current = current.Parent;
                    }
                    else
                    {
                        current = current.SubFolders.First(f => f.Name == bits[2]);
                    }
                }
            }
            else if(bits[0] == "dir")
            {
                current.SubFolders.Add(new Folder {Name = bits[1], Parent = current});
            }
            else
            {
                current.Files.Add(new File(int.Parse(bits[0]), bits[1]));
            }
        }

        return root;
    }
}