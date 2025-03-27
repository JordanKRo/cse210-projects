public class Chooser : TextEvent
{
    protected string prompt;
    List<Option> options;
    public Chooser(string id, string prompt, List<Option> options) : base(id, prompt, null)
    {
        this.prompt = prompt;
        this.options = options;
    }
    public override void DisplayContent()
    {
        Console.WriteLine(prompt);
    }

    public override BaseEvent? GetNextEvent()
    {
        return PromptOptions();
        
    }
    /// <summary>
    /// Displays the options and prompts the user to select one
    /// </summary>
    /// <returns></returns>
    protected BaseEvent PromptOptions(){
        Dictionary<char, BaseEvent> interpretedNodes = new Dictionary<char, BaseEvent>();
        for (int i = 0; i < options.Count; i++){
            // Identifier is just the option number unless the id is null
            char identifier = options[i].Identifier.HasValue ? options[i].Identifier!.Value : (char)(i + 1);
            Console.WriteLine($"{identifier} - {options[i]}");
            interpretedNodes.Add(identifier, options[i].Node);
        }
        Console.Write("Enter an option: ");
        char entry;
        do {
            entry = Console.ReadLine()?.FirstOrDefault() ?? '\0';
            if (!interpretedNodes.ContainsKey(entry)) {
                Console.WriteLine("Invalid option. Please try again.");
            }
        } while (!interpretedNodes.ContainsKey(entry));

        // Start the selected node.
        return interpretedNodes[entry];
    }
}