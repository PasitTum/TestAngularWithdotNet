namespace TestTCCBackEnd.Models;

/// <summary>บันทึกการสอบแต่ละครั้ง</summary>
public class ExamSession
{
    public int Id { get; set; }
    public string ExamineeName { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public DateTime TakenAt { get; set; } = DateTime.Now;

    public ICollection<ExamSessionAnswer> Answers { get; set; } = new List<ExamSessionAnswer>();
}
