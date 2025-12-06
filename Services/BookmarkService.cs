using githubSearch.Api.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace githubSearch.Api.Services;

//---------------
// BookmarksService.cs - Session Management
//---------------

public class BookmarkService
{
    private readonly IHttpContextAccessor _httpContextAccessor;



    public BookmarkService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext?.Session
    ?? throw new InvalidOperationException("Session is not available");


    // Get bookmarks from session
    public List<BookmarkEntry> GetBookmarks()
    {
        var bookmarksJson = Session.GetString("BOOKMARKS");

        // return string.IsNullOrEmpty(bookmarksJson)
        //     ? new List<BookmarkEntry>()
        //     : JsonConvert.DeserializeObject<List<BookmarkEntry>>(bookmarksJson);

        if (string.IsNullOrEmpty(bookmarksJson))
            return new List<BookmarkEntry>();
        
        var list = JsonConvert.DeserializeObject<List<BookmarkEntry>>(bookmarksJson); 
        return list ?? new List<BookmarkEntry>();
    }


    // Add bookmark if not already exist
    public void AddBookmark(BookmarkEntry entry)
    {
        var listBookmarks = GetBookmarks();
        if (!listBookmarks.Any(b => b.Id == entry.Id))
        {
            listBookmarks.Add(entry);
            Session.SetString("BOOKMARKS", JsonConvert.SerializeObject(listBookmarks));
        }
    }

}
