public class DecoratedTextEvent : TextEvent
{
    public DecoratedTextEvent(string id, string content, BaseNode nextEvent, bool autoAdvance = false, bool displayProceedMessage = true, int sleepMils = 0, bool checkpoint = true) : base(id, content, nextEvent, autoAdvance, displayProceedMessage, sleepMils, checkpoint: checkpoint)
    {

    }

    public override void OnExecute()
    {
        var width = Console.WindowWidth;
        List<string> lines = new List<string>(content.Split('\n'));
        Console.WriteLine(new string('=', width));
        foreach (var line in lines)
        {
            var margin = (width - line.Length) / 2;
            if (margin < 0){
                margin = 0;
            }
            Console.WriteLine(new string(' ', margin) + line);
        }
        Console.WriteLine(new string('=', width));
    }
}