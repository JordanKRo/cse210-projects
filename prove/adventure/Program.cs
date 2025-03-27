using ToolBox;

public static class Program
{
    public static void Main(string[] args)
    {
        BaseEvent mainTree = new TextEvent("Main", "Main Content", null);
        mainTree.Main();
    }
}