using System.Text;

public class TextEvent : BaseNode
{
    protected string content;
    protected BaseNode? nextNode;
    const int MAX_WIDTH = 80;
    public TextEvent(string id, string content, BaseNode nextEvent, bool autoAdvance = false, bool displayProceedMessage = true, int sleepMils = 0, bool checkpoint = true) : base(id, sleepMils, autoAdvance, displayProceedMessage, checkpoint: checkpoint)
    {
        this.content = content;
        this.nextNode = nextEvent;
    }

    public TextEvent(string id, string content) : base(id)
    {
        this.content = content;
    }

    public override void OnExecute()
    {
        // split by words and wrap the text
        string[] words = content.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var lines = new List<string>();
        // string builder has less overhead on with creating a bunch of garbage instances
        var currentLine = new StringBuilder();

        foreach (var word in words){
            if (currentLine.Length + word.Length + 1 > MAX_WIDTH){
                lines.Add(currentLine.ToString());
                currentLine.Clear();
            }

            if (currentLine.Length > 0){
                currentLine.Append(' ');
            }
            currentLine.Append(word);
        }

        if (currentLine.Length > 0)
        {
            lines.Add(currentLine.ToString());
        }

        Console.WriteLine(string.Join(Environment.NewLine, lines));
    }

    public override BaseNode? GetNextEvent()
    {
        return nextNode;
    }

    public void SetNextNode(BaseNode node)
    {
        nextNode = node;
    }
}