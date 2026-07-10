using DashkevichStudio.Application.Abstractions;
using DashkevichStudio.Domain.Leads;
using DashkevichStudio.Domain.Notifications;

namespace DashkevichStudio.Application.Leads;

public sealed class LeadSubmissionService(
    ILeadRepository leadRepository,
    IOutboxRepository outboxRepository,
    IUnitOfWork unitOfWork,
    IFileStorage fileStorage,
    TimeProvider timeProvider) : ILeadSubmissionService
{
    public async Task<Guid> CreateAsync(CreateLeadCommand command, CancellationToken cancellationToken)
    {
        CreateLeadValidator.Validate(command);

        var now = timeProvider.GetUtcNow();
        var lead = Lead.Create(
            command.Name,
            command.ContactMethod,
            command.ContactValue,
            command.Service,
            command.CustomService,
            command.Description,
            command.SourcePage,
            command.UtmSource,
            command.UtmMedium,
            command.UtmCampaign,
            now);

        string? storedName = null;

        try
        {
            if (command.File is not null)
            {
                var extension = Path.GetExtension(command.File.FileName).ToLowerInvariant();
                storedName = await fileStorage.SaveAsync(command.File.Content, extension, cancellationToken);
                lead.AddAttachment(
                    Path.GetFileName(command.File.FileName),
                    storedName,
                    command.File.ContentType,
                    command.File.Length);
            }

            await leadRepository.AddAsync(lead, cancellationToken);
            await outboxRepository.AddAsync(OutboxMessage.ForLead(lead.Id, now), cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return lead.Id;
        }
        catch
        {
            if (storedName is not null)
                await fileStorage.DeleteAsync(storedName, CancellationToken.None);

            throw;
        }
    }
}
