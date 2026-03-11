using TestTCCBackEnd.Models;

namespace TestTCCBackEnd.DTOs;

public class DocumentResponse
{
    public int Id { get; set; }
    public string DocumentNo { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string RequestedBy { get; set; } = string.Empty;
    public DateTime RequestedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? Remark { get; set; }
}
