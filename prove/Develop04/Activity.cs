using System.Reflection;
using System.Runtime.InteropServices;
using ToolBox;

public class Activity{
    protected int _delay = 3;
    protected string _intro = "This is an activity.";
    protected int _duration = 3;

    protected Activity(int delay, string description, int duration){
        _delay = delay;
        _intro = description;
        _duration = duration;
    }

    public virtual async Task Start(){
        await Spinner("", _duration);
    }

    protected async Task DisplayIntro(){
        Console.WriteLine($"Welcome to the {GetName()}.\n\n{_intro}\n");
        if (_duration <= 0) {
            _duration = InputValidation.PromptInt("How long, in seconds, would you like your session? ");
        } else {
            Console.WriteLine($"This session will last {_duration} seconds.\nPress enter to continue.");
            Console.ReadLine();
        }
        SafeClearConsole();
        await Spinner("Get Ready... ", _delay);
    }

    protected async Task DisplayOutro(){

        Console.WriteLine("Well Done!");
        await Spinner("", _delay);
        Console.WriteLine($"You have completed another {_duration} seconds of the {GetName()}");
        await Spinner("", _delay);
        
    }

    public int GetDelay(){
        return _delay;
    }

    public string GetIntro(){
        return _intro;
    }

    public int GetDuration(){
        return _duration;
    }

    public virtual string GetName(){
        return "Activity";
    }

    public void SetDelay(int delay){
        _delay = delay;
    }

    public void SetIntro(string intro){
        _intro = intro;
    }

    public void SetDuration(int duration){
        _duration = duration;
    }


    public static async Task Spinner(string message, double seconds, int frameTime = 100, bool leaveMessage = true){

        char[] frames = ['|','/','—','\\',];
        
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

    /// <summary>
    /// Clears the console without throwing an exception in debug.
    /// </summary>
    public static void SafeClearConsole(){
        try{
            Console.Clear();
        } catch (IOException e){
            Console.WriteLine(e.Message);
        }
    }
}