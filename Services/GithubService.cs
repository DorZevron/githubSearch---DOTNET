using Newtonsoft.Json;



namespace githubSearch.Api.Services;

//---------------
// GithubService.cs - GitHub API Integration
//---------------

public class GithubService
{
    private readonly HttpClient _httpClient;


    public GithubService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.github.com/")
        };
        // _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("githubSearchApp");
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("githubSearchApp/1.0");
    _httpClient.DefaultRequestHeaders.Accept
        .ParseAdd("application/vnd.github+json");
    }


    // Search repositories on GitHub
    public async Task<string> SearchRepositories(string query)
    {
        var response = await _httpClient.GetAsync($"search/repositories?q={query}&per_page=50");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"json:{json}");
        // return JsonConvert.DeserializeObject<dynamic>(json);
        return json;
    }
}