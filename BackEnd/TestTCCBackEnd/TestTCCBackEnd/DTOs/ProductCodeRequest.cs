using System.ComponentModel.DataAnnotations;

namespace TestTCCBackEnd.DTOs;

public class ProductCodeRequest
{
    /// <summary>
    /// รหัสสินค้า Format: XXXX-XXXX-XXXX-XXXX
    /// ตัวอักษรพิมพ์ใหญ่ A-Z และ 0-9 เท่านั้น
    /// เข้ากันได้กับ Barcode Code 39
    /// </summary>
    [Required(ErrorMessage = "กรุณากรอกรหัสสินค้า")]
    [RegularExpression(
        @"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$",
        ErrorMessage = "รหัสสินค้าต้องเป็น Format XXXX-XXXX-XXXX-XXXX (A-Z, 0-9 เท่านั้น)")]
    public string Code { get; set; } = string.Empty;
}
