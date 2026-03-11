using TestTCCBackEnd.DTOs;

namespace TestTCCBackEnd.Services;

public interface IProductCodeService
{
    Task<IEnumerable<ProductCodeResponse>> GetAllAsync();
    Task<ProductCodeResponse> AddAsync(ProductCodeRequest request);
    Task DeleteAsync(int id);
}
