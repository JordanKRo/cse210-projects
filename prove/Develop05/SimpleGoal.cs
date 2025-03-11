using System.Drawing;

public class SimpleGoal : BaseGoal{
    private bool _done = false;
    public SimpleGoal(string name, string description, int points) : base(name, description, points){

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
    // TODO remove the override and hand it to the base class
    public override string GetString()
    {
        throw new NotImplementedException();
    }
}