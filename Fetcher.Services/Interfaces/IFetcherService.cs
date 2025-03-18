using Fetcher.Models.Request;
using Fetcher.Models.Response;

namespace Fetcher.Services.Interfaces;

public interface IFetcherService
{
    Task<FetchResponse> FetchRandom();
    Task<SearchApiResponse> Search(SearchRequest request);
}