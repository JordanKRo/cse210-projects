public class Chooser : TextEvent
{
    protected string prompt;
    protected List<Option> options;
    public Chooser(string id, string prompt, List<Option> options) : base(id, prompt, null)
    {
        this.prompt = prompt;
        this.options = options;
        autoAdvance = true;
    }
    public override void OnExecute()
    {
        Console.WriteLine(prompt);
    }

    public override BaseNode? GetNextEvent()
    {
        return PromptOptions();
        
    }
    /// <summary>
    /// Displays the options and prompts the user to select one
    /// </summary>
    /// <returns></returns>
    protected BaseNode PromptOptions(){
        Dictionary<char, BaseNode> interpretedNodes = new Dictionary<char, BaseNode>();
        for (int i = 0; i < options.Count; i++){
            // Identifier is just the option number unless the id is null
            char identifier = options[i].Identifier.HasValue ? options[i].Identifier!.Value : (i + 1).ToString()[0];
            Console.WriteLine($"{(i + 1)} - {options[i]}");
            interpretedNodes.Add(identifier, options[i].Node);
        }
        
        char entry;
        do {
            Console.Write("Enter an option: ");
            entry = Console.ReadLine()?.FirstOrDefault() ?? '\0';
            if (!interpretedNodes.ContainsKey(entry)) {
                Console.WriteLine("\nInvalid option. Please try again.");
            }
        } while (!interpretedNodes.ContainsKey(entry));

        // Start the selected node.
        return interpretedNodes[entry];
    }

    public void SetOptions(List<Option> options) {
        this.options = options;
    }
}