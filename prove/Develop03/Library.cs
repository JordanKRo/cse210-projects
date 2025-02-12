public class Library{
    List<Scripture> _scriptures;
    private bool _enabledApi = false;
    public Library(){

    }

    public Library(bool useApi){
        _enabledApi = useApi;
    }
    /// <summary>
    /// Returns a scripture from the api or from the local volume.
    /// </summary>
    /// <returns></returns>
    public Scripture GetScripture(){
        throw new NotImplementedException();
    }
    // I intend this to be for adding verses to the library
    public void AddToLibraryFromApi(Reference reference){

    }
}