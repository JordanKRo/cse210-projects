public class SimpleGoal : BaseGoal{
    private bool _done;
    public SimpleGoal(string name, string description, int points) : base(name, description, points){

    }

    public override int Evaluate()
    {
        throw new NotImplementedException();
    }

    public override bool IsComplete()
    {
        throw new NotImplementedException();
    }

    public override void Mark()
    {
        throw new NotImplementedException();
    }

    public override string GetString()
    {
        throw new NotImplementedException();
    }
}