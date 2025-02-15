using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        // OS compliant path
        string basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        string projectPath = Path.GetFullPath(Path.Combine(basePath, "..", "..", ".."));
        string path = Path.Combine(projectPath, "scriptureLib.json");
        // Library owns the Client
        Library library = new Library(path);

        
        // check if api is enabled, then check if can obtain book cache.
        
        Scripture scripture = library.GetScripture();
        
        Comprehension comprehension = new Comprehension(scripture);

        comprehension.Start();
    }

}