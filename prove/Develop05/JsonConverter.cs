using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonConverter<TBase> : System.Text.Json.Serialization.JsonConverter<TBase> where TBase : class
{
    public override TBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Create new options without this converter to avoid infinite recursion
        var newOptions = new JsonSerializerOptions(options);
        for (int i = newOptions.Converters.Count - 1; i >= 0; i--)
        {
            if (newOptions.Converters[i] is JsonConverter<TBase>)
            {
                newOptions.Converters.Remove(newOptions.Converters[i]);
            }
        }

        // Read the JSON document
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var rootElement = jsonDoc.RootElement;

        // Try to get the $type property
        if (rootElement.TryGetProperty("$type", out var typeProperty))
        {
            var typeName = typeProperty.GetString();
            var type = Type.GetType(typeName);

            if (type != null && type.IsAssignableTo(typeof(TBase)))
            {
                // Deserialize to the actual type
                var json = rootElement.GetRawText();
                return (TBase)JsonSerializer.Deserialize(json, type, newOptions);
            }
        }

        // Fall back to deserializing as the base type
        return JsonSerializer.Deserialize<TBase>(rootElement.GetRawText(), newOptions);
    }

    public override void Write(Utf8JsonWriter writer, TBase value, JsonSerializerOptions options)
    {
        // Create new options without this converter to avoid infinite recursion
        var newOptions = new JsonSerializerOptions(options);
        for (int i = newOptions.Converters.Count - 1; i >= 0; i--)
        {
            if (newOptions.Converters[i] is JsonConverter<TBase>)
            {
                newOptions.Converters.Remove(newOptions.Converters[i]);
            }
        }

        // Create a temporary dictionary to add the type information
        var valueJson = JsonSerializer.Serialize(value, value.GetType(), newOptions);
        var jsonObject = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(valueJson, newOptions);

        // Add the type information
        if (jsonObject != null)
        {
            jsonObject["$type"] = JsonSerializer.SerializeToElement(value.GetType().AssemblyQualifiedName, newOptions);
            JsonSerializer.Serialize(writer, jsonObject, newOptions);
        }
        else
        {
            // Fallback if the above approach fails
            JsonSerializer.Serialize(writer, value, value.GetType(), newOptions);
        }
    }
}