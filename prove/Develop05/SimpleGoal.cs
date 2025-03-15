using System.Drawing;
using System.Text.Json.Serialization;

public class SimpleGoal : BaseGoal{

    public bool done { get; private set; } = false;
    public SimpleGoal(string name, string description, int points, bool done = false) : base(name, description, points){
        this.done = done;
    }

    public override int Evaluate()
    {
        return done ? points : 0;
    }

    public override bool IsComplete()
    {
        return done;
    }

    public override void Mark()
    {
        done = true;
    }
}