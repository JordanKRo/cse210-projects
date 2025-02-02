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
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
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
                    menu.DisplayNotification("Created a new Untitled Journal");
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