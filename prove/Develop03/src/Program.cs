using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // OS compliant path
        string basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        string projectPath = Path.GetFullPath(Path.Combine(basePath, "..", "..", ".."));
        string path = Path.Combine(projectPath, "scriptureLib.json");
        // Library owns the Client
        Library library = new Library(path);

        
        // check if api is enabled, then check if can obtain book cache.
        
        Scripture scripture = await library.GetScripture();
        
        Comprehension comprehension = new Comprehension(scripture);

        comprehension.Start();
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