using TestTCCBackEnd.DTOs;

namespace TestTCCBackEnd.Services;

public interface IQueueService
{
    Task<QueueResponse> GetCurrentAsync();
    Task<QueueResponse> TakeTicketAsync();
    Task<QueueResponse> ResetAsync();
}
