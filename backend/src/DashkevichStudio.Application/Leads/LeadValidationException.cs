namespace DashkevichStudio.Application.Leads;

public sealed class LeadValidationException(IReadOnlyDictionary<string, string[]> errors)
    : Exception("Lead validation failed.")
{
    public IReadOnlyDictionary<string, string[]> Errors { get; } = errors;
}
