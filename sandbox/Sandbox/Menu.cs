public class Menu{
    public List<string> _options;

    public int PromptOptions(){
        // display all of the options
        for(var i = 0;i < _options.Count;i++){
            Console.WriteLine($"{i + 1} - {_options[i]}");
        }
        int? ret = null;
        while (ret == null){
            // get input from the user
            Console.Write("Enter a number from the list above: ");
            string input = Console.ReadLine();
            // validate the input is a number and parse it
            if(int.TryParse(input, out int result)){
                ret = result;
            }else{
                Console.WriteLine("Enter a valid integer!");
            }
        }
        // returns the users selection starting from 0 (makes it easier for other layers to use)
        return ret - 1 ?? 1;
    }
}