using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello Develop03 World!");

        // string testText = "The quick brown fox jumps, over the lazy dog.";
        // string text2 = "This is the second verse.";
        
        // Scripture scripture = new Scripture([testText, text2], new Reference("FakeBook", 2, 5, 6));
        
        // Comprehension comprehension = new Comprehension(scripture);

        // comprehension.Start();

        
        await clientTest();
    }

    private static async Task clientTest(){
        ScriptureClient client = new ScriptureClient();

        await client.GetBooks();
        Console.WriteLine("Got books");

        Scripture result = await client.GetScriptureFromApi(new Reference("3Nephi", 1, 1, 4));

        if(result != null){
            Console.WriteLine(result.GetDisplay());
        } else {
            Console.WriteLine("SAD");
        }
    }

}