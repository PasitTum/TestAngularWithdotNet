using Microsoft.AspNetCore.Mvc;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Services;

namespace TestTCCBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SerialCodesController : ControllerBase
{
    private readonly ISerialCodeService _service;

    public SerialCodesController(ISerialCodeService service) => _service = service;

    /// <summary>GET /api/serialcodes — ดึงรายการรหัสสินค้าทั้งหมด</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    /// <summary>POST /api/serialcodes — เพิ่มรหัสสินค้า</summary>
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] SerialCodeRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.AddAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>DELETE /api/serialcodes/{id} — ลบรหัสสินค้า</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return Ok(new { message = "ลบรหัสสินค้าเรียบร้อยแล้ว" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
