using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Develop03 World!");

        string testText = "For behold, it came to pass that the Lord spake unto my father, yea, even in a dream, and said unto him: Blessed art thou Lehi, because of the things which thou hast done; and because thou hast been faithful and declared unto this people the things which I commanded thee, behold, they seek to take away thy life.";
        Verse verse = new Verse(testText, 2);
        while(verse.GetNumberVisible() > 0){
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(verse.GetDisplay());

            Console.ReadLine();
            verse.HideWords(2);
        }
        
    }
}