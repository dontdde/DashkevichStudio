using DashkevichStudio.Domain.Leads;

namespace DashkevichStudio.Application.Leads;

public sealed record CreateLeadCommand(
    string Name,
    ContactMethod ContactMethod,
    string ContactValue,
    string Service,
    string? CustomService,
    string Description,
    string SourcePage,
    string? UtmSource,
    string? UtmMedium,
    string? UtmCampaign,
    LeadFile? File);

public sealed record LeadFile(
    Stream Content,
    string FileName,
    string ContentType,
    long Length);
