namespace TestTCCBackEnd.Models;

/// <summary>
/// รหัสสินค้า Format: XXXXX-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX
/// 30 หลัก (A-Z, 0-9 เท่านั้น) เข้ากันได้กับ QR Code
/// </summary>
public class SerialCode
{
    public int Id { get; set; }

    /// <summary>เก็บพร้อม dash เช่น "AB123-CD456-EF789-GH012-IJ345-KL678"</summary>
    public string Code { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
