using TestTCCBackEnd.DTOs;

namespace TestTCCBackEnd.Services;

public interface IExamService
{
    Task<IEnumerable<ExamQuestionPublicResponse>> GetQuestionsAsync();
    Task<ExamResultResponse> SubmitAsync(ExamSubmitRequest request);
}
