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
            new CheckListGoal("Math", "GoalDescriptionMath", 3, 10, 30),
            new EternalGoal("Attend the temple", "Description", 10)
        };

        goals[0].Mark();
        goals[0].Mark();
        goals[1].Mark();
        goals[2].Mark();
        int pointsTotal = 0;

        for(var i = 0; i < goals.Count;i++){
            Console.WriteLine($"{i + 1}. {goals[i].GetString()}");
            pointsTotal += goals[i].Evaluate();
        }
        Console.WriteLine(pointsTotal);

        
    }

    public static void Save(List<BaseGoal> goals){
        string json = JsonSerializer.Serialize(goals);
        Console.WriteLine(json);
        
    }
}