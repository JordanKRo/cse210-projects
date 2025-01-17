using System;
using ToolBox;

class Program
{
    static void Main(string[] args)
    {
        
    }

    static void DisplayWelcome(){
        Console.WriteLine("Welcome to the program");
    }

    static string PromptUsername(){
        Console.Write("What is your username: ");
        string username = Console.ReadLine() ?? "";
        return username;
    }

    static int promptUserNumber(){
        // See class for my implementation
        int favNumber = InputValidation.PromptInt("What is your favorite number: ");
        return favNumber;
    }

    static float SquareNumber(float input){
        return (float) Math.Pow(input,2);
    }

    static void DisplayResult(string name, int number){
        Console.WriteLine($"{name} the square of your number is {SquareNumber(number)}");
    }
}