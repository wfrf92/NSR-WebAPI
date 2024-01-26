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
        var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Json/promotions.json");

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
        if (newPromotion.Id == 0)
        {
            newPromotion.Active = true;
            newPromotion.Id = _promotions.Count + 1;
            _promotions.Add(newPromotion);
        } 
        else{
            var promotion = _promotions.Find(x=>x.Id == newPromotion.Id);
            if(promotion != null){
              promotion.Title = newPromotion.Title;
              promotion.Subtitle = newPromotion.Subtitle;
              promotion.ImageUrl = newPromotion.ImageUrl;
              promotion.Description = newPromotion.Description;
              promotion.Condition = newPromotion.Condition;
              promotion.Active = newPromotion.Active;
              promotion.Discount = newPromotion.Discount;
              promotion.LargeDescription = newPromotion.LargeDescription;
            }

        }
        
        // Serialize the updated products list to JSON
        string json = JsonConvert.SerializeObject(_promotions);

        // Specify the path to your JSON file
        string jfilePath = "Json/promotions.json";

        // Write the JSON data to the file
        File.WriteAllTextAsync(jfilePath, json);
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
