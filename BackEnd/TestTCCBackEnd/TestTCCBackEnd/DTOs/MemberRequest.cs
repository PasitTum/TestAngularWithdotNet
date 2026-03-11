using System.ComponentModel.DataAnnotations;
using TestTCCBackEnd.Models;

namespace TestTCCBackEnd.DTOs;

public class MemberRequest
{
    [Required(ErrorMessage = "กรุณากรอกชื่อ")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณากรอกนามสกุล")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณากรอกอีเมล")]
    [EmailAddress(ErrorMessage = "รูปแบบอีเมลไม่ถูกต้อง")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณากรอกเบอร์โทรศัพท์")]
    [RegularExpression(@"^0[0-9]{8,9}$", ErrorMessage = "รูปแบบเบอร์โทรศัพท์ไม่ถูกต้อง (ตัวอย่าง: 0812345678)")]
    public string Phone { get; set; } = string.Empty;

    /// <summary>Base64 string ของรูปภาพ</summary>
    [Required(ErrorMessage = "กรุณาอัพโหลดรูปโปรไฟล์")]
    public string ProfileBase64 { get; set; } = string.Empty;

    /// <summary>วัน/เดือน/ปี เช่น 15/03/2000</summary>
    [Required(ErrorMessage = "กรุณากรอกวันเกิด")]
    [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$",
        ErrorMessage = "รูปแบบวันเกิดไม่ถูกต้อง (ตัวอย่าง: 01/03/2000)")]
    public string BirthDay { get; set; } = string.Empty;

    [Required(ErrorMessage = "กรุณาเลือกอาชีพ")]
    [Range(1, int.MaxValue, ErrorMessage = "กรุณาเลือกอาชีพ")]
    public int OccupationId { get; set; }

    [Required(ErrorMessage = "กรุณาเลือกเพศ")]
    public Sex Sex { get; set; }
}
