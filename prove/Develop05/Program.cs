using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };

        options.Converters.Add(new JsonStringEnumConverter());

        List<BaseGoal> goals = new List<BaseGoal>{
            new SimpleGoal("Simple", "Short Description", 20),
            new EternalGoal("Eternal", "Eternal Description", 100)
        };
        goals[0].Mark();
        goals[1].Mark();
        goals[1].Mark();

        string json = JsonSerializer.Serialize(goals, options);
        Console.WriteLine(json);

        List<BaseGoal> loadedGoals = new List<BaseGoal>();

        loadedGoals = JsonSerializer.Deserialize<List<BaseGoal>>(json, options);
        int total = 0;
        foreach(BaseGoal gol in loadedGoals){
            Console.WriteLine(gol.GetString());
            total += gol.Evaluate();
        }
        Console.WriteLine("Total: " + total);

        
    }

    public static void Save(List<BaseGoal> goals){
        string json = JsonSerializer.Serialize(goals);
        Console.WriteLine(json);
        
    }
}