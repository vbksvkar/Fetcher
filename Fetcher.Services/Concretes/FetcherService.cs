using System.Text.RegularExpressions;
using Fetcher.Models.Request;
using Fetcher.Models.Response;
using Fetcher.Services.Clients;
using Fetcher.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Fetcher.Services.Concretes;

public class FetcherService: IFetcherService
{
    private readonly ILogger<IFetcherService> logger;
    private readonly IDadJokeClient dadJokeClient;

    public FetcherService(ILogger<IFetcherService> logger,
        IDadJokeClient dadJokeClient)
    {
        this.logger = logger;
        this.dadJokeClient = dadJokeClient;
    }

    /// <summary>
    /// 1. Fetch a random joke.
    /// </summary>
    /// <returns><see cref="FetchResponse"/> </returns>
    public async Task<FetchResponse> FetchRandom()
    {
        return await dadJokeClient.GetRandomDadJoke();
    }

    /// <summary>
    /// There should be two options the user can choose from:
    /// Accept a search term and display the first 30 jokes containing that term. 
    ///      The matching term should be emphasized in some simple way (upper, quotes, angle brackets, etc.). 
    ///      The matching jokes should be grouped by length: Short (< 10 words), Medium (< 20 words), Long (>= 20 words).
    /// </summary>
    /// <param name="request"><see cref="SearchRequest"/> </param>
    /// <returns><see cref="SearchApiResponse"/> </returns>
    public async Task<SearchApiResponse> Search(SearchRequest request)
    {
        /// Commented this code to demontrate Global Exception Handler
        /// Uncomment this code to see the validation error (400) in the response
        // if (string.IsNullOrEmpty(request.Term))
        // {
        //     return new SearchApiResponse
        //     {
        //         Status = 400,
        //         Error = "Search term is required.",
        //     };
        // }

        var queryString = new Dictionary<string, string> 
        {
            { "term", request.Term },
        };
        if (request.Limit.HasValue)
        {
            queryString.Add("limit", request.Limit.Value.ToString());
        }
        if (request.Page.HasValue)
        {
            queryString.Add("page", request.Page.Value.ToString());
        }

        var jokesResults = await dadJokeClient.SearchDadJokes(queryString);
        if (jokesResults.Status != 200)
        {
            logger.LogError($"Failed to fetch dad jokes. Status code: {jokesResults.Status}");
            return new SearchApiResponse
            {
                Status = jokesResults.Status,
            };
        }

        SearchApiResponse searchApiResponse = new SearchApiResponse
        {
            Limit = request.Limit, 
            Status = jokesResults.Status,
            SearchTerm = jokesResults.SearchTerm
        };

        if (jokesResults.Results.Count == 0)
        {
            logger.LogWarning("No dad jokes found. ");
            searchApiResponse.Message = "No dad jokes found.";
            if (request.Page.HasValue && request.Page.Value > jokesResults.TotalPages)
            {
                searchApiResponse.Message = "No dad jokes found. Page number is out of range.";
            }
            
            return searchApiResponse;
        }

        //display the first 30 jokes containing that term
        if (jokesResults.Results.Count > 30)
        {
            jokesResults.Results = jokesResults.Results.Take(30).ToList();
        }

        searchApiResponse.Short = jokesResults.Results.Where(x => x.Joke.Split(' ').Length < 10).ToList();
        searchApiResponse.Medium = jokesResults.Results.Where(x => x.Joke.Split(' ').Length >= 10 
            && x.Joke.Split(' ').Length < 20).ToList();
        searchApiResponse.Long = jokesResults.Results.Where(x => x.Joke.Split(' ').Length >= 20).ToList();
        searchApiResponse.TotalJokes = jokesResults.TotalJokes;
        searchApiResponse.TotalPages = jokesResults.TotalPages;
        searchApiResponse.CurrentPage = jokesResults.CurrentPage;
        searchApiResponse.NextPage = jokesResults.NextPage;
        searchApiResponse.PreviousPage = jokesResults.PreviousPage;

        foreach (var item in jokesResults.Results)
        {
            item.Joke = Regex.Replace(item.Joke, $@"\b{Regex.Escape(request.Term)}\b", $"<{request.Term}>", RegexOptions.IgnoreCase);
        }
        
        return searchApiResponse;
    }
}