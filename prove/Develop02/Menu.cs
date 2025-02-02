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

            if (Directory.Exists(userPath)) {
                // if is a directory
                _workingDirectory = userPath;

            } else if (File.Exists(userPath)) {
                return userPath;
            } else {
                DisplayNotification("The path specified does not exist or cannot be opened.");
                return null;
            }
        }
        string[] saveFiles = Directory.GetFiles(_workingDirectory, "*.psv");
        List<string> fileOptions = [.. saveFiles, "Cancel"];
        int selection = PromptOptions(fileOptions);
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
        while (ret == null){
            // get input from the user
            Console.Write("Enter a number from the list above: ");
            string input = Console.ReadLine();
            // validate the input is a number and parse it
            if (int.TryParse(input, out int result)) {
                if (result >= 1 && result <= options.Count){
                    ret = result;
                }
            } else {
                Console.WriteLine("Enter a valid integer!");
            }
        }
        // returns the users selection starting from 0 (makes it easier for other layers to use)
        return ret - 1 ?? 1;
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
}