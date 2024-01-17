// AwardsController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// AwardsController.cs
[ApiController]
[Route("api/[controller]")]
public class AwardsController : ControllerBase
{
    private readonly IAwardService _awardService;

    public AwardsController(IAwardService awardService)
    {
        _awardService = awardService ?? throw new ArgumentNullException(nameof(awardService));
    }

    [HttpGet]
    public ActionResult<IEnumerable<Award>> Get()
    {
        var awards = _awardService.GetAllAwards();
        return Ok(awards);
    }

    [HttpGet("{id}")]
    public ActionResult<Award> Get(int id)
    {
        var award = _awardService.GetAwardById(id);

        if (award == null)
        {
            return NotFound();
        }

        return Ok(award);
    }

    [HttpPost]
    public ActionResult<Award> Post([FromBody] Award award)
    {
        _awardService.AddAward(award);
        return CreatedAtAction(nameof(Get), new { id = award.Id }, award);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Award updatedAward)
    {
        _awardService.UpdateAward(id, updatedAward);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _awardService.DeleteAward(id);
        return NoContent();
    }
}

