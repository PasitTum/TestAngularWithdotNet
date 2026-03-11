using Microsoft.EntityFrameworkCore;
using TestTCCBackEnd.Data;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Models;

namespace TestTCCBackEnd.Services;

public class ExamService : IExamService
{
    private readonly AppDbContext _db;

    public ExamService(AppDbContext db) => _db = db;

    /// <summary>ดึงโจทย์ทั้งหมด — ไม่รวมเฉลย</summary>
    public async Task<IEnumerable<ExamQuestionPublicResponse>> GetQuestionsAsync()
    {
        return await _db.ExamQuestions
            .OrderBy(q => q.QuestionNo)
            .Select(q => new ExamQuestionPublicResponse
            {
                Id           = q.Id,
                QuestionNo   = q.QuestionNo,
                QuestionText = q.QuestionText,
                ChoiceA      = q.ChoiceA,
                ChoiceB      = q.ChoiceB,
                ChoiceC      = q.ChoiceC,
                ChoiceD      = q.ChoiceD
            })
            .ToListAsync();
    }

    /// <summary>ส่งข้อสอบ: ตรวจคำตอบ, บันทึก, คืนผลลัพธ์</summary>
    public async Task<ExamResultResponse> SubmitAsync(ExamSubmitRequest request)
    {
        var questionIds = request.Answers.Select(a => a.QuestionId).ToList();
        var questions   = await _db.ExamQuestions
            .Where(q => questionIds.Contains(q.Id))
            .ToListAsync();

        if (questions.Count != request.Answers.Count)
            throw new InvalidOperationException("พบคำถามที่ไม่ถูกต้องในคำตอบที่ส่งมา");

        var session = new ExamSession
        {
            ExamineeName   = request.ExamineeName,
            TotalQuestions = questions.Count,
            TakenAt        = DateTime.Now
        };

        var resultItems = new List<ExamAnswerResultItem>();

        foreach (var ans in request.Answers)
        {
            var q         = questions.First(x => x.Id == ans.QuestionId);
            var isCorrect = ans.SelectedChoice.ToUpperInvariant() == q.CorrectChoice;

            session.Answers.Add(new ExamSessionAnswer
            {
                QuestionId      = q.Id,
                SelectedChoice  = ans.SelectedChoice.ToUpperInvariant(),
                IsCorrect       = isCorrect
            });

            if (isCorrect) session.Score++;

            resultItems.Add(new ExamAnswerResultItem
            {
                QuestionNo    = q.QuestionNo,
                QuestionText  = q.QuestionText,
                ChoiceA       = q.ChoiceA,
                ChoiceB       = q.ChoiceB,
                ChoiceC       = q.ChoiceC,
                ChoiceD       = q.ChoiceD,
                SelectedChoice = ans.SelectedChoice.ToUpperInvariant(),
                CorrectChoice  = q.CorrectChoice,
                IsCorrect      = isCorrect
            });
        }

        _db.ExamSessions.Add(session);
        await _db.SaveChangesAsync();

        return new ExamResultResponse
        {
            SessionId      = session.Id,
            ExamineeName   = session.ExamineeName,
            Score          = session.Score,
            TotalQuestions = session.TotalQuestions,
            TakenAt        = session.TakenAt,
            Results        = resultItems.OrderBy(r => r.QuestionNo).ToList()
        };
    }
}
