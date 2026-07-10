namespace DashkevichStudio.Application.Leads;

public interface ILeadSubmissionService
{
    Task<Guid> CreateAsync(CreateLeadCommand command, CancellationToken cancellationToken);
}
