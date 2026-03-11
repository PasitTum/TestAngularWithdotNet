namespace TestTCCBackEnd.DTOs;

public class ProductCodeResponse
{
    public int Id { get; set; }

    /// <summary>รหัสพร้อม dash เช่น "AB12-CD34-EF56-GH78"</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>รหัสไม่มี dash สำหรับ render Barcode Code 39</summary>
    public string CodeRaw { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
