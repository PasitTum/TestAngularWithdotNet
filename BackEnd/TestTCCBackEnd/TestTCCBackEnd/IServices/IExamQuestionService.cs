using TestTCCBackEnd.DTOs;

namespace TestTCCBackEnd.Services;

public interface IExamQuestionService
{
    Task<IEnumerable<ExamQuestionResponse>> GetAllAsync();
    Task<ExamQuestionResponse> AddAsync(ExamQuestionRequest request);
    Task DeleteAsync(int id);
}
