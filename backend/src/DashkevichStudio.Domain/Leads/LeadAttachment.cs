namespace DashkevichStudio.Domain.Leads;

public sealed class LeadAttachment
{
    private LeadAttachment() { }

    public Guid Id { get; private set; }
    public Guid LeadId { get; private set; }
    public string OriginalName { get; private set; } = string.Empty;
    public string StoredName { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long Size { get; private set; }

    internal static LeadAttachment Create(
        Guid leadId,
        string originalName,
        string storedName,
        string contentType,
        long size) => new()
    {
        Id = Guid.NewGuid(),
        LeadId = leadId,
        OriginalName = originalName,
        StoredName = storedName,
        ContentType = contentType,
        Size = size
    };
}
