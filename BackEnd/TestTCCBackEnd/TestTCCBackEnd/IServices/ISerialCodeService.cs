using TestTCCBackEnd.DTOs;

namespace TestTCCBackEnd.Services;

public interface ISerialCodeService
{
    Task<IEnumerable<SerialCodeResponse>> GetAllAsync();
    Task<SerialCodeResponse> AddAsync(SerialCodeRequest request);
    Task DeleteAsync(int id);
}
