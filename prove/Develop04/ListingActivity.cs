public class ListingActivity : Activity{
    private List<string> _prompts = new List<string>();

    private List<string> _responses = new List<string>();

    public ListingActivity(int delay, string description, int duration) : base(delay, description, duration) {

    }

    public ListingActivity(int delay, string description) : base(delay, description, -1) {
        
    }

    public override async Task Start()
    {
        await DisplayIntro();
    }

    public override string GetName()
    {
        return "Listing Activity"; 
    }
}