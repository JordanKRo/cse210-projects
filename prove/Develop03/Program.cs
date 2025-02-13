using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {

        // Console.WriteLine(Path.GetFullPath("scriptureLib.json") + " : path");
        // Library owns the Client
        Library library = new Library(Path.GetFullPath("scriptureLib.json"));

        
        // check if api is enabled, then check if can obtain book cache.

        // string testText = "The quick brown fox jumps, over the lazy dog.";
        // string text2 = "This is the second verse.";
        
        // Scripture scripture = new Scripture([testText, text2], new Reference("FakeBook", 2, 5, 6));
        
        // Comprehension comprehension = new Comprehension(scripture);

        // comprehension.Start();

        List<Scripture> scr = library.GetAllScriptures();

        foreach(Scripture s in scr){
            Console.WriteLine(s.GetDisplay());
            Console.WriteLine();
        }
        
        // await clientTest();
    }

    private static async Task clientTest(){
        ScriptureClient client = new ScriptureClient();

        await client.GetBooks();
        Console.WriteLine("Got books");

        Scripture result = await client.GetScriptureFromApi(new Reference("Alma", 3, 2, 4));

        if(result != null){
            Console.WriteLine(result.GetDisplay());
        } else {
            Console.WriteLine("SAD");
        }
    }

}