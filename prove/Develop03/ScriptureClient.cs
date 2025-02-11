using System.Reflection.Metadata;
using System.Text.Json;

public class ScriptureClient{

    private const string baseRequestUrl = "https://openscriptureapi.org/api/scriptures/v1/lds/en/volume/bookofmormon";
    private const string booksUrl = "https://openscriptureapi.org/api/scriptures/v1/lds/en/volume/bookofmormon";
    private Dictionary<string,string> books;

    private readonly HttpClient client = new HttpClient();
    
    public ScriptureClient(){
        
    }

    public async Task<Scripture> GetScriptureFromApi(Reference reference){
        string bookId = "";
        int verseStart = reference.GetStart();
        int numberVerses = reference.GetEnd() - reference.GetStart();
        if (books.TryGetValue(reference.GetBook(), out string result)){
            bookId = result;
        }else{
            return null;
        }
        
        try
        {
            HttpResponseMessage response = await client.GetAsync(baseRequestUrl);
            response.EnsureSuccessStatusCode();
            string plainJson = await response.Content.ReadAsStringAsync();
            JsonDocument document = JsonSerializer.Deserialize<JsonDocument>(plainJson);
            document
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
                this.books.Add(element.GetProperty("title").ToString(),element.GetProperty("_id").ToString());
            }

        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error making GET request: {e.Message}");
            throw;
        }
    }
}