using ToolBox;

public static class Program
{
    public static void Main(string[] args)
    {
        var options = new List<Option>
        {
            new Option("Option 1", false, new TextEvent("ID1", "Option 1 content")),
            new Option("Option 2", false, new TextEvent("ID2", "Option 2 content")),
            new Option("Option 3", false, new TextEvent("ID3", "Option 3 content")),
            new Option("Option 4", false, new TextEvent("ID4", "Option 4 content"))
        };
        var event1 = new Chooser("event 1","Select a path",options);
        BaseNode mainTree = new TextEvent("Main", "Main Content", event1);
        mainTree.Main();
    }
}