using System.Net.Http.Json;
using Fetcher.Models.Request;
using Fetcher.Models.Response;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Fetcher.Services.Clients;

public class DadJokeClient : IDadJokeClient
{
    private readonly HttpClient httpClient;
    private readonly ILogger<IDadJokeClient> logger;

    public DadJokeClient(HttpClient httpClient, ILogger<IDadJokeClient> logger)
    {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    public async Task<FetchResponse> GetRandomDadJoke()
    {
        return await ExecuteRequest<FetchResponse>(this.httpClient.BaseAddress.AbsoluteUri);
    }

    public async Task<SearchResponse> SearchDadJokes(IDictionary<string, string> queryString)
    {
        var uriString = $"{this.httpClient.BaseAddress.AbsoluteUri}/search";
        if (queryString == null)
        {
            throw new ArgumentNullException("Query string cannot be null");
        }
        if (!queryString.ContainsKey("term"))
        {
            throw new ArgumentNullException("Query string must contain a term");
        }

        var term = QueryHelpers.AddQueryString(uriString, queryString);
        return await ExecuteRequest<SearchResponse>(term);
    }

    private async Task<R> ExecuteRequest<R>(string url) where R : class
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentNullException("Url cannot be null or empty");
        }

        this.logger.LogInformation($"Requesting data from {url}");
        var response = await httpClient.GetAsync(url);
        return await response.Content.ReadFromJsonAsync<R>();
    }
}
