namespace TestTCCBackEnd.Models;

public class Post
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;

    /// <summary>รูปภาพเก็บเป็น Base64</summary>
    public string? ImageBase64 { get; set; }

    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
