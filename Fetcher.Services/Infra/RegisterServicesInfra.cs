using Fetcher.Models.Configs;
using Fetcher.Services.Clients;
using Fetcher.Services.Concretes;
using Fetcher.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fetcher.Services.Infra;

public static class RegisterServicesInfra
{
    public static void ConfigureServicesInfra(this IServiceCollection services)
    {
        services.AddScoped<IFetcherService, FetcherService>();
        services.AddHttpClient<IDadJokeClient, DadJokeClient>((provider, httpClient) => {
            var fetcherConfigOptions = provider.GetRequiredService<IOptions<FetcherConfig>>();
            var fetcherConfig = fetcherConfigOptions.Value;
            httpClient.BaseAddress = new Uri(fetcherConfig.DadJokesApi.BaseAddress);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", fetcherConfig.DadJokesApi.UserAgent);
        });
    }
}