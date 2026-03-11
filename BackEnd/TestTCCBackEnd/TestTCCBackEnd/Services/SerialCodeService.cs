using Microsoft.EntityFrameworkCore;
using TestTCCBackEnd.Data;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Models;

namespace TestTCCBackEnd.Services;

public class SerialCodeService : ISerialCodeService
{
    private readonly AppDbContext _db;

    public SerialCodeService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<SerialCodeResponse>> GetAllAsync()
    {
        var items = await _db.SerialCodes
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

        return items.Select(ToResponse);
    }

    public async Task<SerialCodeResponse> AddAsync(SerialCodeRequest request)
    {
        var code = request.Code.ToUpperInvariant();

        var duplicate = await _db.SerialCodes.AnyAsync(s => s.Code == code);
        if (duplicate)
            throw new InvalidOperationException($"รหัสสินค้า {code} มีอยู่ในระบบแล้ว");

        var entity = new SerialCode
        {
            Code      = code,
            CreatedAt = DateTime.Now
        };

        _db.SerialCodes.Add(entity);
        await _db.SaveChangesAsync();

        return ToResponse(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.SerialCodes.FindAsync(id)
            ?? throw new KeyNotFoundException($"ไม่พบรหัสสินค้า ID {id}");

        _db.SerialCodes.Remove(entity);
        await _db.SaveChangesAsync();
    }

    private static SerialCodeResponse ToResponse(SerialCode s) => new()
    {
        Id        = s.Id,
        Code      = s.Code,
        CodeRaw   = s.Code.Replace("-", ""),   // สำหรับ generate QR Code ที่ Frontend
        CreatedAt = s.CreatedAt
    };
}
