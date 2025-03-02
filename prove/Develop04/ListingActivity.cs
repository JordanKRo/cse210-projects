public class ListingActivity : Activity{
    private List<string> _prompts = new List<string>();

    private List<string> _responses = new List<string>();

    public ListingActivity(int delay, string description, int duration, List<string> prompts) : base(delay, description, duration) {
        _prompts = prompts;
    }

    public ListingActivity(int delay, string description, List<string> prompts) : base(delay, description, -1) {
        _prompts = prompts;
    }

    public override async Task Start()
    {
        await DisplayIntro();

        SetTimer();
    }

    public override string GetName()
    {
        return "Listing Activity"; 
    }
}