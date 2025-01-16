using System;
using System.ComponentModel.DataAnnotations;

class Program
{
    static void Main(string[] args)
    {
        int guesses = 0;
        int secret_number = 0;
        secret_number = promptInt("Enter the secret number: ");

        int last_guess;
        
        do{
            last_guess = promptInt("Enter your guess: ");
            guesses++;
            
            if(last_guess > secret_number){
                Console.WriteLine("Lower!");
            }else if(last_guess < secret_number){
                Console.WriteLine("Higher!");
            }else if(last_guess == secret_number){
                Console.WriteLine($"You guessed the number {secret_number}");
            }else{
                // some kind of error occurred
                throw new Exception("Number was outside of expected constraints");
            }

        }while(last_guess != secret_number);
        

    }

    static int promptInt(string prompt, string errorMessage = "Please Enter a valid integer"){
        Console.Write(prompt);
        while(true){
            string input = Console.ReadLine();
            if(int.TryParse(input, out int result)){
                return result;
            }else{
                Console.WriteLine(errorMessage);
            }
        }
    }
}