// Interfaces/INewsService.cs

using System.Collections.Generic;

public interface INewsService
{
    List<News> GetAllNews();
    News GetNewsById(int id);
    void AddNews(News newNews);
    void UpdateNews(News updatedNews);
    void DeleteNews(int id);
}
