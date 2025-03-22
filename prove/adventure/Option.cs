public class Option {
    protected string name;
    protected bool hidden;
    protected int desiredOrder;
    protected Event trigger;

    /// <summary>
    /// Create an option with a name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="hidden"></param>
    /// <param name="orderPriority"></param>
    public Option(string name, bool hidden, int orderPriority, Event trigger) {
        this.name = name;
        this.hidden = hidden;
        this.desiredOrder = orderPriority;
        this.trigger = trigger;
    }

    public string Name {
        get { return name; }
        set { name = value; }
    }

    public bool Hidden {
        get { return hidden; }
        set { hidden = value; }
    }

    public int OrderPriority {
        get { return desiredOrder; }
        set { desiredOrder = value; }
    }

    public override string ToString() {
        return $"{name}";
    }
}