using githubSearch.Api.Models;
using githubSearch.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace githubSearch.Api.Controllers;
//---------------
// BookmarksController.cs - Session Management
//---------------

[ApiController]
[Route("api/bookmarks")]
[Authorize]
public class BookmarksController : ControllerBase
{
    private readonly BookmarkService _bookmarkService;

    public BookmarksController(BookmarkService bookmarkService)
    {
        _bookmarkService = bookmarkService;
    }


    // Get all bookmarks
    [HttpGet]
    public IActionResult GetBookmarks()
    {
        var bookmarks = _bookmarkService.GetBookmarks();
        return Ok(bookmarks);
    }


    // Add a new bookmark
    [HttpPost]
    public IActionResult AddBookmark([FromBody] BookmarkEntry entry)
    {
        _bookmarkService.AddBookmark(entry);
        return Ok(new { message = "Bookmark added" });
    }
}