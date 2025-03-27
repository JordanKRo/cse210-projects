public class TextEvent : BaseEvent{
    protected string content;
    protected BaseEvent? nextEvent;
    public TextEvent(string id, string content, BaseEvent? nextEvent) : base(id){
        this.content = content;
        this.nextEvent = nextEvent;
    }

    public override void DisplayContent()
    {
        Console.WriteLine(content);
    }

    public override BaseEvent? GetNextEvent()
    {
        return nextEvent;
    }
}