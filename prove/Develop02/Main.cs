using System;
using System.Runtime.InteropServices;
using ToolBox;
// This is so sick!!

class JournalManager
{
    static void Main(string[] args)
    {
        PromptGenerator promptGenerator = new PromptGenerator();
        promptGenerator._prompts = [
            "Prompt 1",
            "prompt 2",
            "prompt 3"
        ];


        Menu menu = new Menu();
        menu._options = [
            "Write a new entry",
            "Display the Journal",
            "Save Journal",
            "Load Journal",
            "Create new",
            "Set save path",
            "Exit"
        ];


        bool exit = false;
        Journal currentJournal = new Journal();

        while (!exit){
            int option = menu.DisplayMain();
            switch (option) {
                case 0:
                    Entry newEntry = new Entry();
                    newEntry._prompt = promptGenerator.GetRandomPrompt();
                    newEntry.DisplayPrompt();
                    newEntry._response = InputValidation.PromptString($"Enter your response to the prompt:\n");
                    newEntry.Sign();
                    currentJournal.AddEntry(newEntry);
                    break;
                case 1:
                    Console.WriteLine();
                    currentJournal.Display();
                    Console.WriteLine();
                    break;
                case 2:
                    currentJournal.Save();
                    break;
                case 3:
                    // currentJournal = Journal.Load(menu.PromptString("Enter the path to your journal: "));
                    string path = menu.FileSelectDialogue();
                    Journal loadedJournal;
                    if (path != null){
                        loadedJournal = Journal.Load(path);

                        // if load is null make a new one
                        if (loadedJournal == null){
                            currentJournal = new Journal();
                        }else{
                            currentJournal = loadedJournal;
                            menu.DisplayNotification($"Successfully opened journal: {loadedJournal._title}");
                        }
                    }else{
                        // do nothing if a path was not returned
                    }
                    break;
                case 4:
                    currentJournal = new Journal();
                    break;
                case 5:
                    string cleanPath = Menu.SanitizePath(menu.PromptString("Enter the path to your save folder: "), out bool isDir) ;
                    if (cleanPath != null && isDir){
                        menu._workingDirectory = cleanPath;
                    }
                    break;
                case 6:
                    exit = true;
                    break;
            }
        }
    }
}