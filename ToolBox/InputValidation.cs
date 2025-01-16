namespace ToolBox;
using System;

public class InputValidation
{
    /// <summary>
    /// Prompts the user for an integer value, does not return until a valid integer is entered
    /// </summary>
    /// <param name="prompt">The text to display ahead of the user's input.</param>
    /// <returns>The integer the user entered</returns>
    public static int PromptInt(string prompt, string errorMessage = "Please enter a valid integer"){

        // TODO use do while
        while(true){
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";
            if(int.TryParse(input, out int result)){
                return result;
            }else{
                Console.WriteLine(errorMessage);
            }
        }
    }

    [Obsolete("Unfinished!!!")]
    public static int PromptInt(string prompt, int minValue, int maxValue){
        while(true){
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";
            if(int.TryParse(input, out int result)){
                return result;
            }else{
                // Console.WriteLine(errorMessage);
            }
        }
    }
}