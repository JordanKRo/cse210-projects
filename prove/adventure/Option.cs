using System.Buffers.Text;

public class Option {
    protected string name;
    protected bool hidden;
    protected BaseEvent trigger;
    protected char? identifier;

    /// <summary>
    /// Create an option with a name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="hidden"></param>
    public Option(string name, bool hidden, BaseEvent trigger) {
        this.name = name;
        this.hidden = hidden;
        this.trigger = trigger;
    }
    public Option(string name, bool hidden, BaseEvent trigger, char identifier) {
        this.name = name;
        this.hidden = hidden;
        this.trigger = trigger;
        this.identifier = identifier;
    }

    public string Name {
        get { return name; }
        set { name = value; }
    }

    public bool Hidden {
        get { return hidden; }
        set { hidden = value; }
    }

    public char? Identifier {
        get {return identifier;}
    }

    public BaseEvent Node{
        get {return trigger;}
    }

    public override string ToString() {
        return $"{name}";
    }
}