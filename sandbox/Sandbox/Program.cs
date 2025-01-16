using System;
using System.Runtime.InteropServices;
using ToolBox;
// This is so sick!!

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Sandbox World!");
        Thingy thing = new Thingy();
        thing.func();
        int number = InputValidation.PromptInt("Enter a number: ", 0, 5);
        Console.WriteLine(number);
    }
}

public class Thingy{
    public Thingy(){
        // create a thingy
    }

    public void func(){

    }
}