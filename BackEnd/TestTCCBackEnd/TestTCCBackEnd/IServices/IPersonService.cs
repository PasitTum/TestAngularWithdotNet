using TestTCCBackEnd.DTOs;

namespace TestTCCBackEnd.Services;

public interface IPersonService
{
    Task<IEnumerable<PersonResponse>> GetAllAsync();
    Task<PersonResponse?> GetByIdAsync(int id);
    Task<PersonResponse> CreateAsync(PersonRequest request);
}
