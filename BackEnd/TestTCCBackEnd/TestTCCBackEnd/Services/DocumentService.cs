using Microsoft.EntityFrameworkCore;
using TestTCCBackEnd.Data;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Models;

namespace TestTCCBackEnd.Services;

public class DocumentService : IDocumentService
{
    private readonly AppDbContext _db;

    public DocumentService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<DocumentResponse>> GetAllAsync()
    {
        var docs = await _db.Documents.OrderByDescending(d => d.RequestedAt).ToListAsync();
        return docs.Select(ToResponse);
    }

    public async Task ApproveAsync(ApprovalRequest request)
    {
        var docs = await _db.Documents
            .Where(d => request.DocumentIds.Contains(d.Id) && d.Status == ApprovalStatus.Pending)
            .ToListAsync();

        if (!docs.Any())
            throw new InvalidOperationException("ไม่พบเอกสารที่รออนุมัติที่เลือก");

        foreach (var doc in docs)
        {
            doc.Status = ApprovalStatus.Approved;
            doc.ApprovedBy = request.ApprovedBy;
            doc.ApprovedAt = DateTime.Now;
            doc.Remark = request.Remark;
        }

        await _db.SaveChangesAsync();
    }

    public async Task RejectAsync(ApprovalRequest request)
    {
        var docs = await _db.Documents
            .Where(d => request.DocumentIds.Contains(d.Id) && d.Status == ApprovalStatus.Pending)
            .ToListAsync();

        if (!docs.Any())
            throw new InvalidOperationException("ไม่พบเอกสารที่รออนุมัติที่เลือก");

        foreach (var doc in docs)
        {
            doc.Status = ApprovalStatus.Rejected;
            doc.ApprovedBy = request.ApprovedBy;
            doc.ApprovedAt = DateTime.Now;
            doc.Remark = request.Remark;
        }

        await _db.SaveChangesAsync();
    }

    private static DocumentResponse ToResponse(Document d) => new()
    {
        Id = d.Id,
        DocumentNo = d.DocumentNo,
        Title = d.Title,
        RequestedBy = d.RequestedBy,
        RequestedAt = d.RequestedAt,
        StatusCode = (int)d.Status,
        Status = d.Status switch
        {
            ApprovalStatus.Pending  => "รออนุมัติ",
            ApprovalStatus.Approved => "อนุมัติ",
            ApprovalStatus.Rejected => "ไม่อนุมัติ",
            _ => "ไม่ทราบ"
        },
        ApprovedBy = d.ApprovedBy,
        ApprovedAt = d.ApprovedAt,
        Remark = d.Remark
    };
}
