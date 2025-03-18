using Fetcher.Models.Request;
using Fetcher.Models.Response;
using Fetcher.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fetcher.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FetcherController : BaseController
{
    private readonly IFetcherService fetcherService;

    public FetcherController(IFetcherService fetcherService)
    {
        this.fetcherService = fetcherService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(SearchRequest searchTerm)
        => await HandleAsync<SearchApiResponse>(await fetcherService.Search(searchTerm));
        //=> Ok(await fetcherService.Search(searchTerm));

    [HttpGet]
    public async Task<IActionResult> FetchRandom()
        => Ok(await fetcherService.FetchRandom());
    
}

