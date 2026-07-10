using DashkevichStudio.Domain.Notifications;

namespace DashkevichStudio.Application.Abstractions;

public interface IOutboxRepository
{
    Task AddAsync(OutboxMessage message, CancellationToken cancellationToken);
    Task<IReadOnlyList<OutboxMessage>> GetPendingAsync(int batchSize, DateTimeOffset now, CancellationToken cancellationToken);
}
