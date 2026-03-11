using Microsoft.EntityFrameworkCore;
using TestTCCBackEnd.Data;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Models;

namespace TestTCCBackEnd.Services;

public class PersonService : IPersonService
{
    private readonly AppDbContext _db;

    public PersonService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<PersonResponse>> GetAllAsync()
    {
        var persons = await _db.Persons.ToListAsync();
        return persons.Select(ToResponse);
    }

    public async Task<PersonResponse?> GetByIdAsync(int id)
    {
        var person = await _db.Persons.FindAsync(id);
        return person is null ? null : ToResponse(person);
    }

    public async Task<PersonResponse> CreateAsync(PersonRequest request)
    {
        var person = new Person
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            Address = request.Address
        };

        _db.Persons.Add(person);
        await _db.SaveChangesAsync();

        return ToResponse(person);
    }

    // คำนวณอายุ: ปีปัจจุบัน - ปีเกิด
    private static int CalculateAge(DateOnly birthDate) =>
        DateTime.Today.Year - birthDate.Year;

    private static PersonResponse ToResponse(Person p) => new()
    {
        Id = p.Id,
        FirstName = p.FirstName,
        LastName = p.LastName,
        FullName = $"{p.FirstName} {p.LastName}",
        BirthDate = p.BirthDate.ToString("dd/MM/yyyy"),
        Age = CalculateAge(p.BirthDate),
        Address = p.Address
    };
}
