using System.Drawing;

public class EternalGoal : BaseGoal {
    private int _times = 0;

    public EternalGoal(string name, string description, int points) : base(name, description, points){

    }

    public override int Evaluate()
    {
        return _times * _points;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override void Mark()
    {
        _times += 1;
    }
}