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
        await Spinner("Get Ready... ", _delay);

        // Set timer
        DateTime end = DateTime.Now.AddSeconds(_duration);
        do{        
            // A random prompt is selected from the hat and removed from the hat
            Console.WriteLine("\nConsider the following Prompt:");
            ReflectionPrompt prompt = _promptHat[_randomSeed.Next(0, _promptHat.Count)];
            _promptHat.Remove(prompt);

            // display the prompt then wait
            Console.WriteLine($"\n——{prompt.GetPrompt()}——\n");
            Console.WriteLine("When you have something in mind press enter to continue.");
            Console.ReadLine();
            Console.WriteLine("Now ponder on each of the following questions as they relate to your experience.");
            await Timer("Get ready... ", _followUpDelay);
            try{
                Console.Clear();
            }catch (IOException e){
                Console.WriteLine(e.Message);
            }
            
            do{
                // Display all of the followup questions in order until time time runs out or the prompt is done.
                await Spinner("> " + prompt.GetFollowUp() + " ", _ponderTime, frameTime: 200, leaveMessage: true);
                Console.WriteLine();
            } while (!(prompt.IsDone() || (end - DateTime.Now).Seconds <= 0));
        } while ((end - DateTime.Now).Seconds > 0);
        Console.WriteLine("Well Done!");
        await Spinner("", _delay);
        DisplayOutro();
        await Spinner("", _delay);
    }

    public override string GetName()
    {
        return "Reflection Activity"; 
    }
}