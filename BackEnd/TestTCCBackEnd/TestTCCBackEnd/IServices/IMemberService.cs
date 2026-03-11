using TestTCCBackEnd.DTOs;

namespace TestTCCBackEnd.Services;

public interface IMemberService
{
    Task<MemberResponse> CreateAsync(MemberRequest request);
    Task<IEnumerable<OccupationResponse>> GetOccupationsAsync();
}
