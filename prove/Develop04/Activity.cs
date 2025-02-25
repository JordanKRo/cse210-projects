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

    public virtual async Task Start(){
        await Spinner("Uh oh", 10);
    }

    protected void DisplayIntro(){
        Console.WriteLine(_description);
    }

    protected void DisplayOutro(){
        Console.WriteLine(_completionMessage);
    }

    public static async Task Spinner(string message, double seconds){

        char[] frames = ['|','/','-','\\',];
        
        DateTime end = DateTime.Now.AddSeconds(seconds);
        Console.Write(" ");
        int frame = 0;
        string lastWrite = "";
        while (DateTime.Now < end) {
            frame++;
            lastWrite = $"\b{frames[frame % 4]}";
            Console.Write(lastWrite);
            await Task.Delay(100);
        }
        Console.Write($"\r");
        Console.Write(new string(' ',lastWrite.Length));
        Console.WriteLine();
    }

    public static async Task Timer(string message, double seconds){
        DateTime end = DateTime.Now.AddSeconds(seconds);

        double secondsLeft = (end - DateTime.Now).Seconds;
        string lastWrite = "";
        while (secondsLeft > 0) {
            secondsLeft = (end - DateTime.Now).Seconds; 
            string newWrite = $"\r{message}{secondsLeft}";
            int extra = lastWrite.Length - newWrite.Length;
            if (extra < 0){
                extra = 0;
            }
            Console.Write(newWrite + new string(' ', extra));
            await Task.Delay(100);
            // Console.Write($"\r" + new string(' ',lastWrite.Length) + "\r");
        }
    }
}