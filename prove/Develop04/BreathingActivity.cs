using System.Formats.Asn1;
using System.Runtime.CompilerServices;

public class BreathingActivity : Activity{
    private int _cycleTime;

    public BreathingActivity(int delay, string description, int duration, int cycleTime) : base(delay, description, duration){
        _cycleTime = cycleTime;
    }

    public BreathingActivity(int delay, string description, int cycleTime) : base(delay, description, -1){
        _cycleTime = cycleTime;
    }
    
    public override async Task Start()
    {
        await DisplayIntro();

        SetTimer();
        do {
            await DisplayTimer("Breath In.. ", _cycleTime);
            Console.WriteLine('\n');
            await DisplayTimer("Breath Out.. ", _cycleTime);
            Console.WriteLine('\n');
        } while (TimerRunning());
        await DisplayOutro();
    }

    public override string GetName() {
        return "Breathing Activity"; 
    }

    public int GetCycleTime() {
        return _cycleTime;
    }

    public void SetCycleTime(int cycleTime) {
        _cycleTime = cycleTime;
    }

}