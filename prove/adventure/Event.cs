public class Event{
    protected string text;
    protected List<Option> options = new List<Option>();
    protected bool hideOptions;
    protected Event? nextEvent;
    protected int time = 0;
    protected static List<Option> globalOptions = new List<Option>();
    public Event(){
        text = "Default Text";
    }

    public Event(string text, Event next){
        this.text = text;
        nextEvent = next;
    }

    public virtual string GetText(){
        return text;
    }

    public virtual Event? GetNextEvent(){
        return nextEvent;
    }

    public virtual void Start(){
        // Display text

        // Events do not display the options but they are invisible.
    }

    public virtual void Options(){

    }

    public static void SetGlobalOptions(){
        // set the global options
    }

    public static int DisplayChooser(List<Option> options, string prompt, string header = ""){
        // display the pre option header
        if(header != null && header.Length > 0){
            Console.WriteLine(header);
        }

        // display the options

        // display the prompt
        
        return 0;
    }
}