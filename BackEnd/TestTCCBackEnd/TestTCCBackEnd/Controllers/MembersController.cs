using Microsoft.AspNetCore.Mvc;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Services;

namespace TestTCCBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _service;

    public MembersController(IMemberService service) => _service = service;

    /// <summary>POST /api/members — บันทึกข้อมูลสมาชิก</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MemberRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.CreateAsync(request);
            return Ok(new
            {
                message = "save data success",
                id = result.Id,
                data = result
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>GET /api/members/occupations — ดึง Combobox อาชีพ</summary>
    [HttpGet("occupations")]
    public async Task<IActionResult> GetOccupations()
    {
        var result = await _service.GetOccupationsAsync();
        return Ok(result);
    }
}
