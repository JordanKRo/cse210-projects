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
            "Set save path",
            "Exit"
        ];


        bool exit = false;
        Journal currentJournal = new Journal();

        while (!exit){
            int option = menu.DisplayMain();
            switch (option){
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
                    // if the load is null use the current one
                    Journal loadedJournal = Journal.Load(menu.FileSelectDialogue());
                    if (loadedJournal == null){
                        currentJournal = new Journal();
                    }else{
                        currentJournal = loadedJournal;
                        menu.DisplayNotification($"Successfully opened journal: {loadedJournal._title}");
                    }
                    break;
                case 4:
                    menu._workingDirectory = menu.PromptString("Enter the path to your save folder: ");
                    break;
                case 5:
                    exit = true;
                    break;
            }
        }
    }
}