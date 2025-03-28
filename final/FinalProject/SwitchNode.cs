
/// <summary>
/// Used to create branches using in game variables.
/// </summary>
public class SwitchNode : BaseNode
{
    private List<SwitchOption> options;
    private string variable;
    private dynamic defaultValue;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="options">The first entry is the default value</param>
    /// <param name="variable"></param>
    /// <param name="defaultValue"></param>
    public SwitchNode(string id, List<SwitchOption> options, string variable, dynamic defaultValue) : base(id, 0, true, false, false)
    {
        this.options = options;
        this.variable = variable;
        this.defaultValue = defaultValue;
    }

    public override BaseNode? GetNextEvent()
    {
        var gameValue = GameState.GetGameState().Get(variable, defaultValue);
        foreach(SwitchOption option in options){
            if (option.Evaluate(gameValue)){
                return option.Path;
            }
        }
        return options[0].Path;
    }

    public override void OnExecute()
    {
        // do nothing
    }

    public void SetOptions(List<SwitchOption> options){
        this.options = options;
    }

    public class SwitchOption
    {
        public dynamic DesiredValue { get; private set; }
        public BaseNode Path { get; private set; }
        public Domain ComparisonDomain { get; private set; }

        public SwitchOption(dynamic desiredValue, BaseNode path, Domain domain = Domain.EQUAL)
        {
            DesiredValue = desiredValue;
            Path = path;
            ComparisonDomain = domain;
        }

        public bool Evaluate(dynamic contender)
        {
            // First check if types are comparable
            if (!CanCompare(DesiredValue, contender))
            {
                return false;
            }

            switch (ComparisonDomain)
            {
                case Domain.NOT:
                    return !Equals(DesiredValue, contender);
                case Domain.EQUAL:
                    return Equals(DesiredValue, contender);
                case Domain.GREATER:
                    return TryCompare(contender, DesiredValue) < 0;
                case Domain.LESS:
                    return TryCompare(contender, DesiredValue) > 0;
                default:
                    return false;
            }
        }

        private bool CanCompare(dynamic value1, dynamic value2)
        {
            try
            {
                // Check if basic equality can be determined
                var result = Equals(value1, value2);
                return true;
            }
            catch
            {
                // If equality check throws an exception, they can't be compared
                return false;
            }
        }

        private int? TryCompare(dynamic value1, dynamic value2)
        {
            try
            {
                // Try to use IComparable if available
                if (value1 is IComparable comparable1)
                {
                    return comparable1.CompareTo(value2);
                }
                else if (value2 is IComparable comparable2)
                {
                    return -comparable2.CompareTo(value1);
                }
                
                // If neither implements IComparable, they can't be compared for ordering
                return null;
            }
            catch
            {
                // If comparison throws an exception, they can't be compared for ordering
                return null;
            }
        }

        public enum Domain
        {
            NOT,
            EQUAL,
            GREATER,
            LESS,
        }
    }
}