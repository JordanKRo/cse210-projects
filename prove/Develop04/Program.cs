using System;
using ToolBox;

class Program
{
    static async Task Main(string[] args)
    {
        ReflectionPrompt prompt1 = new ReflectionPrompt("Think of a time when you did something really difficult.");
        ReflectionPrompt prompt2 = new ReflectionPrompt("Think of a time when you stood up for someone else.");
        ReflectionPrompt prompt3 = new ReflectionPrompt("Think of a time when you were really proud of yourself.");

        List<string> followUps = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        List<string> listingActivityPrompts = new List<string> {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        List<ReflectionPrompt> prompts = new List<ReflectionPrompt> { prompt1, prompt2, prompt3 };
        prompts.ForEach(prompt => prompt.SetFollowUps(followUps));
        while (true){
            Activity.SafeClearConsole();
            Console.WriteLine("Menu Options:");
            int option = InputValidation.PromptOptions("Select a choice from the menu above: ", new List<string> { "Start Breathing Activity", "Start Reflection Activity", "Start Listing Activity", "Exit" });
            Activity currentActivity = null;
            switch (option)
            {
                case 0:
                    currentActivity = new BreathingActivity(4, "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.", 4);
                    break;
                case 1:
                    currentActivity = new ReflectionActivity(4, "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.", prompts, 4, 8);
                    break;
                case 2:
                    currentActivity = new ListingActivity(4, "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.", listingActivityPrompts);
                    break;              
            }
            if (option != 3){
                Activity.SafeClearConsole();
                await currentActivity.Start();
            } else {
                Console.WriteLine("\nGoodbye!");
                break;
            }
        }
    }
}