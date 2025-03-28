using System.Text.Json;
using System.Text.Json.Serialization;

public class EventLoader
{
    // Created transfer objects
    public class EventNodeDTO
    {
        // Could be polymorphic but I might not have time
        public required string Id { get; set; }
        public required string Type { get; set; }
        public required string Content { get; set; }
        public int SleepMils { get; set; } = 0;
        public bool AutoAdvance { get; set; } = false;
        public bool DisplayProceedMessage { get; set; } = true;
        public string? NextId { get; set; }
        public List<OptionDTO> Options { get; set; } = new List<OptionDTO>();
    }

    public class OptionDTO
    {
        public required string Name { get; set; }
        public bool Hidden { get; set; } = false;
        public required string NextId { get; set; }
        public char? Identifier { get; set; }
    }

    public class EventTreeDTO
    {
        public List<EventNodeDTO> Nodes { get; set; } = new List<EventNodeDTO>();
    }

    // Load the entire event tree from a JSON file
    public static BaseNode LoadFromFile(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        // Then process the string
        return LoadFromJson(jsonString);
    }

    // Load the event tree from a JSON string
    public static BaseNode LoadFromJson(string jsonString)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        EventTreeDTO eventTree = JsonSerializer.Deserialize<EventTreeDTO>(jsonString, options);

        // First, create a dictionary of all nodes by their ID (but without connections)
        Dictionary<string, BaseNode> nodesById = new Dictionary<string, BaseNode>();
        Dictionary<string, EventNodeDTO> nodeDtosById = new Dictionary<string, EventNodeDTO>();
        
        foreach (var nodeDto in eventTree.Nodes)
        {
            nodeDtosById[nodeDto.Id] = nodeDto;
            
            if (nodeDto.Type == "TextEvent"){
                nodesById[nodeDto.Id] = new TextEvent(
                    nodeDto.Id, 
                    nodeDto.Content, 
                    null, // Set later when the id is located
                    nodeDto.AutoAdvance,
                    nodeDto.DisplayProceedMessage,
                    nodeDto.SleepMils
                );
            }
            else if (nodeDto.Type == "Chooser"){
                // Create a Chooser with empty options, we'll populate them later
                nodesById[nodeDto.Id] = new Chooser(
                    nodeDto.Id,
                    nodeDto.Content,
                    new List<Option>()
                );
            }
        }

        // Now connect the nodes
        foreach (var nodeDto in eventTree.Nodes)
        {
            if (nodeDto.Type == "TextEvent" && !string.IsNullOrEmpty(nodeDto.NextId))
            {
                if (nodesById.ContainsKey(nodeDto.NextId))
                {
                    // Use reflection to set the protected nextEvent field
                    var textEvent = (TextEvent)nodesById[nodeDto.Id];
                    textEvent.SetNextNode(nodesById[nodeDto.NextId]);

                    // var fieldInfo = typeof(TextEvent).GetField("nextEvent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    // fieldInfo?.SetValue(textEvent, nodesById[nodeDto.NextId]);
                }
                else
                {
                    Console.WriteLine($"Warning: NextId '{nodeDto.NextId}' not found for node '{nodeDto.Id}'");
                }
            }
            else if (nodeDto.Type == "Chooser")
            {
                var chooser = (Chooser)nodesById[nodeDto.Id];
                var dtoOptions = new List<Option>();

                foreach (var optionDto in nodeDto.Options)
                {
                    if (nodesById.ContainsKey(optionDto.NextId))
                    {
                        if (optionDto.Identifier.HasValue)
                        {
                            dtoOptions.Add(new Option(
                                optionDto.Name,
                                optionDto.Hidden,
                                nodesById[optionDto.NextId],
                                optionDto.Identifier.Value
                            ));
                        }
                        else
                        {
                            dtoOptions.Add(new Option(
                                optionDto.Name,
                                optionDto.Hidden,
                                nodesById[optionDto.NextId]
                            ));
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Warning: NextId '{optionDto.NextId}' not found for option '{optionDto.Name}' in node '{nodeDto.Id}'");
                    }
                }

                // Use reflection to set the protected options field
                chooser.SetOptions(dtoOptions);

                // var optionsField = typeof(Chooser).GetField("options", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                // optionsField?.SetValue(chooser, dtoOptions);
            }
        }

        // Find the root node (the one that isn't referenced by any other node)
        HashSet<string> referencedIds = new HashSet<string>();
        
        foreach (var nodeDto in eventTree.Nodes)
        {
            if (!string.IsNullOrEmpty(nodeDto.NextId))
            {
                referencedIds.Add(nodeDto.NextId);
            }
            
            if (nodeDto.Options != null)
            {
                foreach (var option in nodeDto.Options)
                {
                    if (!string.IsNullOrEmpty(option.NextId))
                    {
                        referencedIds.Add(option.NextId);
                    }
                }
            }
        }

        // Find nodes that aren't referenced (potential starting points)
        var rootCandidates = eventTree.Nodes
            .Where(n => !referencedIds.Contains(n.Id))
            .Select(n => n.Id)
            .ToList();

        if (rootCandidates.Count == 0)
        {
            // If no clear root, just use the first node
            return nodesById[eventTree.Nodes[0].Id];
        }
        else if (rootCandidates.Count == 1)
        {
            return nodesById[rootCandidates[0]];
        }
        else
        {
            Console.WriteLine($"Warning: Multiple root nodes found: {string.Join(", ", rootCandidates)}. Using the first one.");
            return nodesById[rootCandidates[0]];
        }
    }
}