public static class Program
{
    public static void Main(string[] args)
    {
        bool isDone = false;
        // Defining nodes reserves them in the registry preventing new ones to take their IDs. This needs to be done before the tree is loaded.
        // create and reserve the system nodes
        SystemNode disposeState = new SystemNode(
            "Dispose_of_state",
            () =>
            {
                GameState.GetGameState().Dispose();
            },
            null,
            false
        );

        SystemNode leaveGame = new SystemNode(
            "Leave_Game",
            () =>
            {
                isDone = true;
            },
            null,
            false
        );

        SystemNode quitGame = new SystemNode(
            "Quit_Game",
            () =>
            {
                isDone = true;
                Environment.Exit(0);
            },
            null,
            false
        );

        Chooser endMenu = new Chooser(
            "end_ops",
            "Now What? ",
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
                    'L'
                )
            },
            false
        );
        // Add quit to all choosers
        List<Option> globalOptions = new List<Option>{
            new Option("Quit", true, quitGame, 'Q')
        };

        Chooser.globalOptions = globalOptions;


        // Console.Clear();
        // Create the game tree
        BaseNode mainTree = ReadDemoFile();

        // need to load the state after the tree otherwise the state cannot find the entry point.
        GameState state = new GameState(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "demos", "demo_save.json"));
        GameState.SetGameState(state);

        while (!isDone)
        {
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

    public static BaseNode DemoTree()
    {
        TextEvent ending = new TextEvent(
            "ending",
            "Demo complete! You've seen all node types.",
            null
        );

        // System action before ending
        SystemNode systemAction = new SystemNode(
            "system_action",
            () => { Console.WriteLine("[System action executed]"); },
            ending
        );

        SwitchNode doorCheck = new SwitchNode(
            "door_check",
            new List<SwitchNode.SwitchOption>(),
            "hasKey",
            false
        );

        TextEvent success = new TextEvent("success", "Door unlocked!", systemAction);
        TextEvent failure = new TextEvent("failure", "Door locked! Find the key first.", null);

        // Add options to the switch
        doorCheck.SetOptions(new List<SwitchNode.SwitchOption>{
        new SwitchNode.SwitchOption(true, success),
        new SwitchNode.SwitchOption(false, failure)
    });

        // Main choice
        Chooser mainChoice = new Chooser(
            "main_choice",
            "What will you do?",
            new List<Option>{
            new Option("Find key", false, new WriteNode("get_key", "hasKey", true, doorCheck)),
            new Option("Try door", false, doorCheck)
            }
        );

        // Set failure to loop back to main choice
        failure.SetNextNode(mainChoice);

        // Decorated intro leading to main choice
        DecoratedTextEvent intro = new DecoratedTextEvent(
            "intro",
            "DEMO\nAll node types",
            mainChoice
        );

        return intro;
    }

    public static BaseNode ReadDemoFile()
    {
        // This sample was made with the help of AI (my project is the engine)
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "demos", "demo.json");
        BaseNode mainTree = EventLoader.LoadFromFile(path);
        return mainTree;
    }

    public static BaseNode ReadTestFile()
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "demos", "test.json");
        BaseNode mainTree = EventLoader.LoadFromFile(path);
        return mainTree;
    }
}