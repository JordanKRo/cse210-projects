public abstract class BaseEvent{
    public string id {get; private set;}
    protected bool autoAdvance = false;
    protected bool isDone = false;
    protected int forceWait = 0;
    public BaseEvent(){

    }
    /// <summary>
    /// Each event calls the main of the other.
    /// </summary>
    /// <returns></returns>
    public virtual async Task Main(){
        Console.WriteLine(GetContent());
        await Task.Delay(forceWait);

        await GetNextEvent().Main();
    }

    public abstract string GetContent();
    // If this event is a chooser it will call options first
    public abstract BaseEvent GetNextEvent();

    public virtual bool IsDone(){
        return isDone;
    }
}