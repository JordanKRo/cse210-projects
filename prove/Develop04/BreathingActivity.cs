using System.Runtime.CompilerServices;

public class BreathingActivity : Activity{

    private double _cycleTime;

    public BreathingActivity(double delay, string description, double duration, string completionMessage, double cycleTime) : base(delay, description, duration, completionMessage){
        _cycleTime = cycleTime;
    }
    public override void Start()
    {
        throw new NotImplementedException();
    }
}