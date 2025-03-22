public abstract class BaseEvent{
    protected bool autoAdvance = false;
    protected bool isDone = false;
    protected int forceWait = 0;
    public BaseEvent(){

    }
    /// <summary>
    /// Called locally to display content
    /// </summary>
    /// <returns></returns>
    public virtual async Task Main(){
        Console.WriteLine(GetContent());
        await Task.Delay(forceWait);
        _ = GetNextEvent().Main();
    }

    public abstract string GetContent();
    public abstract BaseEvent GetNextEvent();

    public virtual bool IsDone(){
        return isDone;
    }
}