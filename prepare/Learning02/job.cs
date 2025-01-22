public class Job{
    public string _jobTitle;
    public string _company;
    public int _startYear;
    public int _endYear;

    public Job(string title, int start, int end, string company= "None"){
        _jobTitle = title;
        _company = company;
        _startYear = start;
        _endYear = end;
    }

    public void Display(){
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
}