namespace Fetcher.Models.Configs;

public class FetcherConfig
{
    public DadJokesConfig DadJokesApi { get; set; }
}

public class DadJokesConfig
{
    public string BaseAddress { get; set; }
    public string UserAgent { get; set; }
}