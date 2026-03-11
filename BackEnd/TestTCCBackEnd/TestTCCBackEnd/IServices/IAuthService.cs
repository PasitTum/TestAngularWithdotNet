using TestTCCBackEnd.DTOs;

namespace TestTCCBackEnd.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}
