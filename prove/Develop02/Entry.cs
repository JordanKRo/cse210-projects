public class Entry{
    public string _prompt;
    public string _response;
    public string _date;

    public void Sign(){
        // uses GetResponseFromUser
        
        // stamps the date
        StampDate();
    }
    /*
    =================
    Prompt: prompt
    -----------------
    Response:
    user's text here

    =================
    */

    public void Display(){
        Console.WriteLine($"Date: {_date}\nprompt: {_prompt}");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(_response);
    }

    public void DisplayPrompt(){
        Console.WriteLine("---------------------------------");
        Console.WriteLine(_prompt);
        Console.WriteLine("---------------------------------");
    }

    public void GetResponseFromUser(){
        // prompts the user for the response
    }

    private void StampDate(){
        // set _date to the current date
        DateTime theCurrentTime = DateTime.Now;
        _date = theCurrentTime.ToShortDateString();
    }
}