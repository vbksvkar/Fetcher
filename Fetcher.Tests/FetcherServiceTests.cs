
using Fetcher.Models.Request;
using Fetcher.Models.Response;
using Fetcher.Services.Clients;
using Fetcher.Services.Concretes;
using Fetcher.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fetcher.Tests;

public class FetcherServiceTests
{
    private readonly Mock<ILogger<IFetcherService>> mockLogger;
    private readonly Mock<IDadJokeClient> mockDadJokeClient;
    private readonly IFetcherService fetcherService;
    private readonly SearchRequest request = new SearchRequest
    {
        Term = "is"
    };
    private FetchResponse jokeOneResponse = new FetchResponse
    {
        JokeId = "1",
        Joke = "which flower <is> most fierce? Dandelion"
    };
    private FetchResponse jokeTwoResponse = new FetchResponse
    {
        JokeId = "2",
        Joke = "What <is> the least spoken language in the world? Sign Language"
    };
    private FetchResponse jokeThreeResponse = new FetchResponse
    {
        JokeId = "3",
        Joke = "A woman <is> on trial for beating her husband to death with his guitar collection. Judge says, ‘First offender?’ She says, ‘No, first a Gibson! Then a Fender!"
    };


    public FetcherServiceTests()
    {
        mockLogger = new Mock<ILogger<IFetcherService>>();
        mockDadJokeClient = new Mock<IDadJokeClient>();
        fetcherService = new FetcherService(mockLogger.Object, mockDadJokeClient.Object);
    }

    [Fact]
    public async Task Search_ShouldReturnJokesWithTermEmphasized()
    {
        // Arrange
        List<FetchResponse> shortJoke = new List<FetchResponse> { jokeOneResponse };
        List<FetchResponse> mediumJoke = new List<FetchResponse> { jokeTwoResponse };
        List<FetchResponse> longJoke = new List<FetchResponse> { jokeThreeResponse };

        SearchResponse searchResponse = new SearchResponse
        {
            Results = new List<FetchResponse> { 
                jokeOneResponse, jokeTwoResponse, jokeThreeResponse },
            SearchTerm = "is", Status = 200, TotalJokes = 3, TotalPages = 1
        };

        var mockClient = mockDadJokeClient.Setup(client => 
            client.SearchDadJokes(It.IsAny<Dictionary<string, string>>()))
            .ReturnsAsync(searchResponse);
        
        // Act
        var response = await fetcherService.Search(request);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Equal(response.Short, shortJoke);
        Assert.Equal(response.Medium, mediumJoke);
        Assert.Equal(response.Long, longJoke);
    }

    [Fact]
    public async Task Search_ShouldGroupJokesByLength()
    {
        // Arrange
        List<FetchResponse> shortJoke = new List<FetchResponse> { jokeOneResponse };
        List<FetchResponse> mediumJoke = new List<FetchResponse> { jokeTwoResponse };
        List<FetchResponse> longJoke = new List<FetchResponse> { jokeThreeResponse };

        SearchResponse searchResponse = new SearchResponse
        {
            Results = new List<FetchResponse> { 
                jokeOneResponse, jokeTwoResponse, jokeThreeResponse },
            SearchTerm = "is", Status = 200, TotalJokes = 3, TotalPages = 1
        };

        mockDadJokeClient.Setup(client => client.SearchDadJokes(It.IsAny<Dictionary<string, string>>()))
            .ReturnsAsync(searchResponse);

        // Act
        var response = await fetcherService.Search(request);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Single(response.Short);
        Assert.Single(response.Medium);
        Assert.Single(response.Long);
    }
}