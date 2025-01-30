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
        Console.WriteLine("=================================");
        Console.Write($"prompt: {_prompt}");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(_response);
    }

    public void GetResponseFromUser(){
        // prompts the user for the response
    }

    private void StampDate(){
        // set _date to the current date
    }
}