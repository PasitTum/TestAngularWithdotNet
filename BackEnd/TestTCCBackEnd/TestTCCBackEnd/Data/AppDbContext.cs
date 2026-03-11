using Microsoft.EntityFrameworkCore;
using TestTCCBackEnd.Models;

namespace TestTCCBackEnd.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<Occupation> Occupations => Set<Occupation>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<QueueState> QueueStates => Set<QueueState>();
    public DbSet<ProductCode> ProductCodes => Set<ProductCode>();
    public DbSet<SerialCode> SerialCodes => Set<SerialCode>();
    public DbSet<ExamQuestion> ExamQuestions => Set<ExamQuestion>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<ExamSession> ExamSessions => Set<ExamSession>();
    public DbSet<ExamSessionAnswer> ExamSessionAnswers => Set<ExamSessionAnswer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ExamSession>()
            .HasMany(s => s.Answers)
            .WithOne(a => a.ExamSession)
            .HasForeignKey(a => a.ExamSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ExamSessionAnswer>()
            .HasOne(a => a.Question)
            .WithMany()
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed mock exam questions สำหรับ IT 10
        modelBuilder.Entity<ExamQuestion>().HasData(
            new ExamQuestion { Id = 1, QuestionNo = 1, QuestionText = "ข้อใดต่างจากพวก",        ChoiceA = "3",  ChoiceB = "5",  ChoiceC = "9",  ChoiceD = "11", CorrectChoice = "C", CreatedAt = new DateTime(2025, 1, 1) },
            new ExamQuestion { Id = 2, QuestionNo = 2, QuestionText = "3 × 2 = 4 หาค่า X",     ChoiceA = "1",  ChoiceB = "2",  ChoiceC = "3",  ChoiceD = "4",  CorrectChoice = "B", CreatedAt = new DateTime(2025, 1, 1) }
        );

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed mockup post
        modelBuilder.Entity<Post>().HasData(
            new Post
            {
                Id        = 1,
                Username  = "Change can",
                Content   = null,
                CreatedAt = new DateTime(2021, 10, 16, 16, 0, 0)
            },
            new Post
            {
                Id        = 2,
                Username  = "ILovetogo",
                Content   = null,
                CreatedAt = new DateTime(2021, 10, 16, 16, 0, 0)
            }
        );

        modelBuilder.Entity<Comment>().HasData(
            new Comment
            {
                Id          = 1,
                PostId      = 1,
                Username    = "Blend 285",
                CommentText = "have a good day",
                CreatedAt   = new DateTime(2021, 10, 16, 16, 5, 0)
            }
        );

        modelBuilder.Entity<ProductCode>()
            .HasIndex(p => p.Code)
            .IsUnique();

        modelBuilder.Entity<SerialCode>()
            .HasIndex(s => s.Code)
            .IsUnique();

        modelBuilder.Entity<Member>()
            .HasOne(m => m.Occupation)
            .WithMany()
            .HasForeignKey(m => m.OccupationId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed QueueState — 1 row เป็น state ของระบบ, CurrentIndex = -1 = ยังไม่มีคิว
        modelBuilder.Entity<QueueState>().HasData(
            new QueueState { Id = 1, CurrentIndex = -1, LastUpdated = new DateTime(2025, 1, 1) }
        );

        modelBuilder.Entity<Occupation>().HasData(
            new Occupation { Id = 1,  Name = "วิศวกรซอฟต์แวร์" },
            new Occupation { Id = 2,  Name = "นักออกแบบ UX/UI" },
            new Occupation { Id = 3,  Name = "นักวิเคราะห์ระบบ" },
            new Occupation { Id = 4,  Name = "ผู้จัดการโครงการ" },
            new Occupation { Id = 5,  Name = "นักบัญชี" },
            new Occupation { Id = 6,  Name = "เจ้าหน้าที่ทรัพยากรบุคคล" },
            new Occupation { Id = 7,  Name = "นักการตลาด" },
            new Occupation { Id = 8,  Name = "นักกฎหมาย" },
            new Occupation { Id = 9,  Name = "แพทย์" },
            new Occupation { Id = 10, Name = "พยาบาล" }
        );

        modelBuilder.Entity<Document>().HasData(
            new Document { Id = 1,  DocumentNo = "DOC-2025-001", Title = "ขอจัดซื้อคอมพิวเตอร์",         RequestedBy = "สมชาย ใจดี",    RequestedAt = new DateTime(2025, 3, 1), Status = ApprovalStatus.Pending },
            new Document { Id = 2,  DocumentNo = "DOC-2025-002", Title = "ขอจัดซื้อเครื่องพิมพ์",         RequestedBy = "สมหญิง รักดี",  RequestedAt = new DateTime(2025, 3, 2), Status = ApprovalStatus.Approved, ApprovedBy = "ผู้จัดการ", ApprovedAt = new DateTime(2025, 3, 3), Remark = "อนุมัติตามที่ขอ" },
            new Document { Id = 3,  DocumentNo = "DOC-2025-003", Title = "ขออนุมัติเดินทางต่างจังหวัด",    RequestedBy = "ประเสริฐ มั่นคง", RequestedAt = new DateTime(2025, 3, 3), Status = ApprovalStatus.Rejected, ApprovedBy = "ผู้จัดการ", ApprovedAt = new DateTime(2025, 3, 4), Remark = "งบประมาณไม่เพียงพอ" },
            new Document { Id = 4,  DocumentNo = "DOC-2025-004", Title = "ขอจัดซื้อโต๊ะทำงาน",            RequestedBy = "มานี มีสุข",    RequestedAt = new DateTime(2025, 3, 4), Status = ApprovalStatus.Pending },
            new Document { Id = 5,  DocumentNo = "DOC-2025-005", Title = "ขออนุมัติค่าล่วงเวลา",          RequestedBy = "วิชัย ขยันดี",  RequestedAt = new DateTime(2025, 3, 5), Status = ApprovalStatus.Approved, ApprovedBy = "ผู้จัดการ", ApprovedAt = new DateTime(2025, 3, 6), Remark = "อนุมัติ OT เดือนมีนาคม" },
            new Document { Id = 6,  DocumentNo = "DOC-2025-006", Title = "ขอจัดซื้อกล้องวงจรปิด",         RequestedBy = "สุดา สวยงาม",   RequestedAt = new DateTime(2025, 3, 6), Status = ApprovalStatus.Pending },
            new Document { Id = 7,  DocumentNo = "DOC-2025-007", Title = "ขออนุมัติใช้รถยนต์ราชการ",      RequestedBy = "อนันต์ ตั้งใจ", RequestedAt = new DateTime(2025, 3, 7), Status = ApprovalStatus.Rejected, ApprovedBy = "ผู้จัดการ", ApprovedAt = new DateTime(2025, 3, 8), Remark = "รถไม่ว่าง" },
            new Document { Id = 8,  DocumentNo = "DOC-2025-008", Title = "ขอจัดซื้อวัสดุสำนักงาน",        RequestedBy = "จิตรา แจ่มใส",  RequestedAt = new DateTime(2025, 3, 8), Status = ApprovalStatus.Pending },
            new Document { Id = 9,  DocumentNo = "DOC-2025-009", Title = "ขออนุมัติฝึกอบรมพนักงาน",       RequestedBy = "ธนา มั่งมี",    RequestedAt = new DateTime(2025, 3, 9), Status = ApprovalStatus.Approved, ApprovedBy = "ผู้จัดการ", ApprovedAt = new DateTime(2025, 3, 10), Remark = "อนุมัติ 5 คน" },
            new Document { Id = 10, DocumentNo = "DOC-2025-010", Title = "ขอจัดซื้อซอฟต์แวร์",            RequestedBy = "ปัญญา เก่งกาจ", RequestedAt = new DateTime(2025, 3, 10), Status = ApprovalStatus.Pending }
        );
    }
}
