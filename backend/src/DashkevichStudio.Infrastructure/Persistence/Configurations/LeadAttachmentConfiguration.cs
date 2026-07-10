using DashkevichStudio.Domain.Leads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DashkevichStudio.Infrastructure.Persistence.Configurations;

public sealed class LeadAttachmentConfiguration : IEntityTypeConfiguration<LeadAttachment>
{
    public void Configure(EntityTypeBuilder<LeadAttachment> builder)
    {
        builder.ToTable("lead_attachments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.LeadId).HasColumnName("lead_id");
        builder.Property(x => x.OriginalName).HasColumnName("original_name").HasMaxLength(255).IsRequired();
        builder.Property(x => x.StoredName).HasColumnName("stored_name").HasMaxLength(255).IsRequired();
        builder.Property(x => x.ContentType).HasColumnName("content_type").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Size).HasColumnName("size");
        builder.HasIndex(x => x.LeadId).IsUnique().HasDatabaseName("ux_lead_attachments_lead_id");
    }
}
