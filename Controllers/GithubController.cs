using githubSearch.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace githubSearch.Api.Controllers;

//---------------
// GithubController.cs - GitHub API Search
//---------------

[ApiController]
[Route("api/github")]
[Authorize]
public class GithubController : ControllerBase
{
    private readonly GithubService _githubService;

    public GithubController(GithubService githubService)
    {
        _githubService = githubService;
    }


    // Search GitHub repositories
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        var result = await _githubService.SearchRepositories(query);
        return Ok(result);
    }
}