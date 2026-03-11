namespace TestTCCBackEnd.DTOs;

/// <summary>ส่งให้ผู้สอบ — ไม่รวมเฉลย</summary>
public class ExamQuestionPublicResponse
{
    public int Id { get; set; }
    public int QuestionNo { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string ChoiceA { get; set; } = string.Empty;
    public string ChoiceB { get; set; } = string.Empty;
    public string ChoiceC { get; set; } = string.Empty;
    public string ChoiceD { get; set; } = string.Empty;
}
