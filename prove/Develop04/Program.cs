using System;

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

        Activity breathing = new BreathingActivity(4, "This activity will help you relax and clear your mind. This activity will walk you through each breathing cycle.", 12, 4);
        await breathing.Start();
       
        Activity reflection = new ReflectionActivity(4, "This activity will help you reflect on times in your life you have shown strength and resilience.", prompts, 4, 4);
        await reflection.Start();

        Activity listing = new ListingActivity(4, "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.", listingActivityPrompts);
        await listing.Start();
    }
}