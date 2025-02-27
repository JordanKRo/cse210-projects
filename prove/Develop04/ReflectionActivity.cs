public class ReflectionActivity : Activity{

    private List<ReflectionPrompt> _prompts;
    private double _followUpDelay;
    private double _ponderTime;

    public ReflectionActivity(double delay, string description, double duration, string completionMessage, List<ReflectionPrompt> prompts, double followUpDelay, double followUpTime) : 
        base(delay, description, duration, completionMessage) {
        
    }

    public override async Task Start()
    {
        DisplayIntro();
        await Timer("Get Ready", _delay);

        
    }
}