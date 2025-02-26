public class ReflectionPrompt{
    private string _text;
    private List<string> _followUps = new List<string>();

    private Random _randomSeed = new Random();

    public ReflectionPrompt(string text, List<string> followUps){
        _text = text;
        _followUps = followUps;
    }

    public ReflectionPrompt(string text){
        _text = text;
    }

    public string GetPrompt(){
        return _text;
    }

    public List<string> GetAllFollowUps(){
        return _followUps;
    }

    public string GetRandomFollowUp(){
        return _followUps[_randomSeed.Next(0, _followUps.Count - 1)];
    }

    public void SetText(string text){
        _text = text;
    }

    public void SetFollowUps(List<string> followUps){
        _followUps = followUps;
    }

    public void AddFollowUp(string followUp){
        _followUps.Add(followUp);
    }
}