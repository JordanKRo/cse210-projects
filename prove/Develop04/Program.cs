using System;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello Develop04 World!");
        await Activity.Spinner(5);
        await Activity.Timer(10);
    }
}