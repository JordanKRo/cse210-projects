using System.Text.Json;
using System.Text.Json.Serialization;

[JsonPolymorphic]
[JsonDerivedType(typeof(SimpleGoal), typeDiscriminator: "Simple")]
[JsonDerivedType(typeof(EternalGoal), typeDiscriminator: "Eternal")]
[JsonDerivedType(typeof(CheckListGoal), typeDiscriminator: "Checklist")]
public abstract class BaseGoal{
    // I know it's not supposed to be public but it needs to be accessible to the serializer.

    public string name { get; private set; }

    public string description { get; private set; }

    public int points { get; private set; }

    public BaseGoal(string name, string description, int points){
        this.name = name;
        this.description = description;
        this.points = points;
    }
    /// <summary>
    /// Returns if this goal should be marked off
    /// </summary>
    /// <returns></returns>
    public abstract bool IsComplete();
    /// <summary>
    /// Returns the number of points accrued from this Goal
    /// </summary>
    /// <returns></returns>
    public abstract int Evaluate();
    public abstract void Mark();
    /// <summary>
    /// 
    /// </summary>
    /// <returns>The list display of this goal.</returns>
    public virtual string GetString(){
        string check = IsComplete() ? "X" : " ";
        return $"[ {check} ] {name} ({description})";
    }
}