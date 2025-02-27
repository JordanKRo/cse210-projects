public class ReflectionActivity : Activity{

    private List<string> _prompts;
    private double _followUpDelay;
    private double _ponderTime;

    public ReflectionActivity(double delay, string description, double duration, string completionMessage) : 
    base(delay, description, duration, completionMessage) {

    }

    public override async Task Start()
    {
        DisplayIntro();
        await Timer("Get Ready", _delay);

        
    }
}