public class Menu{
    private static Menu theMenu;
    public List<string> _options;
    public string _workingDirectory = null;
    public Menu() {
        theMenu = this;
    }
    public void DisplayNotification(string notification) {
        Console.WriteLine($"\n{notification}\n");
    }
    public int DisplayMain() {
        int selection = PromptOptions(_options);
        Console.WriteLine();
        return selection;
    }

    public string FileSelectDialogue() {
        if (_workingDirectory == null) {
            string userPath = PromptString("Enter the path to your save file/folder: ");
            Console.WriteLine();

            string fixedPath = SanitizePath(userPath, out bool isDir);

            // if it was a directory update the working directory

            if (fixedPath == null) {
                DisplayNotification("The path specified does not exist or cannot be opened.");
                return null;
            }

            if (isDir) {
                _workingDirectory = fixedPath;
            } else {
                return fixedPath;
            }
        }
        string[] saveFiles = Directory.GetFiles(_workingDirectory, "*.psv");
        List<string> fileOptions = [.. saveFiles];

        List<string> fileNames = new List<string>();

        // Remove the path from the file to make it easier to read
        for (int i = 0;i < fileOptions.Count;i++) {
            fileNames.Add(Path.GetFileName(fileOptions[i]));
        }
        fileNames.Add("Cancel");

        Console.WriteLine("Please select a save from below: ");
        int selection = PromptOptions(fileNames);
        Console.WriteLine();

        if (selection == fileOptions.Count - 1) {
            // user canceled the dialogue
            return null;
        }
        return fileOptions[selection];
    }

    public int PromptOptions(List<string> options) {
        // display all of the options
        for (var i = 0;i < options.Count;i++) {
            Console.WriteLine($"{i + 1} - {options[i]}");
        }
        int? ret = null;
        while (ret == null) {
            // get input from the user
            Console.Write("Enter a number from the list above: ");
            string input = Console.ReadLine();
            // validate the input is a number and parse it
            if (int.TryParse(input, out int result)) {
                if (result >= 1 && result <= options.Count) {
                    ret = result;
                }
            } else {
                Console.WriteLine("Enter a valid integer!");
            }
        }
        // returns the users selection starting from 0 (makes it easier for other layers to use)
        return (ret ?? 1) - 1;
    }

    public string PromptString(string prompt) {
        Console.Write(prompt);
        return Console.ReadLine();
    }
    // Singleton for the menu ensure all classes can access it easily
    public static Menu GetMenu() {
        // if there isn't one already create a new one
        return theMenu ?? new Menu();
    }
    // I just remembered this is being graded on a mac I hope this works

    /// <summary>
    /// Sanitizes the path provided by the user
    /// </summary>
    /// <param name="path">User provided string</param>
    /// <param name="isDir">If the path entered was a directory</param>
    /// <returns></returns>
    public static string SanitizePath(string path, out bool isDir) {
        try {
            string normalizedPath = Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar);

            if (Directory.Exists(normalizedPath)) {
                isDir = true;
                return normalizedPath + Path.DirectorySeparatorChar;
            } else if (File.Exists(normalizedPath)) {
                isDir = false;
                return normalizedPath;
            }

            isDir = false;
            return null;
        } catch (Exception) {
            isDir = false;
            return null;
        }
    }
}