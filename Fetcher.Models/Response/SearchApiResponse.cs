using System.Text.Json.Serialization;

namespace Fetcher.Models.Response
{
    public class SearchApiResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CurrentPage { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Limit { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? NextPage { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? PreviousPage { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<FetchResponse> Short { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<FetchResponse> Medium { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<FetchResponse> Long { get; set; }
        // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        // public int? ShortCount => Short == null ? null : Short.Count;
        // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        // public int? MediumCount => Medium == null ? null : Medium.Count;
        // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        // public int? LongCount => Long == null ? null : Long.Count;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SearchTerm { get; set; }
        [JsonIgnore]
        public int? Status { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? TotalJokes { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? TotalPages { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }
    }
}