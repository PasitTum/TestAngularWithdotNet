using System.ComponentModel.DataAnnotations;

namespace TestTCCBackEnd.DTOs;

public class CommentRequest
{
    [Required(ErrorMessage = "กรุณากรอกชื่อผู้ใช้")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณากรอกข้อความ")]
    public string CommentText { get; set; } = string.Empty;
}
