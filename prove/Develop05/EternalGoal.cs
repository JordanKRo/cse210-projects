using System.Drawing;
using System.Text.Json.Serialization;

public class EternalGoal : BaseGoal {
    [JsonPropertyName("times")]
    public int _times { get; private set; }

    public EternalGoal(string name, string description, int points, int times = 0) : base(name, description, points){
        _times = times;
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