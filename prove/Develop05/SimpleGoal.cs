using System.Drawing;
using System.Text.Json.Serialization;

public class SimpleGoal : BaseGoal{
    [JsonPropertyName("done")]
    public bool _done { get; private set; } = false;
    public SimpleGoal(string name, string description, int points, bool done = false) : base(name, description, points){
        _done = done;
    }

    public override int Evaluate()
    {
        return _done ? _points : 0;
    }

    public override bool IsComplete()
    {
        return _done;
    }

    public override void Mark()
    {
        _done = true;
    }
}