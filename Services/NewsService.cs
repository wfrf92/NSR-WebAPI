// Services/NewsService.cs

using System.Collections.Generic;
using Newtonsoft.Json;

public class NewsService : INewsService
{
    private List<News> _newsList;

    public NewsService()
    {
        _newsList = GetNewsFromJsonFile();
    }

    public List<News> GetNewsFromJsonFile()
    {
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Json/news.json");

        if (!File.Exists(jsonFilePath))
        {
            // Handle the case when the file does not exist
            return new List<News>();
        }

        var json = File.ReadAllText(jsonFilePath);
        var products = JsonConvert.DeserializeObject<List<News>>(json);

        return products ?? new List<News>();
    }

    public List<News> GetAllNews()
    {
        return _newsList;
    }

    public News GetNewsById(int id)
    {
        return _newsList.Find(news => news.Id == id);
    }

    public void AddNews(News newNews)
    {
        _newsList.Add(newNews);
    }

    public void UpdateNews(News updatedNews)
    {
        var index = _newsList.FindIndex(news => news.Id == updatedNews.Id);
        if (index != -1)
        {
            _newsList[index] = updatedNews;
        }
    }

    public void DeleteNews(int id)
    {
        _newsList.RemoveAll(news => news.Id == id);
    }
}
