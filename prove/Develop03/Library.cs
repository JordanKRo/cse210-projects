public class Library{
    private const string requestUrl = "";
    List<Scripture> _scriptures;
    public Library(){
        // _scriptures.Add(new Scripture());
    }

    public void Load(){

    }

    public Scripture GetScriptureFromApi(Reference reference){
        throw new Exception("Not implemented");
    }
}