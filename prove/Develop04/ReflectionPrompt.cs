public class ReflectionPrompt{
    private string _text;
    private List<string> _followUps = new List<string>();
    private int currentIndex = 0;

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

    public string GetFollowUp(){
        if (_followUps.Count == 0){
            return "";
        }
        var followUp = _followUps[currentIndex];
        currentIndex++;
        return followUp;
    }
    /// <returns>True if all of the followups have been cycled</returns>
    public bool IsDone(){
        return currentIndex >= _followUps.Count - 1;
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