using System.Text.Json;
using System.Text.Json.Serialization;

public abstract class BaseGoal{
    // I know it's not supposed to be public but it needs to be accessible to the serializer.
    [JsonPropertyName("name")]
    public string _name { get; private set; }
    [JsonPropertyName("description")]
    public string _description { get; private set; }
    [JsonPropertyName("points")]
    public int _points { get; private set; }

    public BaseGoal(string name, string description, int points){
        _name = name;
        _description = description;
        _points = points;
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
        return $"[ {check} ] {_name} ({_description})";
    }
}