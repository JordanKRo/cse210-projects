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
            "Exit"
        ];


        bool exit = false;
        Journal currentJournal = new Journal();

        while (!exit){
            int option = menu.PromptOptions();
            switch (option){
                case 0:
                    Entry newEntry = new Entry();
                    newEntry._prompt = promptGenerator.GetRandomPrompt();
                    newEntry._response = InputValidation.PromptString("Enter your response to the prompt:\n");
                    newEntry.Sign();
                    currentJournal.AddEntry(newEntry);
                    break;
                case 1:
                    currentJournal.Display();
                    break;
                case 2:
                    currentJournal.Save();
                    break;
                case 3:

                    break;
                case 4:
                    exit = true;
                    break;
            }
        }
    }
}