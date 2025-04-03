public class WriteNode : BaseNode
{
    private string variable;
    private dynamic value;
    private BaseNode nextEvent;
    public WriteNode(string id, string variable, dynamic value, BaseNode nextEvent) : base(id, 0, true, false, false)
    {
        this.variable = variable;
        this.value = value;
        this.nextEvent = nextEvent;
    }

    public override BaseNode? GetNextEvent()
    {
        return nextEvent;
    }

    public override void OnExecute()
    {
        GameState.GetGameState().Set(variable, value);
    }
}