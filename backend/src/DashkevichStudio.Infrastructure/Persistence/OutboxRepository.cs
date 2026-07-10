using DashkevichStudio.Application.Abstractions;
using DashkevichStudio.Domain.Notifications;
using Microsoft.EntityFrameworkCore;

namespace DashkevichStudio.Infrastructure.Persistence;

public sealed class OutboxRepository(StudioDbContext dbContext) : IOutboxRepository
{
    public Task AddAsync(OutboxMessage message, CancellationToken cancellationToken) =>
        dbContext.OutboxMessages.AddAsync(message, cancellationToken).AsTask();

    public async Task<IReadOnlyList<OutboxMessage>> GetPendingAsync(
        int batchSize,
        DateTimeOffset now,
        CancellationToken cancellationToken) =>
        await dbContext.OutboxMessages
            .Where(x => x.SentAtUtc == null && x.NextAttemptAtUtc <= now)
            .OrderBy(x => x.CreatedAtUtc)
            .Take(batchSize)
            .ToListAsync(cancellationToken);
}
