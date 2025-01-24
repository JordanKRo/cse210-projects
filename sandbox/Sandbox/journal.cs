public class Journal{
    private List<Entry> _entries = new List<Entry>();

    public void Display(){
        // display the entries
    }

    public void Save(){
        // save the Journal
    }

    public void Load(){
        // replace the entries with the new ones in the file
    }

    public void AddEntry(Entry entry){
        _entries.Add(entry);
    }
}