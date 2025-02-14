using System.Reflection.Metadata;

public class Reference{
    private string _book = "";
    private int _chapter = -1;
    private int _start = -1;
    private int _end = -1;

    public static readonly Dictionary<string, string> MyDict = new Dictionary<string, string>
    {
        {"1nephi", "1 Nephi"},
        {"2nephi", "2 Nephi"},
        {"orange", ""}
    };

    public Reference(string book, int chapter, int start, int end){
        _book = book;
        _chapter = chapter;
        _start = start;
        _end = end;
    }

    public Reference(string book, int chapter, int start){
        _book = book;
        _chapter = chapter;
        _start = start;
    }

    public override string ToString()
    {
        if(_end == -1){
            return $"{_book} {_chapter} : {_start}";
        }else{
            return $"{_book} {_chapter} : {_start}-{_end}";
        }
        
    }

    public int GetStart(){
        return _start;
    }

    public int GetEnd(){
        if (_end == -1){
            return _start;
        } else {
            return _end;
        }
    }

    public int GetChapter(){
        return _chapter;
    }

    public string GetBook(){
        return _book;
    }

    //
}