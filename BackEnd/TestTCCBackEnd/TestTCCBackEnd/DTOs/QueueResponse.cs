namespace TestTCCBackEnd.DTOs;

public class QueueResponse
{
    /// <summary>หมายเลขคิว เช่น "A5", "B0", หรือ "00" เมื่อรีเซ็ต</summary>
    public string QueueNumber { get; set; } = string.Empty;

    /// <summary>index ปัจจุบัน (0–259) หรือ -1 เมื่อรีเซ็ต</summary>
    public int CurrentIndex { get; set; }

    /// <summary>true = คิวหมดแล้ว (Z9) และต้องล้างคิวก่อน</summary>
    public bool IsMaxReached { get; set; }

    public DateTime LastUpdated { get; set; }
}
