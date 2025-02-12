using System.Reflection.Metadata;
using System.Text.Json;

public class ScriptureClient{

    private const string baseRequestUrl = "https://openscriptureapi.org/api/scriptures/v1/lds/en/volume/bookofmormon";
    private const string bomUrl = "https://openscriptureapi.org/api/scriptures/v1/lds/en/volume/bookofmormon";
    private Dictionary<string,string> _booksCache = new Dictionary<string, string>();

    private readonly HttpClient client = new HttpClient();
    
    public ScriptureClient(){
        
    }

    public async Task<Scripture> GetScriptureFromApi(Reference reference){
        string bookId = "";
        int verseStart = reference.GetStart();
        int chapter = reference.GetChapter();

        if (_booksCache.TryGetValue(reference.GetBook(), out string result)){
            bookId = result;
        }else{
            return null;
        }

        List<string> verseText = new List<string>();
        
        try
        {
            HttpResponseMessage response = await client.GetAsync($"{bomUrl}/{bookId}/{chapter}");
            response.EnsureSuccessStatusCode();
            string plainJson = await response.Content.ReadAsStringAsync();
            JsonDocument document = JsonSerializer.Deserialize<JsonDocument>(plainJson);
            
            JsonElement verses = document.RootElement.GetProperty("chapter").GetProperty("verses");

            for(int i = verseStart - 1;i < reference.GetEnd();i++){
                JsonElement element = verses[i];
                verseText.Add(element.GetProperty("text").ToString());
            }

            return new Scripture(verseText, reference);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error making GET request: {e.Message}");
            throw;
        }
    }

    public async Task GetBooks(){
        try
        {
            HttpResponseMessage response = await client.GetAsync(baseRequestUrl);
            response.EnsureSuccessStatusCode();
            string plainJson = await response.Content.ReadAsStringAsync();

            JsonDocument document = JsonSerializer.Deserialize<JsonDocument>(plainJson);
            JsonElement books = document.RootElement.GetProperty("books");
            foreach(JsonElement element in books.EnumerateArray()){
                this._booksCache.Add(element.GetProperty("title").ToString(),element.GetProperty("_id").ToString());
            }

        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error making GET request: {e.Message}");
            throw;
        }
    }

    public List<string> GetAvailableBooks(){
        return _booksCache.Keys.ToList();
    }
}