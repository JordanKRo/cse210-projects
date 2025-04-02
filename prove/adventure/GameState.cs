using System.Xml.Linq;

public class GameState{
    private Dictionary<string, dynamic> gameVariables = new Dictionary<string, dynamic>();
    private BaseNode? currentNode;
    private string? filePath;

    public GameState(){

    }

    public GameState(string filePath){
        this.filePath = filePath;
    }

    private static GameState? currentGameState;

    public static void SetGameState(GameState state){
        currentGameState = state;
    }

    public void Set(string variable, dynamic value) {
        gameVariables[variable] = value;

        if(filePath != null){
            // save
        }
    }

    public dynamic Get(string variable, dynamic defaultValue){
        return gameVariables[variable] ?? defaultValue;
    }

    public void SetCurrentNode(BaseNode node){
        currentNode = node;
    }

    public BaseNode? GetCurrentNode(){
        return currentNode;
    }
    /// <summary>
    /// saves to the sates save file
    /// </summary>
    public void Save(){

    }

    public static GameState GetGameState(){
        if(currentGameState == null){
            currentGameState = new GameState();
        }
        return currentGameState;
    }
}