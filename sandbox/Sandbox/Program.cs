using System;
using System.Runtime.InteropServices;
using ToolBox;
// This is so sick!!

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        Menu menu = new Menu();
        menu._options = [
            "Write a new entry",
            "Display the Journal",
            "Save Journal",
            "Load Journal"
        ];


        bool exit = false;

        while(!exit){
            menu.PromptOptions();
        }
    }
}