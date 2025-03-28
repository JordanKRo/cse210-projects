public class Chooser : TextEvent
{
    protected string prompt;
    protected List<Option> options;

    public static List<Option> globalOptions = new List<Option>();
    public Chooser(string id, string prompt, List<Option> options, bool checkpoint = true) : base(id, prompt, null, checkpoint: checkpoint)
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
        // TODO display the numbers before the custom ids
        List<Option> composite = new List<Option>(options);
        composite.AddRange(globalOptions);
        for (int i = 0; i < composite.Count; i++){
            // Identifier is just the option number unless the id is null
            // char identifier = options[i].Identifier.HasValue ? options[i].Identifier ?? 'd' : (i + 1).ToString()[0];
            Option option = composite[i];
            char identifier = char.ToUpper(option.Identifier ?? (i + 1).ToString()[0]);
            if (!option.Hidden){
                Console.WriteLine($"{identifier} - {option}");
            }
            interpretedNodes.Add(identifier, option.Node);
        }
        
        char entry;
        do {
            Console.Write("Enter an option: ");
            entry = Console.ReadLine()?.ToUpper()?.FirstOrDefault() ?? '\0';
            if (!interpretedNodes.ContainsKey(entry)) {
                Console.WriteLine("\nInvalid option. Please try again.");
            }
        } while (!interpretedNodes.ContainsKey(entry));
        Console.WriteLine();

        // Start the selected node.
        return interpretedNodes[entry];
    }

    public void SetOptions(List<Option> options) {
        this.options = options;
    }
}