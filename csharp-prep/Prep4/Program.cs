using System;
using ToolBox;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        List<int> numbers = new List<int>();

        int last_input;
        do{
            last_input = InputValidation.PromptInt("Enter number: ");
            if(last_input == 0){
                break;
            }
            numbers.Add(last_input);
        }while(last_input != 0);

        float total = 0;
        foreach(float value in numbers){
            total += value;
        }
        float average = total / numbers.Count;
        Console.WriteLine(numbers[0]);
        Console.WriteLine($"The sum is : {total}");
        Console.WriteLine($"The average is : {average}");

        int largestValue = numbers[0];
        foreach(int number in numbers){
            if(number > largestValue){
                largestValue = number;
            }
        }

        int smallestValue = numbers[0];
        foreach(int number in numbers){
            if(number >= 0 && number < smallestValue){
                smallestValue = number;
            }
        }
    }

}