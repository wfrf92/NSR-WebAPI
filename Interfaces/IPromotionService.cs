// Interfaces/IPromotionService.cs

using System.Collections.Generic;

public interface IPromotionService
{
    List<Promotion> GetAllPromotions();
    Promotion GetPromotionById(int id);
    void AddPromotion(Promotion newPromotion);
    void UpdatePromotion(Promotion updatedPromotion);
    void DeletePromotion(int id);
}
