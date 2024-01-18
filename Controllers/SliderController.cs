// Controllers/SliderController.cs

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;


[Authorize]
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
        var sliders = _sliderService.GetAllSliders();
        return Ok(sliders);
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

    [HttpPost]
    public IActionResult AddSlider([FromBody] Slider newSlider)
    {
        _sliderService.AddSlider(newSlider);
        return Ok(new { message = "Slider added successfully" });
    }

    [HttpPut]
    public IActionResult UpdateSlider([FromBody] Slider updatedSlider)
    {
        _sliderService.UpdateSlider(updatedSlider);
        return Ok(new { message = "Slider updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSlider(int id)
    {
        _sliderService.DeleteSlider(id);
        return Ok(new { message = "Slider deleted successfully" });
    }
}
