namespace githubSearch.Api.Models;

public class BookmarkEntry
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Description { get; set; }
}