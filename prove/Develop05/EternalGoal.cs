using System.Drawing;
using System.Text.Json.Serialization;

public class EternalGoal : BaseGoal {

    public int times { get; private set; }

    public EternalGoal(string name, string description, int points, int times = 0) : base(name, description, points){
        this.times = times;
    }

    public override int Evaluate()
    {
        return times * points;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override void Mark()
    {
        times += 1;
    }
}