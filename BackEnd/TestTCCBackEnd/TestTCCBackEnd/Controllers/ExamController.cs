using Microsoft.AspNetCore.Mvc;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Services;

namespace TestTCCBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamController : ControllerBase
{
    private readonly IExamService _service;

    public ExamController(IExamService service) => _service = service;

    /// <summary>
    /// GET /api/exam/questions
    /// ดึงโจทย์ทั้งหมด ไม่รวมเฉลย (IT 10-1 โหลดหน้า)
    /// </summary>
    [HttpGet("questions")]
    public async Task<IActionResult> GetQuestions()
    {
        var result = await _service.GetQuestionsAsync();
        return Ok(result);
    }

    /// <summary>
    /// POST /api/exam/submit
    /// ส่งข้อสอบ — ตรวจ, บันทึก, คืนผลลัพธ์ (IT 10-1 กดส่งข้อสอบ → แสดง IT 10-2)
    /// </summary>
    [HttpPost("submit")]
    public async Task<IActionResult> Submit([FromBody] ExamSubmitRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.SubmitAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
