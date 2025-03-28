public class DecoratedTextEvent : TextEvent
{
    public DecoratedTextEvent(string id, string content, BaseNode nextEvent, bool autoAdvance = false, bool displayProceedMessage = true, int sleepMils = 0) : base(id, content, nextEvent, autoAdvance, displayProceedMessage, sleepMils)
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
            Console.WriteLine(new string(' ', margin) + line);
        }
        Console.WriteLine(new string('=', width));
    }
}