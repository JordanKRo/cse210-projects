using System;
using System.Runtime.InteropServices;
using ToolBox;

using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    static void Main(string[] args)
    {
        // SerializeInheritance();
        List<Animal> animals = new List<Animal>
        {
            new Dog { Name = "Rex", Breed = "German Shepherd" },
            new Cat { Name = "Whiskers", LivesRemaining = 7 }
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        // Special Converter is needed for their type to remain in tact
        options.Converters.Add(new JsonStringEnumConverter());
        options.Converters.Add(new DerivedTypeJsonConverter<Animal>());

        // Serialize the list of animals
        string json = JsonSerializer.Serialize(animals, options);
        Console.WriteLine(json);

        // Deserialize back to the list of animals
        List<Animal> deserializedAnimals = JsonSerializer.Deserialize<List<Animal>>(json, options);
        Console.WriteLine("Animals reconstituted");
        foreach (var animal in deserializedAnimals)
        {
            Console.WriteLine($"Type: {animal.GetType().Name}, Name: {animal.Name}\n{animal.Speak()}");
        }
    }

    public static void SerializeInheritance()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        // Add polymorphic type handling
        options.Converters.Add(new JsonStringEnumConverter());
        options.Converters.Add(new DerivedTypeJsonConverter<Animal>());

        // Create a derived class instance
        Animal myPet = new Dog { Name = "Rex", Breed = "German Shepherd" };

        // Serialize with type information
        string json = JsonSerializer.Serialize(myPet, options);
        Console.WriteLine(json);

        // Deserialize back to the correct type
        Animal deserializedPet = JsonSerializer.Deserialize<Animal>(json, options);
        Console.WriteLine($"Type: {deserializedPet.GetType().Name}");
    }
}

// Base class
public abstract class Animal
{
    public string Name { get; set; }

    [JsonIgnore]
    public string InternalId { get; set; } // Excluded from serialization

    public abstract string Speak();
}

// Derived classes
public class Dog : Animal
{
    public string Breed { get; set; }
    public bool IsGoodBoy { get; set; } = true;

    public override string Speak()
    {
        if(IsGoodBoy){
            return "I am good boy";
        } else {
            return $"I am a {Breed}";
        }
    }

}

public class Cat : Animal
{
    public int LivesRemaining { get; set; } = 9;

    public override string Speak()
    {
        return $"I have {LivesRemaining} lives left";
    }
}



// Custom converter for handling derived types
public class DerivedTypeJsonConverter<TBase> : JsonConverter<TBase> where TBase : class
{
    public override TBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Create new options without this converter to avoid infinite recursion
        var newOptions = new JsonSerializerOptions(options);
        for (int i = newOptions.Converters.Count - 1; i >= 0; i--)
        {
            if (newOptions.Converters[i] is DerivedTypeJsonConverter<TBase>)
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
            if (newOptions.Converters[i] is DerivedTypeJsonConverter<TBase>)
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

