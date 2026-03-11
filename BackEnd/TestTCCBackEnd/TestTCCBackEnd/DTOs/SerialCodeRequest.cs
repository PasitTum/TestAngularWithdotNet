using System.ComponentModel.DataAnnotations;

namespace TestTCCBackEnd.DTOs;

public class SerialCodeRequest
{
    /// <summary>
    /// รหัสสินค้า Format: XXXXX-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX
    /// 30 หลัก (A-Z, 0-9 เท่านั้น) ห้ามซ้ำ
    /// </summary>
    [Required(ErrorMessage = "กรุณากรอกรหัสสินค้า")]
    [RegularExpression(
        @"^[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}$",
        ErrorMessage = "รหัสสินค้าต้องเป็น Format XXXXX-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX (A-Z, 0-9 เท่านั้น)")]
    public string Code { get; set; } = string.Empty;
}
