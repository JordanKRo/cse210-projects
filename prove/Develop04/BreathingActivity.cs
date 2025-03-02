using System.Formats.Asn1;
using System.Runtime.CompilerServices;

public class BreathingActivity : Activity{
    private double _cycleTime;

    public BreathingActivity(double delay, string description, double duration, string completionMessage, double cycleTime) : base(delay, description, duration, completionMessage){
        _cycleTime = cycleTime;
    }
    public override async Task Start()
    {
        DisplayIntro();
        await Timer("Get Ready.. ", _delay);
        Console.WriteLine('\n');

        DateTime end = DateTime.Now.AddSeconds(_duration);
        double timeLeft;
        do {
            timeLeft = (end - DateTime.Now).Seconds;
            await Timer("Breath In.. ", _cycleTime);
            Console.WriteLine('\n');
            await Timer("Breath Out.. ", _cycleTime);
            Console.WriteLine('\n');
        } while (timeLeft > 0);
        DisplayOutro();
        Console.WriteLine('\n');
        await Spinner("", _delay);
        
    }

    public override string GetName()
    {
        return "Breathing Activity"; 
    }

    public double GetCycleTime(){
        return _cycleTime;
    }

}