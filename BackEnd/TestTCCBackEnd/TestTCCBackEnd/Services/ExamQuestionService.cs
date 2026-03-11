using Microsoft.EntityFrameworkCore;
using TestTCCBackEnd.Data;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Models;

namespace TestTCCBackEnd.Services;

public class ExamQuestionService : IExamQuestionService
{
    private readonly AppDbContext _db;

    public ExamQuestionService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<ExamQuestionResponse>> GetAllAsync()
    {
        var items = await _db.ExamQuestions
            .OrderBy(q => q.QuestionNo)
            .ToListAsync();

        return items.Select(ToResponse);
    }

    public async Task<ExamQuestionResponse> AddAsync(ExamQuestionRequest request)
    {
        // QuestionNo = จำนวนข้อที่มีอยู่ + 1
        var nextNo = await _db.ExamQuestions.CountAsync() + 1;

        var entity = new ExamQuestion
        {
            QuestionNo    = nextNo,
            QuestionText  = request.QuestionText,
            ChoiceA       = request.ChoiceA,
            ChoiceB       = request.ChoiceB,
            ChoiceC       = request.ChoiceC,
            ChoiceD       = request.ChoiceD,
            CorrectChoice = request.CorrectChoice.ToUpperInvariant(),
            CreatedAt     = DateTime.Now
        };

        _db.ExamQuestions.Add(entity);
        await _db.SaveChangesAsync();

        return ToResponse(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.ExamQuestions.FindAsync(id)
            ?? throw new KeyNotFoundException($"ไม่พบข้อสอบ ID {id}");

        var deletedNo = entity.QuestionNo;

        _db.ExamQuestions.Remove(entity);
        await _db.SaveChangesAsync();

        // Re-number: ข้อที่มี QuestionNo > deletedNo ให้ลดลง 1
        var affected = await _db.ExamQuestions
            .Where(q => q.QuestionNo > deletedNo)
            .ToListAsync();

        foreach (var q in affected)
            q.QuestionNo -= 1;

        if (affected.Any())
            await _db.SaveChangesAsync();
    }

    private static ExamQuestionResponse ToResponse(ExamQuestion q) => new()
    {
        Id            = q.Id,
        QuestionNo    = q.QuestionNo,
        QuestionText  = q.QuestionText,
        ChoiceA       = q.ChoiceA,
        ChoiceB       = q.ChoiceB,
        ChoiceC       = q.ChoiceC,
        ChoiceD       = q.ChoiceD,
        CorrectChoice = q.CorrectChoice,
        CreatedAt     = q.CreatedAt
    };
}
