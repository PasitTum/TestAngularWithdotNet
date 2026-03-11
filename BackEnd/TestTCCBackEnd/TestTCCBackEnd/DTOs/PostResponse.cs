namespace TestTCCBackEnd.DTOs;

public class PostResponse
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? ImageBase64 { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<CommentResponse> Comments { get; set; } = new();
}
