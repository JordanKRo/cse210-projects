using System.Reflection;

public abstract class Activity{
    protected double _delay;
    protected string _description;
    protected double _duration;
    protected string _completionMessage;

    protected Activity(double delay, string description, double duration, string completionMessage){
        _delay = delay;
        _description = description;
        _duration = duration;
        _completionMessage = completionMessage;
    }

    public abstract void Start();

}