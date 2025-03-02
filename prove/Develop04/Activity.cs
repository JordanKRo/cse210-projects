using System.Reflection;
using System.Runtime.InteropServices;

public class Activity{
    protected double _delay;
    protected string _intro;
    protected double _duration;
    protected string _completionMessage;

    protected Activity(double delay, string description, double duration, string completionMessage){
        _delay = delay;
        _intro = description;
        _duration = duration;
        _completionMessage = completionMessage;
    }

    public virtual async Task Start(){
        await Spinner("Uh oh", 10);
    }

    protected void DisplayIntro(){
        Console.WriteLine(_intro);
    }

    protected void DisplayOutro(){
        Console.WriteLine($"You have completed another {_duration} seconds of the {GetName()}");
    }

    public double GetDelay(){
        return _delay;
    }

    public string GetIntro(){
        return _intro;
    }

    public double GetDuration(){
        return _duration;
    }

    public string GetCompletionMessage(){
        return _completionMessage;
    }

    public virtual string GetName(){
        return "Activity";
    }

    public void SetDelay(double delay){
        _delay = delay;
    }

    public void SetIntro(string intro){
        _intro = intro;
    }

    public void SetDuration(double duration){
        _duration = duration;
    }

    public void SetCompletionMessage(string message){
        _completionMessage = message;
    }

    public static async Task Spinner(string message, double seconds, int frameTime = 100, bool leaveMessage = true){

        char[] frames = ['|','/','-','\\',];
        
        DateTime end = DateTime.Now.AddSeconds(seconds);
        Console.Write(" ");
        int frame = 0;
        string lastWrite = "";
        Console.Write(message);
        while (DateTime.Now < end) {
            frame++;
            lastWrite = $"\r{message}{frames[frame % 4]}";
            Console.Write(lastWrite);
            await Task.Delay(frameTime);
        }
        Console.Write($"\r");
        Console.Write(new string(' ',lastWrite.Length));
        if (leaveMessage) {
            Console.Write($"\r{message}");
        }
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
        Console.WriteLine();
    }
}