using System;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using ToolBox;

class Program
{
    static void Main(string[] args)
    {
        List<BaseGoal> loadedGoals = new List<BaseGoal>();
        bool done = false;
        while (!done){
            // Display points
            int totalPoints = 0;
            foreach(BaseGoal goal in loadedGoals){
                totalPoints += goal.Evaluate();
            }
            Console.WriteLine($"\nYou have {totalPoints} points\n");

            var selectedMenu = InputValidation.PromptOptions("Select an option from above: ", ["Create New Goal", "List Goals", "Save Goals", "Load Goals", "Record Event", "Quit"]);
            Console.WriteLine();
            switch (selectedMenu){
                case 0:
                    Console.WriteLine("Select an option from the menu");
                    var selectedType = InputValidation.PromptOptions("Which type of goal would you like to create: ", ["Simple Goal", "Eternal Goal", "Checklist Goal"]);
                    BaseGoal newGoal = null;
                    string name = InputValidation.PromptString("Enter the goal name: ");
                    string description = InputValidation.PromptString("Enter a short description for the goal: ");
                    int points = InputValidation.PromptInt("Enter the points asociated with the goal: ", minValue: 0);
                    switch (selectedType){
                        case 0:
                            newGoal = new SimpleGoal(name, description, points);
                            break;
                        case 1:
                            newGoal = new EternalGoal(name, description, points);
                            break;

                        case 2:
                            int requiredTimes = InputValidation.PromptInt("Enter the number of times this goal needs to be marked: ", minValue: 0);
                            int bonus = InputValidation.PromptInt("Enter the bonus points when completing the goal: ", minValue: 0);
                            newGoal = new CheckListGoal(name, description, points, requiredTimes, bonus);
                            break;
                    }
                    loadedGoals.Add(newGoal);
                    break;
                case 1:
                    DisplayGoals(loadedGoals);
                    break;
                case 2:
                    Save(loadedGoals);
                    break;
                case 3:
                    loadedGoals = Load();
                    break;
                case 4:
                    List<string> goalList = loadedGoals.Select(goal => goal.GetString()).ToList();
                    var selectedGoal = InputValidation.PromptOptions("Select a goal to record an event: ", goalList);
                    loadedGoals[selectedGoal].Mark();
                    break;
                case 5:
                    done = true;
                    break;
            }
        }

        
    }

    public static void DisplayGoals(List<BaseGoal> goals){
        for(var i = 0;i < goals.Count; i++){
            Console.WriteLine($"{i + 1}. {goals[i].GetString()}");
        }
    }

    public static void Save(List<BaseGoal> goals){
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };
        // This is really convenient, would you agree
        string json = JsonSerializer.Serialize(goals, options);

        string fileName = InputValidation.PromptString("Enter the file name: ");
        // Saved in a saves folder by the .exe
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "saves", fileName + ".json");

        File.WriteAllText(filePath, json);
    }

    public static List<BaseGoal> Load(){
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };
        string fileName = InputValidation.PromptString("Enter the file name to load: ");
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "saves", fileName + ".json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<BaseGoal>>(json, options);
        }
        else
        {
            Console.WriteLine("File not found.");
            return new List<BaseGoal>();
        }
    }
}