using System;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Develop03 World!");

        string testText = "The quick brown fox jumps, over the lazy dog.";
        string text2 = "This is the second verse.";
        
        Scripture scripture = new Scripture([testText, text2], new Reference("FakeBook", 2, 5, 6));
        
        Comprehension comprehension = new Comprehension(scripture);

        comprehension.Start();
        
    }
}