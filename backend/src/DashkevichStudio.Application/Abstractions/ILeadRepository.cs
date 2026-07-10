using DashkevichStudio.Domain.Leads;

namespace DashkevichStudio.Application.Abstractions;

public interface ILeadRepository
{
    Task AddAsync(Lead lead, CancellationToken cancellationToken);
    Task<Lead?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
