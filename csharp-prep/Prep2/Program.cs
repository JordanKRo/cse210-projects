using System;

class Program
{
    const float PASSING_THRESHOLD = 70f;
    static void Main(string[] args)
    {
        Console.Write("What is the grade percentage? ");
        Console.WriteLine();
        string input = Console.ReadLine();
        float value = -1;
        if(float.TryParse(input, out float result)){
            value = result;

            string letterGrade = "-";
            bool passed = false;

            if(value == 100f){
                letterGrade = "A+";
            }else if(value >= 90){
                letterGrade = "A";
            }else if(value >= 80){
                letterGrade = "B";
            }else if(value >= 70){
                letterGrade = "C";
            }else if(value >= 60){
                letterGrade = "D";
            }else if(value < 60){
                letterGrade = "F";
            }

            passed = value >= PASSING_THRESHOLD;
            if(passed){
                Console.WriteLine("Congratulations, You passed!");
            }else{
                Console.WriteLine("Sorry, you did not score high enough to pass.");
            }
        }else{
            return;
        }
    }
}