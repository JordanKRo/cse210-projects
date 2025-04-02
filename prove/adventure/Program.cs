public static class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();
        GameState state = new GameState();
        // Create the game tree
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..","..","..", "demos", "demo.json");
        BaseNode mainTree = EventLoader.LoadFromFile(path);
        mainTree.Main();
    }
}