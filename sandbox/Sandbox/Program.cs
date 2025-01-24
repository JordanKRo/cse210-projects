using System;
using System.Runtime.InteropServices;
using ToolBox;
// This is so sick!!

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
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

        while(!exit){
            int option = menu.PromptOptions();
            switch(option){
                case 0:
                    Entry newEntry = new Entry();
                    newEntry._prompt = promptGenerator.GetRandomPrompt();
                    newEntry.Sign();
                    journal.AddEntry(newEntry);
                    break;
                case 1:
                    journal.Display();
                    break;
                case 2:
                    journal.Save();
                    break;
                case 3:
                    journal.Load();
                    break;
                case 4:
                    exit = true;
                    break;
            }
        }
    }
}