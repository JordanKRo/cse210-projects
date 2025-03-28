using System.Runtime.CompilerServices;

public abstract class BaseNode{
    public string id {get; private set;}
    protected bool autoAdvance = false;
    protected bool displayProceedMessage = true;
    protected int sleepMils;
    // This part will be implemented later
    protected bool replayable = false;
    protected bool hasRun = false;
    protected bool checkpoint = true;
    const string PROCEED_MESSAGE = "\nPress Enter";

    private static Dictionary<string,BaseNode> nodesById = new Dictionary<string,BaseNode>();
    public BaseNode(string id, int sleepMils = 0, bool autoAdvance = false, bool displayProceedMessage = true, bool checkpoint = true){
        this.id = id;
        this.sleepMils = sleepMils;
        this.autoAdvance = autoAdvance;
        this.displayProceedMessage = displayProceedMessage;
        this.checkpoint = checkpoint;

        // register myself
        if (nodesById.ContainsKey(id)){
            // Console.WriteLine($"Warning: node '{id}' was is being overwritten in registry");
            throw new ArgumentException($"A node with the ID '{id}' already exists.");
        }
        nodesById.Add(this.id, this);
    }
    /// <summary>
    /// Each event calls the main of the other.
    /// </summary>
    /// <returns></returns>
    public virtual void Main(){
        // Display my content
        GameState.GetGameState().SetCurrentNode(this);
        OnExecute();
        hasRun = true;
        Thread.Sleep(sleepMils);
        if (!autoAdvance){
            if (displayProceedMessage){
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(PROCEED_MESSAGE);
                Console.ResetColor();
            } else {
                Console.WriteLine();
            }
            
            Console.ReadLine();
        }
        // If I have an event then call it
        var nextEvent = GetNextEvent();
        if (nextEvent != null)
        {
            nextEvent.Main();
        }
    }
    /// <summary>
    /// Display your nodes content or perform the action
    /// </summary>
    public abstract void OnExecute();
    // If this event is a chooser it will call options first
    public abstract BaseNode? GetNextEvent();

    // public virtual bool HasRun(){
    //     return hasRun;
    // }

    public bool IsCheckPoint(){
        return checkpoint;
    }

    public bool CanReplay(){
        return replayable;
    }
    /// <summary>
    /// returns
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static BaseNode? FindByID(string id){
        nodesById.TryGetValue(id, out BaseNode? node);
        return node;
    }
}