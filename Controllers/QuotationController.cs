// Controllers/QuotationController.cs

using Microsoft.AspNetCore.Mvc;
using System;

using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/quotations")]
public class QuotationController : ControllerBase
{
    private readonly IQuotationService _quotationService;

    public QuotationController(IQuotationService quotationService)
    {
        _quotationService = quotationService;
    }

    [HttpGet]
    public IActionResult GetAllQuotations()
    {
        var quotations = _quotationService.GetAllQuotations();
        return Ok(quotations);
    }

    [HttpGet("{id}")]
    public IActionResult GetQuotationById(int id)
    {
        var quotation = _quotationService.GetQuotationById(id);

        if (quotation != null)
        {
            return Ok(quotation);
        }

        return NotFound(new { message = "Quotation not found" });
    }

    [Authorize]
    [HttpPost]
    public IActionResult AddQuotation([FromBody] Quotation newQuotation)
    {
        _quotationService.AddQuotation(newQuotation);
        return Ok(new { message = "Quotation added successfully" });
    }

    [Authorize]
    [HttpPut]
    public IActionResult UpdateQuotation([FromBody] Quotation updatedQuotation)
    {
        _quotationService.UpdateQuotation(updatedQuotation);
        return Ok(new { message = "Quotation updated successfully" });
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteQuotation(int id)
    {
        _quotationService.DeleteQuotation(id);
        return Ok(new { message = "Quotation deleted successfully" });
    }
}
