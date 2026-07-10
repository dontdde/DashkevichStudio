using DashkevichStudio.Domain.Leads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DashkevichStudio.Infrastructure.Persistence.Configurations;

public sealed class LeadConfiguration : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.ToTable("leads");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(x => x.ContactMethod).HasColumnName("contact_method").HasConversion<string>().HasMaxLength(30);
        builder.Property(x => x.ContactValue).HasColumnName("contact_value").HasMaxLength(200).IsRequired();
        builder.Property(x => x.Service).HasColumnName("service").HasMaxLength(150).IsRequired();
        builder.Property(x => x.CustomService).HasColumnName("custom_service").HasMaxLength(200);
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(3000).IsRequired();
        builder.Property(x => x.SourcePage).HasColumnName("source_page").HasMaxLength(500);
        builder.Property(x => x.UtmSource).HasColumnName("utm_source").HasMaxLength(200);
        builder.Property(x => x.UtmMedium).HasColumnName("utm_medium").HasMaxLength(200);
        builder.Property(x => x.UtmCampaign).HasColumnName("utm_campaign").HasMaxLength(200);
        builder.Property(x => x.CreatedAtUtc).HasColumnName("created_at_utc");
        builder.Property(x => x.Status).HasColumnName("status").HasConversion<string>().HasMaxLength(40);

        builder.HasOne(x => x.Attachment)
            .WithOne()
            .HasForeignKey<LeadAttachment>(x => x.LeadId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CreatedAtUtc).HasDatabaseName("ix_leads_created_at_utc");
    }
}
