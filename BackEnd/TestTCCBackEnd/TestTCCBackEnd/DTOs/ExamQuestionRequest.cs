using System.ComponentModel.DataAnnotations;

namespace TestTCCBackEnd.DTOs;

public class ExamQuestionRequest
{
    [Required(ErrorMessage = "กรุณากรอกข้อความคำถาม")]
    public string QuestionText { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณากรอกตัวเลือก A")]
    public string ChoiceA { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณากรอกตัวเลือก B")]
    public string ChoiceB { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณากรอกตัวเลือก C")]
    public string ChoiceC { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณากรอกตัวเลือก D")]
    public string ChoiceD { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณาเลือกเฉลย")]
    [RegularExpression("^[ABCD]$", ErrorMessage = "เฉลยต้องเป็น A, B, C หรือ D เท่านั้น")]
    public string CorrectChoice { get; set; } = string.Empty;
}
