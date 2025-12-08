namespace githubSearch.Api.Models;

public class BookmarkEntry
{
    public required int id { get; set; }
    public required string name { get; set; }
    public string? avatarUrl { get; set; }
    public string? description { get; set; }
}