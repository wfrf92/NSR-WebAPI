// Controllers/PromotionController.cs

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using System;

[Authorize]
[ApiController]
[Route("api/promotions")]
public class PromotionController : ControllerBase
{
    private readonly IPromotionService _promotionService;

    public PromotionController(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }

    [HttpGet]
    public IActionResult GetAllPromotions()
    {
        var promotions = _promotionService.GetAllPromotions();
        return Ok(promotions);
    }

    [HttpGet("{id}")]
    public IActionResult GetPromotionById(int id)
    {
        var promotion = _promotionService.GetPromotionById(id);

        if (promotion != null)
        {
            return Ok(promotion);
        }

        return NotFound(new { message = "Promotion not found" });
    }

    [HttpPost]
    public IActionResult AddPromotion([FromBody] Promotion newPromotion)
    {
        _promotionService.AddPromotion(newPromotion);
        return Ok(new { message = "Promotion added successfully" });
    }

    [HttpPut]
    public IActionResult UpdatePromotion([FromBody] Promotion updatedPromotion)
    {
        _promotionService.UpdatePromotion(updatedPromotion);
        return Ok(new { message = "Promotion updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePromotion(int id)
    {
        _promotionService.DeletePromotion(id);
        return Ok(new { message = "Promotion deleted successfully" });
    }
}
