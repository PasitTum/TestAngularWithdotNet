namespace TestTCCBackEnd.Models;

/// <summary>
/// เก็บสถานะคิวปัจจุบัน (1 row ต่อระบบ)
/// CurrentIndex: -1 = รีเซ็ต (แสดง "00"), 0-259 = A0–Z9
/// letter = index / 10 → 0=A, 1=B, ... 25=Z
/// number = index % 10 → 0–9
/// </summary>
public class QueueState
{
    public int Id { get; set; }
    public int CurrentIndex { get; set; } = -1;
    public DateTime LastUpdated { get; set; } = DateTime.Now;
}
