// Services/PromotionService.cs

using System.Collections.Generic;
using Newtonsoft.Json;

public class PromotionService : IPromotionService
{
    private List<Promotion> _promotions;

    public PromotionService()
    {
        _promotions = GetPromoFromJsonFile();
    }

    public List<Promotion> GetPromoFromJsonFile()
    {
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Json/promotions.json");

        if (!File.Exists(jsonFilePath))
        {
            // Handle the case when the file does not exist
            return new List<Promotion>();
        }

        var json = File.ReadAllText(jsonFilePath);
        var products = JsonConvert.DeserializeObject<List<Promotion>>(json);

        return products ?? new List<Promotion>();
    }

    public List<Promotion> GetAllPromotions()
    {
        return _promotions;
    }

    public Promotion GetPromotionById(int id)
    {
        return _promotions.Find(promo => promo.Id == id);
    }

    public void AddPromotion(Promotion newPromotion)
    {
        _promotions.Add(newPromotion);
    }

    public void UpdatePromotion(Promotion updatedPromotion)
    {
        var index = _promotions.FindIndex(promo => promo.Id == updatedPromotion.Id);
        if (index != -1)
        {
            _promotions[index] = updatedPromotion;
        }
    }

    public void DeletePromotion(int id)
    {
        _promotions.RemoveAll(promo => promo.Id == id);
    }
}
