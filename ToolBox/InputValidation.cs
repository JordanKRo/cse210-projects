namespace ToolBox;
using System;

public class InputValidation
{
    /// <summary>
    /// Prompts the user for an integer value, does not return until a valid integer is entered
    /// </summary>
    /// <param name="prompt">The text to display ahead of the user's input.</param>
    /// <returns>The integer the user entered</returns>
    public static int PromptInt(string prompt){
        string errorMessage = "Please enter a valid integer";
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

    public static int PromptInt(string prompt, int? minValue = null, int? maxValue = null){
        string valueErrorMessage = "Please enter a valid integer!";
        string minErrorMessage = "Please enter a value greater than {0}!";
        string maxErrorMessage = "Please enter a value less than {1}!";
        string boundsErrorMessage = "Please enter a value between {0} and {1}!";

        while(true){
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";
            if(int.TryParse(input, out int result)){
                if(minValue != null && maxValue != null){
                    if(result > maxValue || result < minValue){
                        Console.WriteLine(string.Format(boundsErrorMessage, minValue, maxValue, result));
                        continue;
                    }
                }else{
                    if(maxValue != null && result > maxValue){
                        Console.WriteLine(string.Format(maxErrorMessage, minValue, maxValue, result));
                        continue;
                    }
                    if(minValue != null && result < minValue){
                        Console.WriteLine(string.Format(minErrorMessage, minValue, maxValue, result));
                        continue;
                    }
                }
                
                return result;
            }else{
                Console.WriteLine(string.Format(valueErrorMessage, minValue, maxValue, null));
            }
        }
    }

    public static float PromptFloat(string prompt, int? minValue = null, int? maxValue = null){
        string valueErrorMessage = "Please enter a valid number!";
        string minErrorMessage = "Please enter a value greater than {0}!";
        string maxErrorMessage = "Please enter a value less than {1}!";
        string boundsErrorMessage = "Please enter a value between {0} and {1}!";

        while(true){
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";
            if(float.TryParse(input, out float result)){
                if(minValue != null && maxValue != null){
                    if(result > maxValue || result < minValue){
                        Console.WriteLine(string.Format(boundsErrorMessage, minValue, maxValue, result));
                        continue;
                    }
                }else{
                    if(maxValue != null && result > maxValue){
                        Console.WriteLine(string.Format(maxErrorMessage, minValue, maxValue, result));
                        continue;
                    }
                    if(minValue != null && result < minValue){
                        Console.WriteLine(string.Format(minErrorMessage, minValue, maxValue, result));
                        continue;
                    }
                }
                
                return result;
            }else{
                Console.WriteLine(string.Format(valueErrorMessage, minValue, maxValue, null));
            }
        }
    }
}