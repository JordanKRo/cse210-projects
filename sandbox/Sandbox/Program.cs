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
        float number = InputValidation.PromptFloat("Enter a number: ", 0);
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