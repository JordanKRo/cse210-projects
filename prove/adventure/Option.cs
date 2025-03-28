using System.Buffers.Text;

public class Option {
    protected string name;
    protected bool hidden;
    protected BaseNode trigger;
    protected char? identifier;

    /// <summary>
    /// Create an option with a name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="hidden"></param>
    public Option(string name, bool hidden, BaseNode trigger) {
        this.name = name;
        this.hidden = hidden;
        this.trigger = trigger;
    }
    public Option(string name, bool hidden, BaseNode trigger, char identifier) {
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

    public BaseNode Node{
        get {return trigger;}
    }

    public override string ToString() {
        return $"{name}";
    }
}