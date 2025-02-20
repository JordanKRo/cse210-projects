using System;

class Program
{
    static void Main(string[] args)
    {
        Assignment math = new MathAssignment("Jordan", "33333", "4.2", "1,5,7,9,11,14-20");
        Assignment writing = new WritingAssignment("Jordan", "rhetorical", "Citing Sources");
        Assignment cse = new Assignment("Jordan", "Inheritance");

        Console.WriteLine(math.GetSummary());
        Console.WriteLine(writing.GetSummary());
        Console.WriteLine(cse.GetSummary());
    }
}