using Microsoft.AspNetCore.Mvc;
using TestTCCBackEnd.Services;

namespace TestTCCBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QueueController : ControllerBase
{
    private readonly IQueueService _service;

    public QueueController(IQueueService service) => _service = service;

    /// <summary>GET /api/queue — ดูหมายเลขคิวปัจจุบัน (IT 05-1 โหลดหน้า)</summary>
    [HttpGet]
    public async Task<IActionResult> GetCurrent()
    {
        var result = await _service.GetCurrentAsync();
        return Ok(result);
    }

    /// <summary>POST /api/queue/take — รับบัตรคิว (IT 05-1 กดปุ่ม "รับบัตรคิว")</summary>
    [HttpPost("take")]
    public async Task<IActionResult> TakeTicket()
    {
        try
        {
            var result = await _service.TakeTicketAsync();
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>POST /api/queue/reset — ล้างคิว (IT 05-3 กดปุ่ม "ล้างคิว")</summary>
    [HttpPost("reset")]
    public async Task<IActionResult> Reset()
    {
        var result = await _service.ResetAsync();
        return Ok(result);
    }
}
