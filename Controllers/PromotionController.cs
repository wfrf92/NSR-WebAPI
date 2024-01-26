// Controllers/PromotionController.cs

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using System;

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
        // Check if the user is authenticated
        if (User.Identity.IsAuthenticated)
        {
            return Ok(_promotionService.GetAllPromotions());
        }

        return Ok(_promotionService.GetAllPromotions().Where(x => x.Active).ToList());
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

    [Authorize]
    [HttpPost]
    public IActionResult AddPromotion([FromBody] Promotion newPromotion)
    {
        _promotionService.AddPromotion(newPromotion);

        if (newPromotion.Id == 0)
            return Ok(new { message = "Promotion added successfully" });
        else
            return Ok(new { message = "Promotion updated successfully" });
    }
}
