using ToolBox;
using System.IO; 

public class Journal{
    private List<Entry> _entries = new List<Entry>();
    public string _title = null;
    // This makes sure the object knows where it's file is stored. It streamlines the file process.
    /// <summary>
    /// The path to the file, null if the file has not been saved or loaded.
    /// </summary>
    public string _savePath = "D:/Users/Jordan/Documents/BYUI/CSE 210/cse210-projects/prove/Develop02/journals/";

    public void Display(){
        // display the entries
        foreach(Entry entry in _entries){
            Console.WriteLine("=================================");
            entry.Display();
        }
        Console.WriteLine("=================================");
    }

    public void AddEntry(Entry entry){
        _entries.Add(entry);
    }
    public void Save(){
        if(_title == null){
            Console.Write("Please name your Journal: ");
            _title = Console.ReadLine();
        }
        using (StreamWriter outputFile = new StreamWriter($"{_savePath}{_title}.psv")){
            outputFile.WriteLine("Date|prompt|response");
            foreach(Entry entry in _entries){
                outputFile.WriteLine($"{entry._date}|{entry._prompt}|{entry._response}");
            }
        }
    }

    public static Journal Load(string path){
        string[] lines = File.ReadAllLines(path);

        Journal retJournal = new Journal();

        // This part gets the name of the file minus the extension and the path
        string[] splitPath = path.Split("|");
        string fileName = splitPath[splitPath.Length-1];
        retJournal._title = fileName.Substring(0, fileName.IndexOf('.'));

        // begin iterate the lines and create entries
        for(int i = 1; i < lines.Length;i++){
            string line = lines[i];
            string[] items = line.Split("|");

            Entry entry = new Entry();
            entry._date = items[0];
            entry._prompt = items[1];
            entry._response = items[2];
            retJournal.AddEntry(entry);
        }

        return retJournal;
    }
}