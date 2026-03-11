namespace TestTCCBackEnd.Models;

/// <summary>
/// รหัสสินค้า Format: XXXX-XXXX-XXXX-XXXX (A-Z, 0-9 เท่านั้น)
/// เข้ากันได้กับ Barcode มาตรฐาน Code 39 ซึ่งรองรับ A-Z, 0-9 และ "-"
/// </summary>
public class ProductCode
{
    public int Id { get; set; }

    /// <summary>เก็บพร้อม dash เช่น "AB12-CD34-EF56-GH78"</summary>
    public string Code { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
