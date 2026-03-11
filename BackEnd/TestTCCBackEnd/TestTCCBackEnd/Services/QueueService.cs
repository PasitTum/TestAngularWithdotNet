using Microsoft.EntityFrameworkCore;
using TestTCCBackEnd.Data;
using TestTCCBackEnd.DTOs;

namespace TestTCCBackEnd.Services;

public class QueueService : IQueueService
{
    // ป้องกันการกดพร้อมกัน — SemaphoreSlim(1,1) = mutex
    private static readonly SemaphoreSlim _lock = new(1, 1);

    private const int MaxIndex = 259; // Z9 = index 259

    private readonly IServiceScopeFactory _scopeFactory;

    // ใช้ IServiceScopeFactory เพราะ QueueService register เป็น Singleton
    // แต่ AppDbContext เป็น Scoped
    public QueueService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task<QueueResponse> GetCurrentAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var state = await db.QueueStates.FirstAsync(q => q.Id == 1);
        return ToResponse(state);
    }

    public async Task<QueueResponse> TakeTicketAsync()
    {
        await _lock.WaitAsync();
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var state = await db.QueueStates.FirstAsync(q => q.Id == 1);

            if (state.CurrentIndex >= MaxIndex)
                throw new InvalidOperationException("คิวเต็มแล้ว (Z9) กรุณาล้างคิวก่อน");

            state.CurrentIndex += 1;
            state.LastUpdated = DateTime.Now;
            await db.SaveChangesAsync();

            return ToResponse(state);
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task<QueueResponse> ResetAsync()
    {
        await _lock.WaitAsync();
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var state = await db.QueueStates.FirstAsync(q => q.Id == 1);
            state.CurrentIndex = -1;
            state.LastUpdated = DateTime.Now;
            await db.SaveChangesAsync();

            return ToResponse(state);
        }
        finally
        {
            _lock.Release();
        }
    }

    // ---- helpers ----

    private static QueueResponse ToResponse(Models.QueueState state) => new()
    {
        QueueNumber  = IndexToQueueNumber(state.CurrentIndex),
        CurrentIndex = state.CurrentIndex,
        IsMaxReached = state.CurrentIndex >= MaxIndex,
        LastUpdated  = state.LastUpdated
    };

    /// <summary>
    /// แปลง index → หมายเลขคิว
    /// -1  → "00" (ยังไม่มี / รีเซ็ต)
    ///  0  → "A0"
    ///  9  → "A9"
    /// 10  → "B0"
    /// 259 → "Z9"
    /// </summary>
    public static string IndexToQueueNumber(int index)
    {
        if (index < 0) return "00";
        char letter = (char)('A' + index / 10);
        int  number = index % 10;
        return $"{letter}{number}";
    }
}
