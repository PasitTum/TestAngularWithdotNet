namespace TestTCCBackEnd.Models;

/// <summary>คำตอบแต่ละข้อในการสอบครั้งนั้น</summary>
public class ExamSessionAnswer
{
    public int Id { get; set; }
    public int ExamSessionId { get; set; }
    public ExamSession? ExamSession { get; set; }
    public int QuestionId { get; set; }
    public ExamQuestion? Question { get; set; }

    /// <summary>คำตอบที่ผู้สอบเลือก: "A", "B", "C" หรือ "D"</summary>
    public string SelectedChoice { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
