using Microsoft.AspNetCore.Mvc;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Services;

namespace TestTCCBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamQuestionsController : ControllerBase
{
    private readonly IExamQuestionService _service;

    public ExamQuestionsController(IExamQuestionService service) => _service = service;

    /// <summary>GET /api/examquestions — ดึงรายการข้อสอบทั้งหมด เรียงตามข้อ (IT 08-1)</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    /// <summary>POST /api/examquestions — บันทึกข้อสอบใหม่ (IT 08-2 กดบันทึก)</summary>
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ExamQuestionRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.AddAsync(request);
        return Ok(result);
    }

    /// <summary>DELETE /api/examquestions/{id} — ลบข้อสอบและ Re-number (IT 08-1 กดลบ)</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return Ok(new { message = "ลบข้อสอบเรียบร้อยแล้ว" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
