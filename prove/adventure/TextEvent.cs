public class TextEvent : BaseNode{
    protected string content;
    protected BaseNode? nextNode;
    public TextEvent(string id, string content, BaseNode nextEvent, bool autoAdvance = false, bool displayProceedMessage = true, int sleepMils = 0, bool checkpoint = true) : base(id, sleepMils, autoAdvance, displayProceedMessage, checkpoint: checkpoint){
        this.content = content;
        this.nextNode = nextEvent;
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
        return nextNode;
    }

    public void SetNextNode(BaseNode node){
        nextNode = node;
    }
}