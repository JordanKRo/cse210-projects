public class ReflectionPrompt{
    private string _text;
    private List<string> _followUps = new List<string>();
    private Random _randomSeed = new Random();
    private List<string> _hat = new List<string>();

    public ReflectionPrompt(string text, List<string> followUps){
        _text = text;
        _followUps = followUps;

        _hat = new List<string>(_followUps);
    }

    public ReflectionPrompt(string text) {
        _text = text;
    }

    public string GetPrompt() {
        return _text;
        
    }

    public List<string> GetAllFollowUps() {
        return _followUps;
    }

    public string GetFollowUp() {
        // if the hat is empty reset it
        if (_hat.Count == 0) {
            _hat = new List<string>(_followUps);
        }
        var followUp = _hat[_randomSeed.Next(0, _hat.Count)];
        _hat.Remove(followUp);
        return followUp;
    }

    public void SetText(string text) {
        _text = text;
    }

    public void SetFollowUps(List<string> followUps) {
        _followUps = followUps;
        _hat = new List<string>(_followUps);
    }

    public void AddFollowUp(string followUp) {
        _followUps.Add(followUp);
        _hat.Add(followUp);
    }
}