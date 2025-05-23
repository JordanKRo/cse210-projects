using ToolBox;
using System.IO; 

public class Journal{
    private List<Entry> _entries = new List<Entry>();
    public string _title = null;
    // This makes sure the object knows where it's file is stored. It streamlines the file process.
    /// <summary>
    /// The path to the file, null if the file has not been saved or loaded.
    /// </summary>
    public string _savePath = null;

    public void Display() {
        // display the entries
        Console.WriteLine("=================================");
        Console.WriteLine(_title);
        foreach(Entry entry in _entries){
            Console.WriteLine("=================================");
            entry.Display();
            Console.WriteLine();
        }
        Console.WriteLine("=================================");
    }

    public void AddEntry(Entry entry) {
        _entries.Add(entry);
    }

    public void Save() {
        Menu menu = Menu.GetMenu();
        if (_title == null) {
            _title = menu.PromptString("Please name your Journal: ");
        }
        if (_savePath == null) {
            if (menu._workingDirectory != null) {
                _savePath = menu._workingDirectory;
            } else {
                while (true) {
                    _savePath = menu.PromptString("Enter the save path: ");
                    _savePath = Menu.SanitizePath(_savePath, out bool isDir);

                    if (_savePath != null && isDir){
                        break;
                    }
                }
            }
        }
        string fullPath = Path.Combine(_savePath, $"{_title}.psv");
        using (StreamWriter outputFile = new StreamWriter(fullPath)) {
            outputFile.WriteLine("Date|prompt|response");
            foreach(Entry entry in _entries){
                outputFile.WriteLine($"{entry._date}|{entry._prompt}|{entry._response}");
            }
        }
    }

    public static Journal Load(string path) {
        Journal retJournal = new Journal();
        try {
            string[] lines = File.ReadAllLines(path);
            
            string fileName = Path.GetFileName(path);

            retJournal._title = Path.GetFileNameWithoutExtension(path);
            
            retJournal._savePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar;

            // begin iterate the lines and create entries
            for (int i = 1; i < lines.Length;i++) {
                string line = lines[i];
                string[] items = line.Split("|");

                Entry entry = new Entry();
                entry._date = items[0];
                entry._prompt = items[1];
                entry._response = items[2];
                retJournal.AddEntry(entry);
            }
        } catch (FileNotFoundException) {
            Console.WriteLine("Could not open Journal");
            return null;
        } catch (Exception e) {
            Console.WriteLine($"Could not open Journal, an error occurred:\n{e}");
            return null;
        }
        
        return retJournal;
    }
}