using Fetcher.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fetcher.Api.Controllers;

public class BaseController: ControllerBase
{
    public async Task<IActionResult> HandleAsync<T>(SearchApiResponse response)
    {
        if (response.Status.HasValue)
        {
            return StatusCode(response.Status.Value, response);
        }

        return await Task.FromResult(Ok(response));
    }
}