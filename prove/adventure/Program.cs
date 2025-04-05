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
            () =>
            {
                isDone = true;
            },
            null,
            false
        );

        SystemNode quitGame = new SystemNode(
            "Quit Game",
            () =>
            {
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
        BaseNode mainTree = ReadLoopDemo();

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
        // Create introduction nodes
        TextEvent intro = new TextEvent(
            "intro",
            "Welcome to the Text Adventure Demo!\nYou find yourself in an ancient ruin with multiple pathways ahead.",
            null
        );

        // Create a decorated text event for the main hall description
        DecoratedTextEvent mainHall = new DecoratedTextEvent(
            "main_hall",
            "THE MAIN HALL\nAncient pillars line the walls of this vast chamber.\nThree passages lead away from here, and there's a locked door to the north.",
            null
        );

        // Create a chooser for the main paths
        Chooser mainPathChoice = new Chooser(
            "main_path_choice",
            "Which way will you go?",
            new List<Option>()
        );

        // Initial write node to set hasKey variable to false
        WriteNode initializeInventory = new WriteNode(
            "initialize_inventory",
            "hasKey",
            false,
            null
        );

        // Path descriptions
        TextEvent westPassage = new TextEvent(
            "west_passage",
            "You follow the west passage. The corridor is dark and damp, with moss growing on the walls.",
            null
        );

        TextEvent eastPassage = new TextEvent(
            "east_passage",
            "You follow the east passage. It slopes gently downward, and you feel a cool breeze.",
            null
        );

        TextEvent southPassage = new TextEvent(
            "south_passage",
            "You follow the south passage. It's well-lit with ancient torches that still burn mysteriously.",
            null
        );

        // West passage branch - has a dead end
        TextEvent westRoom = new TextEvent(
            "west_room",
            "The passage opens into a small chamber. There's nothing of interest here except some ancient pottery shards.",
            null
        );

        // East passage branch - has the key
        TextEvent eastRoom = new TextEvent(
            "east_room",
            "The passage leads to a treasure room. Among various worthless trinkets, you spot a golden key on a pedestal!",
            null
        );

        // WriteNode to set the hasKey variable to true when the player finds the key
        WriteNode obtainKey = new WriteNode(
            "obtain_key",
            "hasKey",
            true,
            null
        );

        TextEvent keyObtained = new TextEvent(
            "key_obtained",
            "You take the golden key. It might open that locked door back in the main hall.",
            null
        );

        // South passage branch - just another room
        TextEvent southRoom = new TextEvent(
            "south_room",
            "The passage leads to a room with a large statue. It doesn't seem to be hiding anything of value.",
            null
        );

        // Return to hall options from each room
        Chooser westRoomChoice = new Chooser(
            "west_room_choice",
            "What will you do?",
            new List<Option>()
        );

        Chooser eastRoomChoice = new Chooser(
            "east_room_choice",
            "What will you do?",
            new List<Option>()
        );

        Chooser southRoomChoice = new Chooser(
            "south_room_choice",
            "What will you do?",
            new List<Option>()
        );

        // Try to open the door
        TextEvent tryDoor = new TextEvent(
            "try_door",
            "You approach the locked door in the main hall.",
            null
        );

        // Switch node to check if the player has the key
        TextEvent doorLocked = new TextEvent(
            "door_locked",
            "You try the door, but it's firmly locked. You'll need to find a key to open it.",
            null
        );

        TextEvent doorUnlocked = new TextEvent(
            "door_unlocked",
            "You insert the golden key into the lock. It turns smoothly, and the door swings open!",
            null
        );

        // Create switch node for the door
        SwitchNode doorCheck = new SwitchNode(
            "door_check",
            new List<SwitchNode.SwitchOption>{
            new SwitchNode.SwitchOption(false, doorLocked, SwitchNode.SwitchOption.Domain.EQUAL),
            new SwitchNode.SwitchOption(true, doorUnlocked, SwitchNode.SwitchOption.Domain.EQUAL)
            },
            "hasKey",
            false
        );

        // Final room
        TextEvent treasureRoom = new TextEvent(
            "treasure_room",
            "Beyond the door, you discover a magnificent treasure chamber filled with gold and jewels! Congratulations!",
            null
        );

        // SystemNode to demonstrate system functionality
        SystemNode saveGame = new SystemNode(
            "save_game",
            () =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[Game progress saved automatically via checkpoint]");
                Console.ResetColor();
            },
            null
        );

        // Final message
        TextEvent ending = new TextEvent(
            "ending",
            "Thank you for playing the Text Adventure Demo! You've seen how variables can be used to track inventory items and create puzzles.",
            null
        );

        // Connect the nodes
        intro.SetNextNode(initializeInventory);
        initializeInventory.SetNextNode(mainHall);
        mainHall.SetNextNode(mainPathChoice);

        // Set main hall choices
        mainPathChoice.SetOptions(new List<Option>{
        new Option("Take the west passage", false, westPassage),
        new Option("Take the east passage", false, eastPassage),
        new Option("Take the south passage", false, southPassage),
        new Option("Try the locked door", false, tryDoor)
    });

        // Connect west path
        westPassage.SetNextNode(westRoom);
        westRoom.SetNextNode(westRoomChoice);
        westRoomChoice.SetOptions(new List<Option>{
        new Option("Return to the main hall", false, mainHall)
    });

        // Connect east path with key
        eastPassage.SetNextNode(eastRoom);
        eastRoom.SetNextNode(eastRoomChoice);
        eastRoomChoice.SetOptions(new List<Option>{
        new Option("Take the golden key", false, obtainKey),
        new Option("Return to the main hall", false, mainHall)
    });
        obtainKey.SetNextNode(keyObtained);
        keyObtained.SetNextNode(mainHall);

        // Connect south path
        southPassage.SetNextNode(southRoom);
        southRoom.SetNextNode(southRoomChoice);
        southRoomChoice.SetOptions(new List<Option>{
        new Option("Return to the main hall", false, mainHall)
    });

        // Connect door path
        tryDoor.SetNextNode(doorCheck);
        doorLocked.SetNextNode(mainPathChoice);
        doorUnlocked.SetNextNode(treasureRoom);
        treasureRoom.SetNextNode(saveGame);
        saveGame.SetNextNode(ending);

        // Return the starting node
        return intro;
    }

    public static BaseNode ReadDemoFile()
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "demos", "demo.json");
        BaseNode mainTree = EventLoader.LoadFromFile(path);
        return mainTree;
    }

    public static BaseNode ReadSpaceGameFile()
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "demos", "space_game.json");
        BaseNode mainTree = EventLoader.LoadFromFile(path);
        return mainTree;
    }

    public static BaseNode ReadLoopDemo(){
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "demos", "loop.json");
        BaseNode mainTree = EventLoader.LoadFromFile(path);
        return mainTree;
    }
}