using DashkevichStudio.Application.Abstractions;
using DashkevichStudio.Domain.Leads;
using DashkevichStudio.Domain.Notifications;
using Microsoft.EntityFrameworkCore;

namespace DashkevichStudio.Infrastructure.Persistence;

public sealed class StudioDbContext(DbContextOptions<StudioDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Lead> Leads => Set<Lead>();
    public DbSet<LeadAttachment> Attachments => Set<LeadAttachment>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudioDbContext).Assembly);
    }
}
