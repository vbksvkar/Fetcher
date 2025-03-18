using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Fetcher.Models.Request;

public class SearchRequest
{
    [JsonPropertyName("term")] [FromQuery] public string Term { get; set; }
    [JsonPropertyName("limit")] [FromQuery] public int? Limit { get; set; } = 30;
    [JsonPropertyName("page")] [FromQuery] public int? Page { get; set; } = 1;
}