using System.Text.Json;
using System.Text.Json.Serialization;

public abstract class BaseGoal{
    protected string _name;
    protected string _description;
    protected int _points;

    public BaseGoal(string name, string description, int points){
        
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
    public abstract string GetString();
}