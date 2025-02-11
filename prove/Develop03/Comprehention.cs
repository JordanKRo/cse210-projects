public class Comprehension{
    private Scripture _scripture;

    private Comprehension(){
        // This is now allowed
    }
    public Comprehension(Scripture scripture){
        _scripture = scripture;
    }

    public void Start(){
        while(true){
            Console.Clear();

            Console.WriteLine(_scripture.GetDisplay());

            Console.ReadLine();

            if(_scripture.GetVisibleWordCount() == 0){
                break;
            }
            _scripture.HideWords(3);
        }
    }
    
}