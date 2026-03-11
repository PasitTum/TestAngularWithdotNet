using Microsoft.AspNetCore.Mvc;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Services;

namespace TestTCCBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _service;

    public PostsController(IPostService service) => _service = service;

    /// <summary>
    /// GET /api/posts/{postId}
    /// ดึง Post พร้อม Comments ทั้งหมด (IT 08-1 โหลดหน้า)
    /// </summary>
    [HttpGet("{postId:int}")]
    public async Task<IActionResult> GetPost(int postId)
    {
        var result = await _service.GetPostWithCommentsAsync(postId);
        if (result is null)
            return NotFound(new { message = $"ไม่พบ Post ID {postId}" });

        return Ok(result);
    }

    /// <summary>
    /// POST /api/posts/{postId}/comments
    /// เพิ่ม Comment (IT 08-1 กด ENTER)
    /// </summary>
    [HttpPost("{postId:int}/comments")]
    public async Task<IActionResult> AddComment(int postId, [FromBody] CommentRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.AddCommentAsync(postId, request);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
