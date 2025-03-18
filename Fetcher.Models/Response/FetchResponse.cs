using System.Text.Json.Serialization;

namespace Fetcher.Models.Response;

public class FetchResponse
{
    [JsonPropertyName("id")] public string JokeId { get; set; }
    [JsonPropertyName("joke")] public string Joke { get; set; }
    [JsonIgnore] public int Status { get; set; }
}
