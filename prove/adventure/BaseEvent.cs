public abstract class BaseNode{
    public string id {get; private set;}
    protected bool autoAdvance = false;
    protected bool displayProceedMessage = true;
    protected int sleepMils;
    // This part will be implemented later
    protected bool replayable = false;
    protected bool hasRun = false;
    const string PROCEED_MESSAGE = "\nPress Enter";
    public BaseNode(string id, int sleepMils = 0, bool autoAdvance = false, bool displayProceedMessage = true){
        this.id = id;
        this.sleepMils = sleepMils;
        this.autoAdvance = autoAdvance;
        this.displayProceedMessage = displayProceedMessage;
    }
    /// <summary>
    /// Each event calls the main of the other.
    /// </summary>
    /// <returns></returns>
    public virtual void Main(){
        // Display my content
        OnExecute();
        hasRun = true;
        Thread.Sleep(sleepMils);
        if (!autoAdvance){
            if (displayProceedMessage){
                Console.WriteLine(PROCEED_MESSAGE);
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

    public virtual bool HasRun(){
        return hasRun;
    }

    public bool CanReplay(){
        return replayable;
    }
}