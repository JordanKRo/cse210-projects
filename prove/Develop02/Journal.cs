public class Journal{
    private List<Entry> _entries = new List<Entry>();
    // This makes sure the object knows where it's file is stored. It streamlines the file process.
    /// <summary>
    /// The path to the file, null if the file has not been saved or loaded.
    /// </summary>
    private string _filePath;

    public void Display(){
        // display the entries
    }

    public void Save(){
        // save the Journal
    }

    public void AddEntry(Entry entry){
        _entries.Add(entry);
    }

    public static Journal Load(){
        // replace the entries with the new ones in the file
        throw new NotImplementedException("File loading is not implemented");
    }
}