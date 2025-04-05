using System.Net.WebSockets;
using System.Text.Json;
using System.Xml.Schema;

public class WriteNode : BaseNode
{
    private string variable;
    private dynamic value;
    private BaseNode nextEvent;
    public WriteNode(string id, string variable, dynamic value, BaseNode nextEvent) : base(id, 0, true, false, false)
    {
        this.variable = variable;
        this.value = value;
        this.nextEvent = nextEvent;
    }

    public override BaseNode? GetNextEvent()
    {
        return nextEvent;
    }

    public override void OnExecute()
    {
        if (value is string stringValue)
        {
            if(stringValue.Length >= 2 && stringValue.Substring(0,1) == "$"){
                var rest = stringValue.Substring(1, stringValue.Length - 1);
                var currentValue = (JsonElement)GameState.GetGameState().Get(variable, 0);
                if(int.TryParse(rest, out int num) && currentValue.ValueKind == JsonValueKind.Number){
                    // Using the incremental format
                    GameState.GetGameState().Set(variable, currentValue.GetInt32());
                    return;
                }else if(currentValue is JsonElement currentString){
                    // Concat string
                    GameState.GetGameState().Set(variable, currentString.ToString() + rest);
                    return;
                }

                

            }
        }
        GameState.GetGameState().Set(variable, value);
        
    }

    public void SetNextNode(BaseNode nextNode){
        nextEvent = nextNode;
    }


}