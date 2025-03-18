using System.Text.Json.Serialization;

namespace Fetcher.Models.Response;

public class SearchResponse
{
    [JsonPropertyName("current_page")] public int CurrentPage { get; set; }
    [JsonPropertyName("limit")] public int Limit { get; set; }
    [JsonPropertyName("next_page")] public int NextPage { get; set; }
    [JsonPropertyName("previous_page")] public int PreviousPage { get; set; }
    [JsonPropertyName("results")] public List<FetchResponse> Results { get; set; }
    [JsonPropertyName("search_term")] public string SearchTerm { get; set; }
    [JsonPropertyName("status")] public int Status { get; set; }
    [JsonPropertyName("total_jokes")] public int TotalJokes { get; set; }
    [JsonPropertyName("total_pages")] public int TotalPages { get; set; }
}