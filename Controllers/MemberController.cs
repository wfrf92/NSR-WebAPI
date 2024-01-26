// Controllers/MemberController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/members")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public IActionResult GetAllMembers()
    {
        var members = _memberService.GetAllMembers();
        return Ok(members);
    }

    [HttpGet("{id}")]
    public IActionResult GetMemberById(int id)
    {
        var member = _memberService.GetMemberById(id);

        if (member != null)
        {
            return Ok(member);
        }

        return NotFound(new { message = "Member not found" });
    }

    [Authorize]
    [HttpPost]
    public IActionResult AddMember([FromBody] Member newMember)
    {
        _memberService.AddMember(newMember);

        if (newMember.Id == 0)
            return Ok(new { message = "Member added successfully" });
        else
            return Ok(new { message = "Member updated successfully" });
    }
}
