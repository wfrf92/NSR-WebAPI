// Controllers/NewsController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/news")]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet]
    public IActionResult GetAllNews()
    {
        // Check if the user is authenticated
        if (User.Identity.IsAuthenticated)
        {
            return Ok(_newsService.GetAllNews());
        }

        return Ok(_newsService.GetAllNews().Where(x=>x.Active).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetNewsById(int id)
    {
        var news = _newsService.GetNewsById(id);

        if (news != null)
        {
            return Ok(news);
        }

        return NotFound(new { message = "News not found" });
    }

    [Authorize]
    [HttpPost]
    public IActionResult AddNews([FromBody] News newNews)
    {
        _newsService.AddNews(newNews);
        return Ok(new { message = "News added successfully" });
    }

    [Authorize]
    [HttpPut]
    public IActionResult UpdateNews([FromBody] News updatedNews)
    {
        _newsService.UpdateNews(updatedNews);
        return Ok(new { message = "News updated successfully" });
    }
}
