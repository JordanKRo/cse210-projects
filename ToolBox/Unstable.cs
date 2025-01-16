[AttributeUsage(AttributeTargets.All)]
public class UnstableAttribute : Attribute 
{
    public string Reason { get; }
    public UnstableAttribute(string reason) 
    {
        Reason = reason;
    }
}