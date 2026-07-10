using DashkevichStudio.Domain.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DashkevichStudio.Infrastructure.Persistence.Configurations;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.LeadId).HasColumnName("lead_id");
        builder.Property(x => x.CreatedAtUtc).HasColumnName("created_at_utc");
        builder.Property(x => x.NextAttemptAtUtc).HasColumnName("next_attempt_at_utc");
        builder.Property(x => x.SentAtUtc).HasColumnName("sent_at_utc");
        builder.Property(x => x.AttemptCount).HasColumnName("attempt_count");
        builder.Property(x => x.LastError).HasColumnName("last_error").HasMaxLength(1000);
        builder.HasIndex(x => new { x.SentAtUtc, x.NextAttemptAtUtc })
            .HasDatabaseName("ix_outbox_pending");
    }
}
