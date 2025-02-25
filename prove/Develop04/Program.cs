using System;

class Program
{
    static async Task Main(string[] args)
    {

        Activity breathing = new BreathingActivity(6, "This is a breathing activity.",15,"Well done!",6);
        await breathing.Start();
    }
}