namespace DashkevichStudio.Domain.Notifications;

public sealed class OutboxMessage
{
    private OutboxMessage() { }

    private OutboxMessage(Guid leadId, DateTimeOffset createdAtUtc)
    {
        Id = Guid.NewGuid();
        LeadId = leadId;
        CreatedAtUtc = createdAtUtc;
        NextAttemptAtUtc = createdAtUtc;
    }

    public Guid Id { get; private set; }
    public Guid LeadId { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }
    public DateTimeOffset NextAttemptAtUtc { get; private set; }
    public DateTimeOffset? SentAtUtc { get; private set; }
    public int AttemptCount { get; private set; }
    public string? LastError { get; private set; }

    public static OutboxMessage ForLead(Guid leadId, DateTimeOffset createdAtUtc) =>
        new(leadId, createdAtUtc);

    public void MarkSent(DateTimeOffset sentAtUtc)
    {
        SentAtUtc = sentAtUtc;
        LastError = null;
    }

    public void MarkFailed(string error, DateTimeOffset now)
    {
        AttemptCount++;
        LastError = error.Length > 1000 ? error[..1000] : error;
        NextAttemptAtUtc = now.AddMinutes(Math.Min(Math.Pow(2, AttemptCount), 60));
    }
}
