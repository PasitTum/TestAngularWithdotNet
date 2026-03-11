using Microsoft.EntityFrameworkCore;
using TestTCCBackEnd.Data;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Models;

namespace TestTCCBackEnd.Services;

public class ProductCodeService : IProductCodeService
{
    private readonly AppDbContext _db;

    public ProductCodeService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<ProductCodeResponse>> GetAllAsync()
    {
        var items = await _db.ProductCodes
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return items.Select(ToResponse);
    }

    public async Task<ProductCodeResponse> AddAsync(ProductCodeRequest request)
    {
        // ตรวจสอบรหัสซ้ำ
        var duplicate = await _db.ProductCodes
            .AnyAsync(p => p.Code == request.Code);

        if (duplicate)
            throw new InvalidOperationException($"รหัสสินค้า {request.Code} มีอยู่ในระบบแล้ว");

        var entity = new ProductCode
        {
            Code      = request.Code.ToUpperInvariant(),
            CreatedAt = DateTime.Now
        };

        _db.ProductCodes.Add(entity);
        await _db.SaveChangesAsync();

        return ToResponse(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.ProductCodes.FindAsync(id)
            ?? throw new KeyNotFoundException($"ไม่พบรหัสสินค้า ID {id}");

        _db.ProductCodes.Remove(entity);
        await _db.SaveChangesAsync();
    }

    private static ProductCodeResponse ToResponse(ProductCode p) => new()
    {
        Id        = p.Id,
        Code      = p.Code,
        CodeRaw   = p.Code.Replace("-", ""),   // สำหรับ render Barcode Code 39
        CreatedAt = p.CreatedAt
    };
}
