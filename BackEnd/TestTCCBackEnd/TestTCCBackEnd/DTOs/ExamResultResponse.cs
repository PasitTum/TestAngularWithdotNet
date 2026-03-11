namespace TestTCCBackEnd.DTOs;

public class ExamResultResponse
{
    public int SessionId { get; set; }
    public string ExamineeName { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalQuestions { get; set; }

    /// <summary>เช่น "1/2"</summary>
    public string ScoreText => $"{Score}/{TotalQuestions}";

    public DateTime TakenAt { get; set; }
    public List<ExamAnswerResultItem> Results { get; set; } = new();
}

public class ExamAnswerResultItem
{
    public int QuestionNo { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string ChoiceA { get; set; } = string.Empty;
    public string ChoiceB { get; set; } = string.Empty;
    public string ChoiceC { get; set; } = string.Empty;
    public string ChoiceD { get; set; } = string.Empty;
    public string SelectedChoice { get; set; } = string.Empty;
    public string CorrectChoice { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
