public class Scripture{
    private Reference _reference;
    private List<Verse> _verses = new List<Verse>();

    private Random _randomSeed = new Random();

    public Scripture(List<string> originalTexts, Reference reference){
        _reference = reference;

        for(int i = 0;i < originalTexts.Count;i++){
            int verseNumber = i + reference.GetStart();

            _verses.Add(new Verse(originalTexts[i], verseNumber));
        }
        
    }

    public string GetDisplay(){
        string ret = _reference + "\n";
        foreach(Verse verse in _verses){
            ret += verse.GetDisplay() + "\n\n";
        }

        return ret;
    }


    public void HideWords(int count){
        
        for(int i = 0;i < count;i++){
            List<Verse> visibleVerses = GetVersesStillVisible();

            if (visibleVerses.Count == 0){
                return;
            }

            int next = _randomSeed.Next(visibleVerses.Count);

            visibleVerses[next].HideWords(1);
        }
    }

    public int GetVisibleWordCount(){
        int ret = 0;
        
        foreach(Verse verse in _verses){
            ret += verse.GetNumberVisible();
        }

        return ret;
    }

    private List<Verse> GetVersesStillVisible(){
        List<Verse> ret = new List<Verse>();

        foreach(Verse verse in _verses){
            if(verse.GetNumberVisible() > 0){
                ret.Add(verse);
            }
        }
        return ret;
    }
}