using DashkevichStudio.Application.Abstractions;
using DashkevichStudio.Domain.Leads;
using Microsoft.EntityFrameworkCore;

namespace DashkevichStudio.Infrastructure.Persistence;

public sealed class LeadRepository(StudioDbContext dbContext) : ILeadRepository
{
    public Task AddAsync(Lead lead, CancellationToken cancellationToken) =>
        dbContext.Leads.AddAsync(lead, cancellationToken).AsTask();

    public Task<Lead?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        dbContext.Leads
            .Include(x => x.Attachment)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
}
