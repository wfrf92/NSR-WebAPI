// ContactController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactRepository;
    private FooterContactUs _contactInfo;

    public ContactController(IContactService contactRepository)
    {
        _contactRepository = contactRepository;
        _contactInfo = GetContactInfoAsync().Result;
    }

    [HttpGet]
    public Task<FooterContactUs> GetContactInfoAsync()
    {
        return Task.FromResult(_contactRepository.GetContactInfoAsync().Result);
    }

    [HttpPost("updateContactInfoAsync")]
    public Task<bool> UpdateContactInfoAsync(FooterContactUs updatedContactInfo)
    {
        try
        {
            return Task.FromResult(_contactRepository.UpdateContactInfoAsync(updatedContactInfo).Result);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            return Task.FromResult(false);
        }
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
