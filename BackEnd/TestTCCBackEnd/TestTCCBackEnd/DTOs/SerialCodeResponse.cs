namespace TestTCCBackEnd.DTOs;

public class SerialCodeResponse
{
    public int Id { get; set; }

    /// <summary>รหัสพร้อม dash เช่น "AB123-CD456-EF789-GH012-IJ345-KL678"</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>รหัสไม่มี dash — ส่งให้ Frontend ใช้ generate QR Code</summary>
    public string CodeRaw { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
