using DashkevichStudio.Domain.Leads;

namespace DashkevichStudio.Application.Abstractions;

public interface ITelegramNotifier
{
    Task SendAsync(Lead lead, CancellationToken cancellationToken);
}
