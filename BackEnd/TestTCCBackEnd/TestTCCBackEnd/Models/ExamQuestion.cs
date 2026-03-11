namespace TestTCCBackEnd.Models;

public class ExamQuestion
{
    public int Id { get; set; }

    /// <summary>ลำดับข้อสอบ — Running Number (re-calculate เมื่อลบ)</summary>
    public int QuestionNo { get; set; }

    public string QuestionText { get; set; } = string.Empty;

    public string ChoiceA { get; set; } = string.Empty;
    public string ChoiceB { get; set; } = string.Empty;
    public string ChoiceC { get; set; } = string.Empty;
    public string ChoiceD { get; set; } = string.Empty;

    /// <summary>เฉลย: "A", "B", "C" หรือ "D"</summary>
    public string CorrectChoice { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
