using Fetcher.Models.Request;
using Fetcher.Models.Response;

namespace Fetcher.Services.Clients;

public interface IDadJokeClient
{
    Task<FetchResponse> GetRandomDadJoke();
    Task<SearchResponse> SearchDadJokes(IDictionary<string, string> queryString); 
}
