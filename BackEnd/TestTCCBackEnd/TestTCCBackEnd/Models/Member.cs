namespace TestTCCBackEnd.Models;

public enum Sex
{
    Male = 0,
    Female = 1
}

public class Member
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? ProfileBase64 { get; set; }
    public DateOnly BirthDay { get; set; }
    public int OccupationId { get; set; }
    public Occupation? Occupation { get; set; }
    public Sex Sex { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
