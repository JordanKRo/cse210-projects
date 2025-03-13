public class CheckListGoal : BaseGoal{
    private int _requiredTimes;
    private int _times = 0;

    private int _bonusPoints;

    public CheckListGoal(string name, string description, int points, int requiredTimes, int bonusPoints) : base(name, description, points){
        _requiredTimes = requiredTimes;
        _bonusPoints = bonusPoints;
    }

    public override bool IsComplete()
    {
        return _times == _requiredTimes;
    }

    public override int Evaluate()
    {
        return _times * _points + (IsComplete() ? _bonusPoints : 0);
    }

    public override void Mark()
    {
        _times += 1;
    }

    public override string GetString()
    {
        return base.GetString() + $"(Completed {_times}/{_requiredTimes})";
    }
}