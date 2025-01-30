using ToolBox;
using System.IO; 

public class Journal{
    private List<Entry> _entries = new List<Entry>();
    public string _title;
    // This makes sure the object knows where it's file is stored. It streamlines the file process.
    /// <summary>
    /// The path to the file, null if the file has not been saved or loaded.
    /// </summary>
    public string _savePath = "\\journals\\";

    public void Display(){
        // display the entries
        foreach(Entry entry in _entries){
            entry.Display();
        }
    }

    public void Save(){
        // StreamWriter outputFile = new StreamWriter($"{_savePath}{_title}");
        
    }

    public void AddEntry(Entry entry){
        _entries.Add(entry);
    }

    public static Journal Load(){
        // replace the entries with the new ones in the file
        throw new NotImplementedException("File loading is not implemented");
    }
}