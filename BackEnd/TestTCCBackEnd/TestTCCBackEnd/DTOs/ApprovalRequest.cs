namespace TestTCCBackEnd.DTOs;

public class ApprovalRequest
{
    public List<int> DocumentIds { get; set; } = new();
    public string Remark { get; set; } = string.Empty;
    public string ApprovedBy { get; set; } = string.Empty;
}
