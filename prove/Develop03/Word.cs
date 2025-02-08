public class Word{
    string _theWord;
    bool _visible = true;

    public override string ToString()
    {
        if (_visible){
            return _theWord;
        } else {
            return new string('_', _theWord.Length);
        }
        
    }

    public void SetVisibility(bool visible){
        _visible = visible;
    }

    public bool IsVisible(){
        return _visible;
    }
}