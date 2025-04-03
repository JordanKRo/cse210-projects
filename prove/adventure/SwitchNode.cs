
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

        if (options.Count == 0){
            throw new ArgumentException("SwitchOptions must have at least 1 option and path");
        }
    }

    public override BaseNode? GetNextEvent()
    {
        var gameValue = GameState.GetGameState().Get(variable, options[0].desiredValue);
        foreach(SwitchOption option in options){
            if (option.Evaluate(gameValue)){
                return option.path;
            }
        }
        return options[0].path;
    }

    public override void OnExecute()
    {
        // do nothing
    }

    public struct SwitchOption{
        public dynamic desiredValue;
        public BaseNode path;

        public bool Evaluate(dynamic contender){
            return false
        }

        private enum Domain{
            NOT,
            EQUAL,
            GREATER,
            LESS,
        }
    }
}