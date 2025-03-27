public abstract class BaseEvent{
    public string id {get; private set;}
    protected bool autoAdvance = false;
    protected bool displayProceedMessage = false;
    protected int sleepMils;
    protected bool replayable = false;
    protected bool hasRun = false;
    const string PROCEED_MESSAGE = "Press Enter";
    public BaseEvent(string id, int sleepMils = 0){
        this.id = id;
        this.sleepMils = sleepMils;
    }
    /// <summary>
    /// Each event calls the main of the other.
    /// </summary>
    /// <returns></returns>
    public virtual void Main(){
        // Display my content
        DisplayContent();
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
    /// Display your nodes content
    /// </summary>
    public abstract void DisplayContent();
    // If this event is a chooser it will call options first
    public abstract BaseEvent? GetNextEvent();

    public virtual bool HasRun(){
        return hasRun;
    }

    public bool CanReplay(){
        return replayable;
    }
}