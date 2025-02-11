public class Verse{
    int _verseNumber = -1;
    List<Word> _words = new List<Word>();

    Random randomSeed = new Random();

    public Verse(){
        
    }

    public Verse(List<Word> words, int verseNumber){
        _words = words;
        _verseNumber = verseNumber;
    }

    public Verse(string originalText){
        string[] tokens = originalText.Split(' ');
        foreach(string token in tokens){
            _words.Add(new Word(token));
        }
    }

    public Verse(string originalText, int verseNumber){
        _verseNumber = verseNumber;

        string[] tokens = originalText.Split(' ');
        foreach(string token in tokens){
            _words.Add(new Word(token));
        }
    }

    public void HideWords(int count){
        for(int i = 0;i < count;i++){
            List<Word> visibleWords = GetVisibleWords();

            if(visibleWords.Count == 0){
                return;
            }

            int chosen = randomSeed.Next(visibleWords.Count);

            visibleWords[chosen].SetVisibility(false);
        }


    }

    public string GetDisplay(){
        string ret = $"{_verseNumber} : ";

        foreach(Word word in _words){
            ret += word + " ";
        }
        return ret;
    }

    public int GetNumberVisible(){
        int ret = 0;
        foreach(Word word in _words){
            if(word.IsVisible()){
                ret++;
            }
        }

        return ret;
    }

    private List<Word> GetVisibleWords(){
        List<Word> ret = new List<Word>();
        foreach(Word word in _words){
            if(word.IsVisible()){
                ret.Add(word);
            }
        }

        return ret;
    }
}