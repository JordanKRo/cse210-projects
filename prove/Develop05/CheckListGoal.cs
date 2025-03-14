using System.Text.Json.Serialization;

public class CheckListGoal : BaseGoal{
    [JsonPropertyName("requiredTimes")]
    public int _requiredTimes { get; private set; }
    [JsonPropertyName("times")]
    public int _times { get; private set; } = 0;
    [JsonPropertyName("bonusPoints")]
    public int _bonusPoints { get; private set; }

    public CheckListGoal(string name, string description, int points, int requiredTimes, int bonusPoints, int times = 0) : base(name, description, points){
        _requiredTimes = requiredTimes;
        _bonusPoints = bonusPoints;
        _times = times;
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