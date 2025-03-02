public class ReflectionActivity : Activity{

    private List<ReflectionPrompt> _prompts = new List<ReflectionPrompt>();
    private double _followUpDelay;
    private double _ponderTime;

    private Random _randomSeed = new Random();

    public ReflectionActivity(int delay, string description, int duration, List<ReflectionPrompt> prompts, double followUpDelay, 
    double ponderTime) : 
    base(delay, description, duration) {
        _prompts = prompts;
        _followUpDelay = followUpDelay;
        _ponderTime = ponderTime;
    }

    public ReflectionActivity(int delay, string description, List<ReflectionPrompt> prompts, double followUpDelay, 
    double ponderTime) : 
    base(delay, description, -1) {
        _prompts = prompts;
        _followUpDelay = followUpDelay;
        _ponderTime = ponderTime;
    }

    public override async Task Start()
    {
        await DisplayIntro();

        // A random prompt is selected
        ReflectionPrompt prompt = _prompts[_randomSeed.Next(0, _prompts.Count)];

        Console.WriteLine("\nConsider the following Prompt:");
        // display the prompt then wait
        Console.WriteLine($"\n——{prompt.GetPrompt()}——\n");
        Console.WriteLine("When you have something in mind press enter to continue.");
        Console.ReadLine();
        Console.WriteLine("Now ponder on each of the following questions as they relate to your experience.");
        await DisplayTimer("Get ready... ", _followUpDelay);
        SafeClearConsole();
        // Set timer
        SetTimer();
        do{
            // Display all of the followup questions until the time runs out
            await DisplaySpinner("> " + prompt.GetFollowUp() + " ", _ponderTime, frameTime: 200, leaveMessage: true);
            Console.WriteLine();
        } while (TimerRunning());
        
        await DisplayOutro();
    }

    public override string GetName()
    {
        return "Reflection Activity"; 
    }

    public List<ReflectionPrompt> GetPrompts(){
        return _prompts;
    }
}