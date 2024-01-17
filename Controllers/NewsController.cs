// Controllers/NewsController.cs

using Microsoft.AspNetCore.Mvc;

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
        var newsList = _newsService.GetAllNews();
        return Ok(newsList);
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

    [HttpPost]
    public IActionResult AddNews([FromBody] News newNews)
    {
        _newsService.AddNews(newNews);
        return Ok(new { message = "News added successfully" });
    }

    [HttpPut]
    public IActionResult UpdateNews([FromBody] News updatedNews)
    {
        _newsService.UpdateNews(updatedNews);
        return Ok(new { message = "News updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteNews(int id)
    {
        _newsService.DeleteNews(id);
        return Ok(new { message = "News deleted successfully" });
    }
}
