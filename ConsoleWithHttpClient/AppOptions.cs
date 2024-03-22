using System.Text.Json.Serialization;

namespace ConsoleWithHttpClient;

public class AppOptions
{
    [JsonPropertyName("apiUrl")]
    public string ApiUrl { get; set; } = null!;
}