namespace DashkevichStudio.Domain.Leads;

public sealed class Lead
{
    private Lead() { }

    private Lead(
        Guid id,
        string name,
        ContactMethod contactMethod,
        string contactValue,
        string service,
        string? customService,
        string description,
        string sourcePage,
        string? utmSource,
        string? utmMedium,
        string? utmCampaign,
        DateTimeOffset createdAtUtc)
    {
        Id = id;
        Name = name;
        ContactMethod = contactMethod;
        ContactValue = contactValue;
        Service = service;
        CustomService = customService;
        Description = description;
        SourcePage = sourcePage;
        UtmSource = utmSource;
        UtmMedium = utmMedium;
        UtmCampaign = utmCampaign;
        CreatedAtUtc = createdAtUtc;
        Status = LeadStatus.NotificationPending;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ContactMethod ContactMethod { get; private set; }
    public string ContactValue { get; private set; } = string.Empty;
    public string Service { get; private set; } = string.Empty;
    public string? CustomService { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string SourcePage { get; private set; } = string.Empty;
    public string? UtmSource { get; private set; }
    public string? UtmMedium { get; private set; }
    public string? UtmCampaign { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }
    public LeadStatus Status { get; private set; }
    public LeadAttachment? Attachment { get; private set; }

    public static Lead Create(
        string name,
        ContactMethod contactMethod,
        string contactValue,
        string service,
        string? customService,
        string description,
        string sourcePage,
        string? utmSource,
        string? utmMedium,
        string? utmCampaign,
        DateTimeOffset createdAtUtc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(contactValue);
        ArgumentException.ThrowIfNullOrWhiteSpace(service);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        return new Lead(
            Guid.NewGuid(),
            name.Trim(),
            contactMethod,
            contactValue.Trim(),
            service.Trim(),
            string.IsNullOrWhiteSpace(customService) ? null : customService.Trim(),
            description.Trim(),
            string.IsNullOrWhiteSpace(sourcePage) ? "/" : sourcePage.Trim(),
            NormalizeOptional(utmSource),
            NormalizeOptional(utmMedium),
            NormalizeOptional(utmCampaign),
            createdAtUtc);
    }

    public void AddAttachment(string originalName, string storedName, string contentType, long size)
    {
        Attachment = LeadAttachment.Create(Id, originalName, storedName, contentType, size);
    }

    public void MarkNotificationSent() => Status = LeadStatus.NotificationSent;

    public void MarkNotificationFailed() => Status = LeadStatus.NotificationFailed;

    private static string? NormalizeOptional(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
