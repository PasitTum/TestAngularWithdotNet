namespace TestTCCBackEnd.Models;

public enum ApprovalStatus
{
    Pending = 0,   // รออนุมัติ
    Approved = 1,  // อนุมัติ
    Rejected = 2   // ไม่อนุมัติ
}

public class Document
{
    public int Id { get; set; }
    public string DocumentNo { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string RequestedBy { get; set; } = string.Empty;
    public DateTime RequestedAt { get; set; }
    public ApprovalStatus Status { get; set; } = ApprovalStatus.Pending;
    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? Remark { get; set; }
}
