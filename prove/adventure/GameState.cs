using System.Xml.Linq;

public class GameState
{
    private Dictionary<string, dynamic> gameVariables = new Dictionary<string, dynamic>();
    private BaseNode? lastCheckpointNode;
    private string? filePath;

    public GameState()
    {

    }

    public GameState(string filePath)
    {
        this.filePath = filePath;

        // Attempt to load
        Load();
    }

    private static GameState? currentGameState;

    public static void SetGameState(GameState state)
    {
        currentGameState = state;
    }

    public void Set(string variable, dynamic value)
    {
        gameVariables[variable] = value;

        if (filePath != null)
        {
            Save();
        }

    }

    public dynamic Get(string variable, dynamic defaultValue)
    {
        gameVariables.TryGetValue(variable, out dynamic? value);
        return value ?? defaultValue;
    }

    public void SetCurrentNode(BaseNode node)
    {
        lastCheckpointNode = node;
        if (lastCheckpointNode.IsCheckPoint())
        {
            if (filePath != null)
            {
                Save();
            }
        }
    }

    public BaseNode? GetCurrentNode()
    {
        return lastCheckpointNode;
    }
    /// <summary>
    /// saves to the file path
    /// </summary>
    public void Save()
    {
        if (filePath == null)
        {
            throw new InvalidOperationException("File path is not set.");
        }

        var saveData = new
        {
            Save = new
            {
                NodeID = lastCheckpointNode?.id,
                vars = gameVariables
            }
        };

        string json = System.Text.Json.JsonSerializer.Serialize(saveData, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(filePath, json);
        /*
        stored as
        {
            "Save": {
                "NodeID": "Id of the current node"
                "vars":{

                }
            }
        }
        */
    }
    /// <summary>
    /// If a file path has a valid state the state will be updated using the file
    /// </summary>
    public void Load()
    {
        if (filePath == null || !File.Exists(filePath))
        {
            return;
        }

        string json = File.ReadAllText(filePath);

        Dictionary<string, dynamic>? saveData;
        try
        {
            saveData = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, dynamic>>(json);
        }
        catch
        {
            Console.WriteLine("File cannot be parsed");
            return;
        }


        if (saveData != null && saveData.ContainsKey("Save"))
        {
            if (saveData["Save"] is System.Text.Json.JsonElement saveElement)
            {
                Console.WriteLine("Loading Game State...");

                if (saveElement.ValueKind != System.Text.Json.JsonValueKind.Object)
                {
                    Console.WriteLine("Malformed file cannot be opened!");
                    return;
                }

                // Check for NodeID
                if (saveElement.TryGetProperty("NodeID", out var nodeIdElement) &&
                    nodeIdElement.ValueKind == System.Text.Json.JsonValueKind.String)
                {
                    string nodeId = nodeIdElement.GetString();

                    lastCheckpointNode = BaseNode.FindByID(nodeId);
                    if (lastCheckpointNode == null)
                    {
                        Console.WriteLine("Could not load the game entry point...");
                    }
                }

                // Check for vars
                if (saveElement.TryGetProperty("vars", out var varsElement) &&
                    varsElement.ValueKind == System.Text.Json.JsonValueKind.Object)
                {
                    // Deserialize to a Dictionary
                    gameVariables = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, dynamic>>(
                        varsElement.GetRawText()) ?? new Dictionary<string, dynamic>();
                }
            }
        }
    }

    /// <summary>
    /// Refreshes the state and clears the file
    /// </summary>
    public void Dispose(){
        lastCheckpointNode = null;
        gameVariables = new Dictionary<string, dynamic>();
        Save();
    }

    public static GameState GetGameState()
    {
        if (currentGameState == null)
        {
            currentGameState = new GameState();
        }
        return currentGameState;
    }
}