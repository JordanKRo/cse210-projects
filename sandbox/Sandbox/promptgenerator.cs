public class PromptGenerator{
    public List<string> _prompts = new List<string>();

    private Random random = new Random();


    public string GetRandomPrompt(){
        int RandomIndex = random.Next(0,_prompts.Count);
        return _prompts[RandomIndex];
    }
}