public static class Program
{
    public static void Main(string[] args)
    {
        bool isDone = false;
        // Defining nodes reserves them in the registry preventing new ones to take their IDs. This needs to be done before the tree is loaded.
        // create and reserve the system nodes
        SystemNode disposeState = new SystemNode(
            "Dispose of state",
            () =>
            {
                GameState.GetGameState().Dispose();
            },
            null,
            false
        );

        SystemNode leaveGame = new SystemNode(
            "Leave Game",
            () => {
                isDone = true;
            },
            null,
            false
        );

        SystemNode quitGame = new SystemNode(
            "Quit Game",
            () => {
                isDone = true;
                Environment.Exit(0);
            },
            null,
            false
        );

        Chooser endMenu = new Chooser(
            "end ops",
            "Now What: ",
            new List<Option>
            {
                new Option(
                    "Delete Save and replay",
                    false,
                    disposeState,
                    'C'
                ),
                new Option(
                    "Leave",
                    false,
                    leaveGame,
                    'E'
                )
            },
            false
        );
        // Add quit to all choosers
        List<Option> globalOptions = new List<Option>{
            new Option("Quit", true, quitGame, 'Q')
        };

        Chooser.globalOptions = globalOptions;


        Console.Clear();
        // Create the game tree
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "demos", "demo.json");
        BaseNode mainTree = EventLoader.LoadFromFile(path);

        // need to load the state after the tree otherwise the state cannot find the entry point.
        GameState state = new GameState(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "demos", "demo_save.json"));
        GameState.SetGameState(state);

        while (!isDone){
            if (state.GetCurrentNode() != null)
            {
                Console.WriteLine("Returning to current save...\n");
                state.GetCurrentNode()!.Main();
            }
            else
            {
                mainTree.Main();
            }

            // Give options
            endMenu.Main();
        }

        

    }
}