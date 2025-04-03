/// <summary>
/// System nodes can be used to simply call things within the tree. This Node cannot be referenced within the Json Tree.
/// </summary>
public class SystemNode : BaseNode
{
    protected Action callable;
    protected BaseNode nextEvent;
    public SystemNode(string id, Action callable, BaseNode nextEvent, bool checkpoint = true) : base(id, 0, true, false, checkpoint)
    {
        this.callable = callable;
        this.nextEvent = nextEvent;
    }

    public override BaseNode? GetNextEvent()
    {
        return nextEvent;
    }

    public override void OnExecute()
    {
        callable.Invoke();
    }

    public void SetNextNode(BaseNode nextNode){
        nextEvent = nextNode;
    }
}