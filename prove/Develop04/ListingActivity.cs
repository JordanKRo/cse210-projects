using ToolBox;

public class ListingActivity : Activity{
    private List<string> _prompts = new List<string>();

    private int _ponderTime = 6;
    
    private List<string> _responses = new List<string>();

    private Random _randomSeed = new Random();

    public ListingActivity(int delay, string description, int duration, List<string> prompts) : base(delay, description, duration) {
        _prompts = prompts;
    }

    public ListingActivity(int delay, string description, List<string> prompts) : base(delay, description, -1) {
        _prompts = prompts;
    }

    public override async Task Start()
    {
        await DisplayIntro();

        // Pick a prompt from the hat and remove it
        string prompt = _prompts[_randomSeed.Next(0, _prompts.Count)];

        // Display the prompt
        Console.WriteLine($"\n——{prompt}——\n");
        // Give the user time to think
        await DisplayTimer("You make begin in... ", _ponderTime);
        // Set timer and commence the listing activity
        SetTimer();
        do{
            string _response = InputValidation.PromptString("> ");
            _responses.Add(_response);

            
        } while (TimerRunning());
        Console.WriteLine($"You listed {_responses.Count} items!\n");

        await DisplayOutro();
    }

    public override string GetName()
    {
        return "Listing Activity"; 
    }

    public List<string> getResponses(){
        return _responses;
    }

    public List<string> getPrompts(){
        return _prompts;
    }

    public int getPonderTime(){
        return _ponderTime;
    }

    public void setPonderTime(int ponderTime){
        _ponderTime = ponderTime;
    }
}