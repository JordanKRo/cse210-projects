using System.Reflection;

public class Activity{
    protected double _delay;
    protected string _description;
    protected double _duration;
    protected string _completionMessage;

    protected Activity(double delay, string description, double duration, string completionMessage){
        _delay = delay;
        _description = description;
        _duration = duration;
        _completionMessage = completionMessage;
    }

    public virtual void Start(){

    }

    public static async Task Spinner(string message, double seconds){

        char[] frames = ['|','/','-','\\',];
        
        DateTime end = DateTime.Now.AddSeconds(seconds);
        Console.Write(" ");
        int frame = 0;
        while (DateTime.Now < end) {
            frame++;
            Console.Write($"\b{frames[frame % 4]}");
            await Task.Delay(100);
        }
        Console.Write(new string(' ', Console.WindowWidth));
    }

    public static async Task Timer(string message, double seconds){
        DateTime end = DateTime.Now.AddSeconds(seconds);

        double secondsLeft = (end - DateTime.Now).Seconds;

        while (secondsLeft > 0) {
            Console.Write($"{message}{secondsLeft}");
            await Task.Delay(100);
            clearLine();
            
        }
        
    }

    private static void clearLine(){
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
    }

}