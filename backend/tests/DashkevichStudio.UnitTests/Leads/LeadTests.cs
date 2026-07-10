using DashkevichStudio.Domain.Leads;

namespace DashkevichStudio.UnitTests.Leads;

public sealed class LeadTests
{
    [Fact]
    public void Create_NormalizesInputAndSetsPendingStatus()
    {
        var now = DateTimeOffset.UtcNow;

        var lead = Lead.Create(
            "  Ян  ",
            ContactMethod.Email,
            "  hello@example.com  ",
            "  Backend  ",
            null,
            "  API для сайта  ",
            "/development/",
            null,
            null,
            null,
            now);

        Assert.Equal("Ян", lead.Name);
        Assert.Equal("hello@example.com", lead.ContactValue);
        Assert.Equal("Backend", lead.Service);
        Assert.Equal(LeadStatus.NotificationPending, lead.Status);
        Assert.Equal(now, lead.CreatedAtUtc);
    }
}
