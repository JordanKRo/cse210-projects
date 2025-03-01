public class ReflectionActivity : Activity{

    private List<ReflectionPrompt> _prompts;
    private double _followUpDelay;
    private double _ponderTime;

    private List<ReflectionPrompt> _promptHat = new List<ReflectionPrompt>();

    private Random _randomSeed = new Random();

    public ReflectionActivity(double delay, string description, double duration, 
    string completionMessage, List<ReflectionPrompt> prompts, double followUpDelay, 
    double ponderTime) : 
    base(delay, description, duration, completionMessage) {
        _prompts = prompts;
        _followUpDelay = followUpDelay;
        _ponderTime = ponderTime;

        _promptHat = new List<ReflectionPrompt>(_prompts);
    }

    public override async Task Start()
    {
        DisplayIntro();
        await Spinner("Get Ready", _delay);

        // Set timer
        DateTime end = DateTime.Now.AddSeconds(_duration);
        double timeLeft;
        do{
            timeLeft = (end - DateTime.Now).Seconds;
            
            // A random prompt is selected from the hat and removed from the hat
            Console.WriteLine("\nConsider the following Prompt:");
            ReflectionPrompt prompt = _promptHat[_randomSeed.Next(0, _promptHat.Count)];
            _promptHat.Remove(prompt);

            // display the prompt then wait
            Console.WriteLine(prompt.GetPrompt());
            await Timer("Ponder.. ", _ponderTime);

            do{
                Console.WriteLine(prompt.GetFollowUp());
                Console.WriteLine();
                await Spinner("",_ponderTime);
            } while (!prompt.IsDone() && timeLeft > 0);
        } while (timeLeft > 0);

        
    }
}