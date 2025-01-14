using System;

class Program
{
    const float PASSING_THRESHOLD = 70f;
    static void Main(string[] args)
    {
        // was the input from the last pass valid
        bool validInput = false;
        while(!validInput){
            Console.Write("What is the grade percentage? ");
            string input = Console.ReadLine();
            Console.WriteLine();
            float value = -1;
            if(float.TryParse(input, out float result)){
                if(!(result >= 0 && result <= 100)){
                    Console.WriteLine("Value is out of range (0-100)!");
                    continue;
                }
                validInput = true;
                value = result;
                string letter = "";
                string letterGrade = "-";
                bool passed = false;

                // letter
                if(value == 100f){
                    letter = "A";
                }else if(value >= 90f){
                    letter = "A";
                }else if(value >= 80f){
                    letter = "B";
                }else if(value >= 70f){
                    letter = "C";
                }else if(value >= 60f){
                    letter = "D";
                }else if(value < 60f){
                    letter = "F";
                }

                letterGrade = letter;
                
                // get sign
                int firstDigit = (int)(value % 10f);
                bool minusSign = firstDigit < 3;
                bool plusSign = firstDigit >= 7;
                if(minusSign && letterGrade != "F"){
                    letterGrade += '-';
                } else if(plusSign && letterGrade != "A" && letterGrade != "F"){
                    letterGrade += '+';
                }

                Console.WriteLine($"Your letter grade: {letterGrade}");

                passed = value >= PASSING_THRESHOLD;
                if(passed){
                    if(letter == "A"){
                        Console.WriteLine($"Congratulations, You passed with an {letterGrade}!");
                    }else{
                        Console.WriteLine($"Congratulations, You passed with a {letterGrade}!");
                    }
                    
                }else{
                    Console.WriteLine($"Sorry, a grade of {letterGrade} is not enough to pass. It happens to the best of us.");
                }
            }else{
                Console.WriteLine("Please Enter a valid number!");
            }
        }
        
    }
}