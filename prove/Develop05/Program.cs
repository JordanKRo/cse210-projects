using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        List<BaseGoal> goals = new List<BaseGoal> {
            new SimpleGoal("GoalName", "GoalDescription", 3),
            new CheckListGoal("Math", "GoalDescriptionMath", 3, 10)
        };

        Save(goals);
    }

    public static void Save(List<BaseGoal> goals){
        string json = JsonSerializer.Serialize(goals);
        Console.WriteLine(json);
        
    }
}