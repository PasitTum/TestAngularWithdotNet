using TestTCCBackEnd.DTOs;

namespace TestTCCBackEnd.Services;

public interface IDocumentService
{
    Task<IEnumerable<DocumentResponse>> GetAllAsync();
    Task ApproveAsync(ApprovalRequest request);
    Task RejectAsync(ApprovalRequest request);
}
