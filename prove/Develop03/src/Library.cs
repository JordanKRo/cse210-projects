using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class Library{
    List<Scripture> _scriptures;
    private bool _enabledApi = false;

    ScriptureClient client;

    Random _randomSeed = new Random();
    public Library(){

    }

    public Library(string filePath){
        _scriptures = LoadScripturesFile(filePath);
    }

    public Library(bool useApi = false) {
        _enabledApi = useApi;

        if (_enabledApi) {
            client = new ScriptureClient();
        }
    }

    /// <summary>
    /// Returns a scripture from the api or from the local volume.
    /// </summary>
    /// <returns></returns>
    public Scripture GetScripture() {
        return _scriptures[_randomSeed.Next(_scriptures.Count)];
    }
    // I intend this to be for adding verses to the library
    public void AddToLibraryFromApi(Reference reference){

    }

    public List<Scripture> GetAllScriptures(){
        return _scriptures;
    }

    public async void CacheAvailableVolume(){
        await client.GetBooks();
    }

    public List<Scripture> LoadScripturesFile(string path){

        List<Scripture> ret = new List<Scripture>();
        try{
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                JsonDocument document = JsonSerializer.Deserialize<JsonDocument>(json);

                JsonElement scripturesElement = document.RootElement.GetProperty("Scriptures");
                
                
                
                for(int i = 0;i < scripturesElement.GetArrayLength();i++){
                    JsonElement r = scripturesElement[i].GetProperty("reference");
                    JsonElement v = scripturesElement[i].GetProperty("verses");
                    // scripturesElement[i]
                    int end = -1;
                    if(r.TryGetProperty("end", out JsonElement e)){
                        end = e.GetInt16();
                    }
                    
                    Reference reference = new Reference(r.GetProperty("book").GetString(), r.GetProperty("chapter").GetInt16(), r.GetProperty("start").GetInt16(), end);
                    List<string> verses = new List<string>();
                    
                    
                    for(int j = 0;j < v.GetArrayLength();j++){
                        verses.Add(v[j].GetString());
                    }
                    ret.Add(new Scripture(verses, reference));
                    
                }


                return ret;
            }
        }catch(IOException ioex){
            Console.WriteLine("Failed to load the library file, an IO exception occurred.\n" + ioex);
            return [];
        }catch(JsonException jsonex){
            Console.WriteLine("Failed to load the library file, a JSON exception occurred.\n" + jsonex);
            return [];
        }
        
    }
}