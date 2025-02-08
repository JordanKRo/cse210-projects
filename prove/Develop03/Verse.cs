public class Verse{
    int verseNumber;
    string originalText;
    List<Word> words;

    public string GetDisplay(){
        string ret = $"{verseNumber} ";
        foreach(Word word in words){
            ret += word;
        }
        return ret;
    }
}