using Newtonsoft.Json.Linq;



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
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("githubSearchApp");
    }


    // Search repositories on GitHub
    public async Task<JObject> SearchRepositories(string query)
    {
        var response = await _httpClient.GetAsync($"search/repositories?q={query}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JObject.Parse(json);
    }
}