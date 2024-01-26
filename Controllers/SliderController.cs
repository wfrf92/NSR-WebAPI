// Controllers/SliderController.cs

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/sliders")]
public class SliderController : ControllerBase
{
    private readonly ISliderService _sliderService;

    public SliderController(ISliderService sliderService)
    {
        _sliderService = sliderService;
    }

    [HttpGet]
    public IActionResult GetAllSliders()
    {
        // Check if the user is authenticated
        if (User.Identity.IsAuthenticated)
        {
            return Ok(_sliderService.GetAllSliders());
        }

        return Ok(_sliderService.GetAllSliders().Where(x => x.Active).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetSliderById(int id)
    {
        var slider = _sliderService.GetSliderById(id);

        if (slider != null)
        {
            return Ok(slider);
        }

        return NotFound(new { message = "Slider not found" });
    }

    [Authorize]
    [HttpPost]
    public IActionResult AddSlider([FromBody] Slider newSlider)
    {
        _sliderService.AddSlider(newSlider);

        if (newSlider.Id == 0)
            return Ok(new { message = "Slider added successfully" });
        else
            return Ok(new { message = "Slider updated successfully" });
    }
}
