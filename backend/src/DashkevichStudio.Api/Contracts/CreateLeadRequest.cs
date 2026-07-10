using Microsoft.AspNetCore.Mvc;

namespace DashkevichStudio.Api.Contracts;

public sealed class CreateLeadRequest
{
    [FromForm(Name = "name")]
    public string Name { get; init; } = string.Empty;

    [FromForm(Name = "contact_method")]
    public string ContactMethod { get; init; } = string.Empty;

    [FromForm(Name = "contact_value")]
    public string ContactValue { get; init; } = string.Empty;

    [FromForm(Name = "service")]
    public string Service { get; init; } = string.Empty;

    [FromForm(Name = "custom_service")]
    public string? CustomService { get; init; }

    [FromForm(Name = "message")]
    public string Description { get; init; } = string.Empty;

    [FromForm(Name = "source_page")]
    public string SourcePage { get; init; } = "/";

    [FromForm(Name = "utm_source")]
    public string? UtmSource { get; init; }

    [FromForm(Name = "utm_medium")]
    public string? UtmMedium { get; init; }

    [FromForm(Name = "utm_campaign")]
    public string? UtmCampaign { get; init; }

    [FromForm(Name = "brief_file")]
    public IFormFile? File { get; init; }

    [FromForm(Name = "personal_data_consent")]
    public bool PersonalDataConsent { get; init; }

    [FromForm(Name = "company")]
    public string? Company { get; init; }
}
