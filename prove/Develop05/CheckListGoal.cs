using System.Text.Json.Serialization;

public class CheckListGoal : BaseGoal{

    public int requiredTimes { get; private set; }

    public int times { get; private set; } = 0;

    public int bonusPoints { get; private set; }

    public CheckListGoal(string name, string description, int points, int requiredTimes, int bonusPoints, int times = 0) : base(name, description, points){
        this.requiredTimes = requiredTimes;
        this.bonusPoints = bonusPoints;
        this.times = times;
    }

    public override bool IsComplete()
    {
        return times == requiredTimes;
    }

    public override int Evaluate()
    {
        return times * points + (IsComplete() ? bonusPoints : 0);
    }

    public override void Mark()
    {
        if (times < requiredTimes) {
            times += 1;
        }
    }

    public override string GetString()
    {
        string check = IsComplete() ? "X" : " ";
        return $"[ {check} ] {name} ({description}) (Completed {times}/{requiredTimes}) Points Earned: {Evaluate()} points";
    }
}