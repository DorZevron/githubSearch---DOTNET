namespace githubSearch.Api.Models;

public class UserLoginDto
{
    public required string username { get; set; }
    public required string password { get; set; }
}