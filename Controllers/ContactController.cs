// ContactController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactRepository;

    public ContactController(IContactService contactRepository)
    {
        _contactRepository = contactRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ContactForm contactForm)
    {
        if (contactForm == null)
        {
            return BadRequest();
        }

        bool result = await _contactRepository.SaveContactForm(contactForm);

        if (result)
        {
            return Ok();
        }
        else
        {
            return StatusCode(500, "Failed to save contact form");
        }
    }
}
