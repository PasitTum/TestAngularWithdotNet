namespace TestTCCBackEnd.DTOs;

public class ExamQuestionResponse
{
    public int Id { get; set; }
    public int QuestionNo { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string ChoiceA { get; set; } = string.Empty;
    public string ChoiceB { get; set; } = string.Empty;
    public string ChoiceC { get; set; } = string.Empty;
    public string ChoiceD { get; set; } = string.Empty;
    public string CorrectChoice { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
