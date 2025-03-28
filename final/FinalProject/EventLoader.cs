using System.Text.Json;
using System.Text.Json.Serialization;

public class EventLoader
{
    // Transfer Objects
    private class EventNodeDTO
    {
        // Could be polymorphic but I might not have time so it is a mess
        // Common
        public required string Id { get; set; }
        public required string Type { get; set; }
        public required string Content { get; set; }
        public int SleepMils { get; set; } = 0;
        public bool checkpoint { get; set; } = true;
        public bool AutoAdvance { get; set; } = false;
        public bool DisplayProceedMessage { get; set; } = true;
        public string? NextId { get; set; }
        // Chooser
        public List<OptionDTO> Options { get; set; } = new List<OptionDTO>();
        // Switcher
        public List<SwitchOptionDTO>? SwitchOptions { get; set; } = new List<SwitchOptionDTO>();
        public string? Variable { get; set; } // Shared with WriteNode
        public dynamic? DefaultValue { get; set; }

        // WriteNode
        public dynamic? WriteValue { get; set; }
    }

    private class SwitchOptionDTO
    {
        public required dynamic DesiredValue { get; set; }
        public required string PathId { get; set; }
        public required string Domain { get; set; }
    }

    private class OptionDTO
    {
        public required string Name { get; set; }
        public bool Hidden { get; set; } = false;
        public required string NextId { get; set; }
        public char? Identifier { get; set; }
    }

    private class EventTreeDTO
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
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        };

        EventTreeDTO eventTree;
        try
        {
            eventTree = JsonSerializer.Deserialize<EventTreeDTO>(jsonString, options);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error parsing JSON tree, check syntax!!!");
            throw new FileLoadException($"Failed to parse Node tree {ex.Message}");
        }

        // First, create a dictionary of all nodes by their ID (but without connections)
        Dictionary<string, BaseNode> nodesById = new Dictionary<string, BaseNode>();
        // Keeps track of the transfer objects
        Dictionary<string, EventNodeDTO> nodeDtosById = new Dictionary<string, EventNodeDTO>();

        foreach (var nodeDto in eventTree.Nodes)
        {
            nodeDtosById[nodeDto.Id] = nodeDto;

            if (nodesById.Keys.Contains(nodeDto.Id)){
                // skip this one
                Console.WriteLine($"Warning: Another occurrence of node - '{nodeDto.Id}' was found.");
                continue;
            }

            switch (nodeDto.Type)
            {
                case "TextEvent":
                    nodesById[nodeDto.Id] = new TextEvent(
                    nodeDto.Id,
                    nodeDto.Content,
                    null, // Set later when the id is located
                    nodeDto.AutoAdvance,
                    nodeDto.DisplayProceedMessage,
                    nodeDto.SleepMils,
                    checkpoint: nodeDto.checkpoint
                    );
                    break;

                case "Chooser":
                    nodesById[nodeDto.Id] = new Chooser(
                    nodeDto.Id,
                    nodeDto.Content,
                    new List<Option>(),
                    checkpoint: nodeDto.checkpoint
                    );
                    break;
                case "DecoratedTextEvent":
                    nodesById[nodeDto.Id] = new DecoratedTextEvent(
                    nodeDto.Id, 
                    nodeDto.Content, 
                    null, 
                    autoAdvance: nodeDto.AutoAdvance, 
                    displayProceedMessage: nodeDto.DisplayProceedMessage, 
                    sleepMils: nodeDto.SleepMils,
                    checkpoint: nodeDto.checkpoint);
                    break;
                case "Switcher":
                    if (string.IsNullOrEmpty(nodeDto.Variable))
                    {
                        Console.WriteLine($"Warning: Switcher node '{nodeDto.Id}' is missing a Variable property.");
                        continue;
                    }
                    nodesById[nodeDto.Id] = new SwitchNode(
                        nodeDto.Id, 
                        new List<SwitchNode.SwitchOption>(), 
                        nodeDto.Variable, 
                        nodeDto.DefaultValue ?? ""
                    );
                    break;
                case "WriteNode":
                    if (string.IsNullOrEmpty(nodeDto.Variable))
                    {
                        Console.WriteLine($"Warning: WriteNode '{nodeDto.Id}' is missing a Variable property.");
                        continue;
                    }
                    if (string.IsNullOrEmpty(nodeDto.NextId))
                    {
                        Console.WriteLine($"Warning: WriteNode '{nodeDto.Id}' is missing a NextId property.");
                        continue;
                    }
                    if(nodeDto.WriteValue is JsonElement el && el.ValueKind == JsonValueKind.String){
                        nodeDto.WriteValue = el.ToString();
                    }
                    // We'll set the next node later
                    nodesById[nodeDto.Id] = new WriteNode(
                        nodeDto.Id,
                        nodeDto.Variable,
                        nodeDto.WriteValue,
                        null // Set later
                    );
                    break;
            }
        }

        // Now connect the nodes
        foreach (var nodeDto in eventTree.Nodes)
        {
            // Handle simple nodes with a next event (TextEvent, DecoratedTextEvent, WriteNode)
            if ((nodeDto.Type == "TextEvent" || nodeDto.Type == "DecoratedTextEvent" || nodeDto.Type == "WriteNode") 
                && !string.IsNullOrEmpty(nodeDto.NextId))
            {
                if (nodesById.ContainsKey(nodeDto.NextId))
                {
                    switch (nodeDto.Type)
                    {
                        case "TextEvent":
                            ((TextEvent)nodesById[nodeDto.Id]).SetNextNode(nodesById[nodeDto.NextId]);
                            break;
                        case "DecoratedTextEvent":
                            ((DecoratedTextEvent)nodesById[nodeDto.Id]).SetNextNode(nodesById[nodeDto.NextId]);
                            break;
                        case "WriteNode":
                            ((WriteNode)nodesById[nodeDto.Id]).SetNextNode(nodesById[nodeDto.NextId]);
                            break;
                    }
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
                    // check if this option route ID is present
                    if (nodesById.ContainsKey(optionDto.NextId))
                    {
                        // Check if it has a custom identifier
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

                chooser.SetOptions(dtoOptions);
            } 
            else if (nodeDto.Type == "Switcher" && nodeDto.SwitchOptions != null)
            {
                if (nodesById.ContainsKey(nodeDto.Id) && nodesById[nodeDto.Id] is SwitchNode switcher)
                {
                    var switchOptions = new List<SwitchNode.SwitchOption>();

                    foreach (var switchOptionDTO in nodeDto.SwitchOptions)
                    {
                        // check if this option route ID is present
                        if (nodesById.ContainsKey(switchOptionDTO.PathId))
                        {
                            SwitchNode.SwitchOption.Domain domain;
                            switch (switchOptionDTO.Domain.ToUpper())
                            {
                                case "NOT":
                                    domain = SwitchNode.SwitchOption.Domain.NOT;
                                    break;
                                case "EQUAL":
                                    domain = SwitchNode.SwitchOption.Domain.EQUAL;
                                    break;
                                case "GREATER":
                                    domain = SwitchNode.SwitchOption.Domain.GREATER;
                                    break;
                                case "LESS":
                                    domain = SwitchNode.SwitchOption.Domain.LESS;
                                    break;
                                default:
                                    domain = SwitchNode.SwitchOption.Domain.EQUAL;
                                    Console.WriteLine($"Warning: Unknown domain '{switchOptionDTO.Domain}' in node '{nodeDto.Id}'. Using EQUAL.");
                                    break;
                            }
                            switchOptions.Add(new SwitchNode.SwitchOption(switchOptionDTO.DesiredValue, nodesById[switchOptionDTO.PathId], domain));
                        }
                        else
                        {
                            Console.WriteLine($"Warning: PathId '{switchOptionDTO.PathId}' not found in node '{nodeDto.Id}'");
                        }
                    }

                    try
                    {
                        switcher.SetOptions(switchOptions);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error setting switch options for node '{nodeDto.Id}': {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Warning: Could not find or cast node '{nodeDto.Id}' as SwitchNode");
                }
            }
        }

        // Find the root node (the one that isn't referenced by any other node)
        // turns out this functionality is not all that useful anymore, the first node is almost always at the top ¯\_(ツ)_/¯
        HashSet<string> referencedIds = new HashSet<string>();

        foreach (var nodeDto in eventTree.Nodes)
        {
            // If the node has a next ID
            if (!string.IsNullOrEmpty(nodeDto.NextId))
            {
                referencedIds.Add(nodeDto.NextId);
            }
            // If the node has options
            if (nodeDto.Options != null)
            {
                // Add all of the ids from the options
                foreach (var option in nodeDto.Options)
                {
                    if (!string.IsNullOrEmpty(option.NextId))
                    {
                        referencedIds.Add(option.NextId);
                    }
                }
            }
            // if it has switch options
            if (nodeDto.SwitchOptions != null)
            {
                // Add all of the switch options
                foreach (var switchOption in nodeDto.SwitchOptions)
                {
                    if (!string.IsNullOrEmpty(switchOption.PathId))
                    {
                        referencedIds.Add(switchOption.PathId);
                    }
                }
            }
        }

        List<string> rootCandidates = new List<string>();
        foreach(EventNodeDTO node in eventTree.Nodes){
            if(!referencedIds.Contains(node.Id)){
                rootCandidates.Add(node.Id);
            }
        }

        if (rootCandidates.Count == 0)
        {
            // If no clear root, just use the first node
            if (nodesById.Count == 0){
                throw new InvalidOperationException("Cannot find the root node in file!");
            }
            return nodesById[eventTree.Nodes[0].Id];
        }
        else if (rootCandidates.Count == 1)
        {
            return nodesById[rootCandidates[0]];
        }
        else
        {
            // uses first one
            return nodesById[rootCandidates[0]];
        }
    }
}