This is an adventure game engine. To run the demo make sure the ReadDemoFile() method is called in Program and used to load the root of the tree.
Press run to play (some functionality may not work or might break when running in debug).

You might notice that if you run it again it takes you back to where you left off. 
The easiest way to restart is to delete the contents of the demo_save.json file. Or by choosing the "Delete Save and replay" when the game is over.
if you would like you can try making your own game file and load it. Make sure the file is formatted properly.
You can patter you nodes as they are below:

{
  "Nodes": [
    {
        "Id": "unique_node_id",
        "Type": "TextEvent",
        "Content": "Your text content goes here",
        "NextId": "next_node_id",
        "SleepMils": 0,
        "AutoAdvance": false,
        "DisplayProceedMessage": true,
        "checkpoint": true
    }
  ]
}

"TextEvent" means the node displays it's content and then advances to the next. There is also a decorated variety that just displays
centred content with bars on the top and bottom.



{
  "Id": "main_path_choice",
  "Type": "Chooser",
  "Content": "Which way will you go?",
  "Options": [
    {
      "Name": "Take the west passage",
      "NextId": "west_passage",
      "Hidden": false,
      "Identifier": "W"
    },
    {
      "Name": "Take the east passage",
      "NextId": "east_passage",
      "Hidden": false
    }
  ],
  "checkpoint": true
}

"Choosers" are what prompt the user with options. Notice the "Identifier" field is optional if it is not specified the option will be numbered.
This is also what the user types to choose the option
The hidden field is also optional, if an option is hidden it will not be displayed but can still be entered from the user.
This is helpful if you have options that you don't want to clutter the screen like the Q - for quit which is part of the global options.

Switchers are similar to choosers except they read the GameState variables. The WriteNode can write to the state and the switchers can
read it to create a smarter and more immersive experience. This can be used for keeping track of inventory or which NPCs you have spoken
to just to name a couple. Please refer to the demo file for how they are used.

PS: SwitchOptions can have 4 types of domains EQUAL, NOT, LESS, GREATER.

PPS: WriteNodes can also increment integers and concatenate strings. use syntax "$n" increment by n "$-1" decrement by -1.
"$abc" to add "abc" to the end of the existing variable (converts variables to strings if they are not already).