using System.Formats.Asn1;
using System.Runtime.CompilerServices;

public class BreathingActivity : Activity{
    private double _cycleTime;

    public BreathingActivity(int delay, string description, int duration, double cycleTime) : base(delay, description, duration){
        _cycleTime = cycleTime;
    }

    public BreathingActivity(int delay, string description, double cycleTime) : base(delay, description, -1){
        _cycleTime = cycleTime;
    }
    
    public override async Task Start()
    {
        await DisplayIntro();

        SetTimer();
        do {
            await Timer("Breath In.. ", _cycleTime);
            Console.WriteLine('\n');
            await Timer("Breath Out.. ", _cycleTime);
            Console.WriteLine('\n');
        } while (TimerRunning());
        await DisplayOutro();
    }

    public override string GetName()
    {
        return "Breathing Activity"; 
    }

    public double GetCycleTime(){
        return _cycleTime;
    }

}