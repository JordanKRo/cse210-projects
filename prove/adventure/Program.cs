public static class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();
        // Create the game tree
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..","..","..", "demos", "demo.json");
        BaseNode mainTree = EventLoader.LoadFromFile(path);
        mainTree.Main();
    }
    
    private static BaseNode CreateDemoTree()
    {
        // We need to build the game tree from bottom to top, since events need
        // their next events passed in the constructor

        // === ENDINGS (LEAF NODES) ===
        DecoratedTextEvent gameOver = new DecoratedTextEvent("game over", "Game Over\nother line?", null, autoAdvance: true, false, 200);
        
        // Crystal path endings
        TextEvent crystalEscape = new TextEvent(
            "crystal_escape",
            "Using the light of the crystal, you navigate through the collapsing ruins and make it outside just in time.\n" +
            "As you catch your breath, you notice the crystal has changed color. You've discovered a powerful artifact that will make you famous!",
            gameOver,
            autoAdvance: true,
            true,
            1000
        );
        
        TextEvent findMechanism = new TextEvent(
            "find_mechanism",
            "You search the room and find a slot in the wall that matches the crystal's shape. This must be the mechanism shown in the carving.\n" +
            "Curious, you return to the pedestal, carefully take the crystal, and place it in the slot. The wall slides open, revealing a hidden library of ancient knowledge!",
            gameOver,
            autoAdvance: true,
            true,
            1000
        );
        
        // Pool path endings
        TextEvent recoveredMemory = new TextEvent(
            "recovered_memory",
            "With your memory restored, you realize the ruins contain dangerous knowledge that was sealed away for a reason.\n" +
            "You decide to leave and report your findings to the university, ensuring these ruins are protected from those who might misuse their power.",
            gameOver,
            autoAdvance: true,
            true,
            1000
        );
        
        TextEvent treasureHunter = new TextEvent(
            "treasure_hunter",
            "The treasure chamber contains artifacts beyond your wildest dreams. Gold, jewels, and priceless historical items.\n" +
            "You gather what you can carry and find a hidden exit that leads back to the jungle. Your life will never be the same!",
            gameOver,
            autoAdvance: true,
            true,
            1000
        );
        
        // Chest path endings
        TextEvent followJournal = new TextEvent(
            "follow_journal",
            "The journal contains detailed notes about a hidden temple deeper in the ruins, where Professor Hamilton discovered something extraordinary.\n" +
            "The rusty key must open the way. With the journal as your guide, you set out to find your mentor and uncover what he discovered.",
            gameOver,
            autoAdvance: true,
            true,
            1000
        );
        
        TextEvent secretStudy = new TextEvent(
            "secret_study",
            "The study belongs to Professor Hamilton. His notes reveal he found evidence of an advanced ancient civilization with technology beyond our understanding.\n" +
            "Among his papers, you find a map marking the location of other ruins around the world. This could be the archaeological discovery of the century!",
            gameOver,
            autoAdvance: true,
            true,
            1000
        );
        
        // Outside path endings
        TextEvent rescue = new TextEvent(
            "rescue",
            "The locals tell you that you came to explore the ruins but never returned to the village. They've been searching for days.\n" +
            "They take you back to their village where you recover from your ordeal. You promise to return someday to continue your exploration with proper preparations.",
            gameOver,
            autoAdvance: true,
            true,
            1000
        );
        
        TextEvent reunion = new TextEvent(
            "reunion",
            "Professor Hamilton explains that the ruins contain powerful ancient technology that someone is trying to exploit.\n" +
            "He's been hiding since discovering the truth. Together, you make a plan to protect the ruins and expose the conspiracy.",
            gameOver,
            autoAdvance: true,
            true,
            1000
        );
        
        // === PRE-ENDING CHOICES ===
        
        // Crystal room choices
        TextEvent takeCrystal = new TextEvent(
            "take_crystal",
            "You reach out and take the crystal. It feels warm in your hand and pulses with energy.\n" +
            "Suddenly, the ground begins to shake. The ruins are collapsing! You sprint back the way you came.",
            crystalEscape,
            false,
            true,
            1000
        );
        
        TextEvent leaveCrystal = new TextEvent(
            "leave_crystal",
            "You decide not to touch the crystal. As you examine the wall carvings more closely, you notice they tell a story\n" +
            "of an ancient civilization that harnessed the power of magical crystals. One carving shows the crystal being placed in a mechanism.",
            findMechanism,
            false,
            true,
            1000
        );
        
        List<Option> crystalOptions = new List<Option>
        {
            new Option("Take the crystal", false, takeCrystal),
            new Option("Leave the crystal alone", false, leaveCrystal)
        };
        
        Chooser crystalChoice = new Chooser(
            "crystal_choice",
            "What do you do with the crystal?",
            crystalOptions
        );
        
        // Pool choices
        TextEvent drinkWater = new TextEvent(
            "drink_water",
            "You cup your hands and drink some of the glowing water. It tastes sweet and refreshing.\n" +
            "A strange energy courses through your body. You feel rejuvenated and your memories begin to return!\n" +
            "You remember you are an archaeologist who came to study these ruins, but something went wrong...",
            recoveredMemory,
            false,
            true,
            1000
        );
        
        TextEvent wadeIntoPool = new TextEvent(
            "wade_into_pool",
            "You wade into the pool. The water is warm and seems to embrace you. As you go deeper, you notice a passage beneath the surface.\n" +
            "Taking a deep breath, you dive under and swim through the passage. You emerge in a hidden chamber filled with treasure!",
            treasureHunter,
            false,
            true,
            1000
        );
        
        List<Option> poolOptions = new List<Option>
        {
            new Option("Drink from the pool", false, drinkWater),
            new Option("Wade into the pool", false, wadeIntoPool)
        };
        
        Chooser poolChoice = new Chooser(
            "pool_choice",
            "What do you do at the pool?",
            poolOptions
        );
        
        // Chest choices
        TextEvent openChest = new TextEvent(
            "open_chest",
            "You approach the chest and carefully lift the lid. Inside, you find an old journal and a rusty key.\n" +
            "The journal belongs to Professor Hamilton, your mentor who disappeared six months ago while exploring these ruins.",
            followJournal,
            false,
            true,
            1000
        );
        
        TextEvent leaveChest = new TextEvent(
            "leave_chest",
            "You decide not to open the chest. As you turn to leave, you notice a small passage hidden behind a loose stone in the wall.\n" +
            "You squeeze through the passage and find yourself in a secret study with maps and research notes scattered everywhere.",
            secretStudy,
            false,
            true,
            1000
        );
        
        List<Option> chestOptions = new List<Option>
        {
            new Option("Open the chest", false, openChest),
            new Option("Leave the chest alone", false, leaveChest)
        };
        
        Chooser chestChoice = new Chooser(
            "chest_choice",
            "What do you do with the chest?",
            chestOptions
        );
        
        // Outside choices
        TextEvent investigateNoises = new TextEvent(
            "investigate_noises",
            "You gather your courage and venture into the jungle to investigate the noises.\n" +
            "After walking a short distance, you discover a group of locals who have been searching for you since you disappeared three days ago.",
            rescue,
            false,
            true,
            1000
        );
        
        TextEvent stayByFire = new TextEvent(
            "stay_by_fire",
            "You decide to stay by the fire. Throughout the night, the strange noises grow closer.\n" +
            "Just before dawn, a figure emerges from the darkness - it's Professor Hamilton! He's alive, but looks haggard and afraid.",
            reunion,
            false,
            true,
            1000
        );
        
        List<Option> outsideOptions = new List<Option>
        {
            new Option("Investigate the noises", false, investigateNoises),
            new Option("Stay by the fire", false, stayByFire)
        };
        
        Chooser outsideChoice = new Chooser(
            "outside_choice",
            "What do you do about the strange noises?",
            outsideOptions
        );
        
        // === CORRIDOR EXPLORATION ===
        
        TextEvent leftCorridor = new TextEvent(
            "left_corridor",
            "You follow the left corridor. After walking for a few minutes, you find yourself in a large chamber with ancient carvings on the walls.\n" +
            "In the center of the room is a pedestal with a glowing blue crystal.",
            crystalChoice,
            false,
            true,
            1000
        );
        
        TextEvent centerCorridor = new TextEvent(
            "center_corridor",
            "The center corridor leads downward. As you descend, the air becomes more humid.\n" +
            "You eventually reach an underground pool of water. The water glows with an eerie blue light.",
            poolChoice,
            false,
            true,
            1000
        );
        
        TextEvent rightCorridor = new TextEvent(
            "right_corridor",
            "The right corridor is narrow and winding. Spider webs brush against your face as you walk.\n" +
            "Eventually, you emerge into a small room with a wooden chest in the corner.",
            chestChoice,
            false,
            true,
            1000
        );
        
        // === INITIAL CHOICES ===
        
        // Corridor choice after entering ruins
        List<Option> corridorOptions = new List<Option>
        {
            new Option("Take the left corridor", false, leftCorridor),
            new Option("Take the center corridor", false, centerCorridor),
            new Option("Take the right corridor", false, rightCorridor)
        };
        
        Chooser corridorChoice = new Chooser(
            "corridor_choice",
            "Which corridor do you choose?",
            corridorOptions
        );
        
        TextEvent enterRuins = new TextEvent(
            "enter_ruins",
            "You squeeze through the gap and enter the ruins. The air is stale and cool inside.\n" +
            "Your footsteps echo in the darkness. As your eyes adjust, you see three corridors ahead.",
            corridorChoice,
            false,
            true,
            1000
        );
        
        TextEvent stayOutside = new TextEvent(
            "stay_outside",
            "You decide to stay outside and set up a small camp. As night falls, you hear strange noises from the jungle around you.\n" +
            "You manage to start a small fire with some sticks and leaves.",
            outsideChoice,
            false,
            true,
            1000
        );
        
        // First choice - enter ruins or stay outside
        List<Option> firstChoiceOptions = new List<Option>
        {
            new Option("Enter the ruins", false, enterRuins),
            new Option("Stay outside and make camp", false, stayOutside)
        };
        
        Chooser firstChoice = new Chooser(
            "first_choice",
            "What do you want to do?",
            firstChoiceOptions
        );
        
        // Game introduction (root node)
        TextEvent intro = new TextEvent(
            "intro",
            "You wake up at the entrance of an ancient ruin. Your head hurts and you can't remember how you got here.\n" +
            "The sun is setting and you need to find shelter soon. The entrance to the ruins is partially open.",
            firstChoice,
            false,
            true,
            1000
        );

        DecoratedTextEvent title = new DecoratedTextEvent("title", "THE FORGOTTEN RUINS\nA Text Adventure Demo", intro, true, false, 200);
        
        return title;
    }
}