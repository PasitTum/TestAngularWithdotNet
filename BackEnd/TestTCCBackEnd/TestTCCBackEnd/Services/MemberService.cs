using System.Globalization;
using Microsoft.EntityFrameworkCore;
using TestTCCBackEnd.Data;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Models;

namespace TestTCCBackEnd.Services;

public class MemberService : IMemberService
{
    private readonly AppDbContext _db;

    public MemberService(AppDbContext db) => _db = db;

    public async Task<MemberResponse> CreateAsync(MemberRequest request)
    {
        var occupation = await _db.Occupations.FindAsync(request.OccupationId)
            ?? throw new InvalidOperationException("ไม่พบข้อมูลอาชีพที่เลือก");

        // Parse BirthDay from dd/MM/yyyy
        if (!DateOnly.TryParseExact(request.BirthDay, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthDay))
        {
            throw new InvalidOperationException("รูปแบบวันเกิดไม่ถูกต้อง (ตัวอย่าง: 01/03/2000)");
        }

        var member = new Member
        {
            FirstName    = request.FirstName,
            LastName     = request.LastName,
            Email        = request.Email,
            Phone        = request.Phone,
            ProfileBase64 = request.ProfileBase64,
            BirthDay     = birthDay,
            OccupationId = request.OccupationId,
            Sex          = request.Sex,
            CreatedAt    = DateTime.Now
        };

        _db.Members.Add(member);
        await _db.SaveChangesAsync();

        return new MemberResponse
        {
            Id             = member.Id,
            FirstName      = member.FirstName,
            LastName       = member.LastName,
            Email          = member.Email,
            Phone          = member.Phone,
            ProfileBase64  = member.ProfileBase64,
            BirthDay       = member.BirthDay.ToString("dd/MM/yyyy"),
            OccupationId   = member.OccupationId,
            OccupationName = occupation.Name,
            Sex            = member.Sex == Sex.Male ? "ชาย" : "หญิง",
            CreatedAt      = member.CreatedAt
        };
    }

    public async Task<IEnumerable<OccupationResponse>> GetOccupationsAsync()
    {
        return await _db.Occupations
            .OrderBy(o => o.Id)
            .Select(o => new OccupationResponse { Id = o.Id, Name = o.Name })
            .ToListAsync();
    }
}
