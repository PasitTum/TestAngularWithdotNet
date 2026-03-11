using System.ComponentModel.DataAnnotations;

namespace TestTCCBackEnd.DTOs;

public class ExamSubmitRequest
{
    [Required(ErrorMessage = "กรุณากรอกชื่อ-สกุล")]
    public string ExamineeName { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณาตอบคำถามทุกข้อ")]
    [MinLength(1, ErrorMessage = "กรุณาตอบคำถามทุกข้อ")]
    public List<ExamAnswerItem> Answers { get; set; } = new();
}

public class ExamAnswerItem
{
    [Required]
    public int QuestionId { get; set; }

    [Required]
    [RegularExpression("^[ABCD]$", ErrorMessage = "คำตอบต้องเป็น A, B, C หรือ D")]
    public string SelectedChoice { get; set; } = string.Empty;
}
