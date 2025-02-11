public class Verse{
    int _verseNumber;
    List<Word> _words = new List<Word>();

    public Verse(){
        
    }

    public Verse(List<Word> words, int verseNumber){
        _words = words;
        _verseNumber = verseNumber;
    }

    public Verse(string originalText, int verseNumber){
        _verseNumber = verseNumber;

        string[] tokens = originalText.Split(' ');
        foreach(string token in tokens){
            _words.Add(new Word(token));
        }
    }
    

    public string GetDisplay(){
        string ret = $"{_verseNumber} ";
        foreach(Word word in _words){
            ret += word;
        }
        return ret;
    }
}