public class CheckListGoal : BaseGoal{
    private int _requiredTimes;
    private int _times = 0;

    public CheckListGoal(string name, string description, int points, int requiredTimes) : base(name, description, points){
        _requiredTimes = requiredTimes;
    }

    public override bool IsComplete()
    {
        return _times == _requiredTimes;
    }

    public override int Evaluate()
    {
        throw new NotImplementedException();
    }

    public override string GetString()
    {
        throw new NotImplementedException();
    }

    public override void Mark()
    {
        throw new NotImplementedException();
    }
}