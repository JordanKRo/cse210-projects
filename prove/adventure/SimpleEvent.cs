public class TextEvent : BaseNode{
    protected string content;
    protected BaseNode? nextEvent;
    public TextEvent(string id, string content, BaseNode nextEvent) : base(id){
        this.content = content;
        this.nextEvent = nextEvent;
    }

    public TextEvent(string id, string content) : base(id){
        this.content = content;
    }

    public override void OnExecute()
    {
        Console.WriteLine(content);
    }

    public override BaseNode? GetNextEvent()
    {
        return nextEvent;
    }
}