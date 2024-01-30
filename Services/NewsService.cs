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
        var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Json/news.json");

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
        var news = _newsList.Find(news => news.Id == id);

        return news ?? new News();
    }

    public void AddNews(News newNews)
    {
        if (newNews.Id == 0)
        {
            newNews.Active = true;
            newNews.Id = _newsList.Count + 1;
            _newsList.Add(newNews);
        }
        else
        {
            var news = _newsList.Find(x => x.Id == newNews.Id);
            if (news != null)
            {
                news.Title = newNews.Title;
                news.Subtitle = newNews.Subtitle;
                news.ImageUrl = newNews.ImageUrl;
                news.Description = newNews.Description;
                news.Description2 = newNews.Description2;
                news.Active = newNews.Active;
                news.Date = newNews.Date;
                news.Author = newNews.Author;
                news.ImageUrl = newNews.ImageUrl;
                news.ImageUrl2 = newNews.ImageUrl;
                news.ImgDescription = newNews.ImgDescription;
                news.ImgTitle = newNews.ImgTitle;
            }
        }
        // Serialize the updated products list to JSON
        string json = JsonConvert.SerializeObject(_newsList);

        // Specify the path to your JSON file
        string jfilePath = "Json/news.json";

        // Write the JSON data to the file
        File.WriteAllTextAsync(jfilePath, json);
    }

    public void UpdateNews(News updatedNews)
    {
        var index = _newsList.FindIndex(news => news.Id == updatedNews.Id);
        if (index != -1)
        {
            _newsList[index] = updatedNews;

            // Serialize the updated products list to JSON
            string json = JsonConvert.SerializeObject(_newsList);

            // Specify the path to your JSON file
            string jfilePath = "Json/news.json";

            // Write the JSON data to the file
            File.WriteAllTextAsync(jfilePath, json);
        }
    }

    public void DeleteNews(int id)
    {
        _newsList.RemoveAll(news => news.Id == id);
    }
}
