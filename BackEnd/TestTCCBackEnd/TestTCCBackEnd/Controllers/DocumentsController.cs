using Microsoft.AspNetCore.Mvc;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Services;

namespace TestTCCBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _service;

    public DocumentsController(IDocumentService service) => _service = service;

    /// <summary>GET /api/documents — ดึงรายการทั้งหมด</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    /// <summary>POST /api/documents/approve — อนุมัติเอกสารที่เลือก</summary>
    [HttpPost("approve")]
    public async Task<IActionResult> Approve([FromBody] ApprovalRequest request)
    {
        if (!request.DocumentIds.Any())
            return BadRequest(new { message = "กรุณาเลือกเอกสารอย่างน้อย 1 รายการ" });

        try
        {
            await _service.ApproveAsync(request);
            return Ok(new { message = "อนุมัติเอกสารเรียบร้อยแล้ว" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>POST /api/documents/reject — ไม่อนุมัติเอกสารที่เลือก</summary>
    [HttpPost("reject")]
    public async Task<IActionResult> Reject([FromBody] ApprovalRequest request)
    {
        if (!request.DocumentIds.Any())
            return BadRequest(new { message = "กรุณาเลือกเอกสารอย่างน้อย 1 รายการ" });

        try
        {
            await _service.RejectAsync(request);
            return Ok(new { message = "ไม่อนุมัติเอกสารเรียบร้อยแล้ว" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
