using DashkevichStudio.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DashkevichStudio.Infrastructure.Notifications;

public sealed class OutboxWorker(
    IServiceScopeFactory scopeFactory,
    TimeProvider timeProvider,
    ILogger<OutboxWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessBatchAsync(stoppingToken);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Outbox processing cycle failed.");
            }

            await Task.Delay(TimeSpan.FromSeconds(10), timeProvider, stoppingToken);
        }
    }

    private async Task ProcessBatchAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var outbox = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
        var leads = scope.ServiceProvider.GetRequiredService<ILeadRepository>();
        var notifier = scope.ServiceProvider.GetRequiredService<ITelegramNotifier>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var now = timeProvider.GetUtcNow();

        var messages = await outbox.GetPendingAsync(10, now, cancellationToken);
        foreach (var message in messages)
        {
            var lead = await leads.GetByIdAsync(message.LeadId, cancellationToken);
            if (lead is null)
            {
                message.MarkFailed("Lead not found.", now);
                continue;
            }

            try
            {
                await notifier.SendAsync(lead, cancellationToken);
                message.MarkSent(now);
                lead.MarkNotificationSent();
            }
            catch (Exception exception)
            {
                message.MarkFailed(exception.Message, now);
                lead.MarkNotificationFailed();
                logger.LogWarning(exception, "Could not send lead {LeadId} to Telegram.", lead.Id);
            }
        }

        if (messages.Count > 0)
            await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
